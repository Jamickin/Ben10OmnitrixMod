using System.Collections.Generic;
using System.Linq;
using Ben10OmnitrixMod.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Ben10OmnitrixMod.Common.Players
{
    public class Ben10Player : ModPlayer
    {
        private const int TRANSITION_DURATION_TICKS = 30; // 0.5s - short transition for now
        private const int COOLDOWN_DURATION_TICKS = 180; // 3s post-revert lockout
        private const float MAX_ENERGY = 100f;
        private const float MIN_ENERGY_TO_TRANSFORM = 15f;
        private const float ENERGY_DRAIN_PER_TICK = 100f / (60f * 60f); // full drain over 60s active
        private const float ENERGY_REGEN_PER_TICK = 100f / (60f * 90f); // full regen over 90s human

        // Settled state; None = human. Single value, not a dict - structurally enforces that
        // only one alien can ever be active at a time (unlike MistbornPlayer's simultaneous
        // multi-metal-burning model, which Ben10 deliberately does not need).
        public AlienType CurrentForm { get; private set; } = AlienType.None;

        // Target of an in-flight transition (transform-in) or AlienType.None (transform-out).
        public AlienType PendingForm { get; private set; } = AlienType.None;

        // True during both transform-in and revert-out lock windows.
        public bool IsTransforming { get; private set; } = false;

        // Ticks remaining in the current transition.
        public int TransitionTimer { get; private set; } = 0;

        // 0-100, one shared pool - Ben10 doesn't need Mistborn's per-metal-reserve complexity.
        public float OmnitrixEnergy { get; private set; } = MAX_ENERGY;

        // Ticks; post-revert lockout. Lives on ModPlayer (not on the buff) deliberately - a
        // documented MistbornMod bug class was cooldowns stored on ModBuff instances breaking
        // because buffs get recreated/re-added constantly.
        public int CooldownTimer { get; private set; } = 0;

        // Bones-scope default: all 10 unlocked. Gating slots in later via a future
        // UnlockAlien() call with zero architecture change - the wheel UI and RequestTransform
        // both gate exclusively on this set already.
        public HashSet<AlienType> UnlockedAliens { get; private set; } = new HashSet<AlienType>();

        // Rate-limits for the three ability-hook slots (AlienFormBuff.OnAbility1/2/3). One set
        // of fields is sufficient since CurrentForm already enforces only one alien (and
        // therefore one ability kit) can be active at a time.
        public int Ability1Cooldown { get; private set; } = 0;
        public int Ability2Cooldown { get; private set; } = 0;
        public int Ability3Cooldown { get; private set; } = 0;

        public override void Initialize()
        {
            CurrentForm = AlienType.None;
            PendingForm = AlienType.None;
            IsTransforming = false;
            TransitionTimer = 0;
            OmnitrixEnergy = MAX_ENERGY;
            CooldownTimer = 0;
            Ability1Cooldown = 0;
            Ability2Cooldown = 0;
            Ability3Cooldown = 0;

            UnlockedAliens.Clear();
            foreach (AlienType alien in System.Enum.GetValues(typeof(AlienType)))
            {
                if (alien != AlienType.None)
                    UnlockedAliens.Add(alien);
            }
        }

        // Single public entry point for the alien-select UI. Bones-scope rule: if already
        // transformed, any selection just reverts (discards the new pick) rather than queuing
        // a transform-into-new-alien - avoids a subtly buggy queued-transition feature this pass.
        public void RequestTransform(AlienType target)
        {
            if (IsTransforming)
                return;

            if (CurrentForm != AlienType.None)
            {
                BeginRevert(forced: false);
                return;
            }

            if (target == AlienType.None)
                return;

            if (!UnlockedAliens.Contains(target))
            {
                ShowCannotTransformMessage("You haven't unlocked that alien yet!");
                return;
            }

            if (CooldownTimer > 0)
            {
                ShowCannotTransformMessage("The Omnitrix is recharging...");
                return;
            }

            if (OmnitrixEnergy < MIN_ENERGY_TO_TRANSFORM)
            {
                ShowCannotTransformMessage("Not enough Omnitrix energy!");
                return;
            }

            PendingForm = target;
            IsTransforming = true;
            TransitionTimer = TRANSITION_DURATION_TICKS;
            PlayTransitionEffect();
        }

        public void RequestRevert()
        {
            if (IsTransforming || CurrentForm == AlienType.None)
                return;

            BeginRevert(forced: false);
        }

        private void BeginRevert(bool forced)
        {
            PendingForm = AlienType.None;
            IsTransforming = true;
            TransitionTimer = TRANSITION_DURATION_TICKS;
            PlayTransitionEffect();

            if (forced)
                Main.NewText("The Omnitrix timed out!", 255, 100, 100);
        }

        // Attack input (Ability 1) replaces weapon combat entirely while transformed - routes
        // to the active alien's own AlienFormBuff.OnAbility1 instead of the equipped item.
        // Returning false cancels all normal item-use processing (animation, mana, vanilla
        // effects), the standard tModLoader pattern for "abilities replace gear" mods.
        public override bool PreItemCheck()
        {
            if (CurrentForm == AlienType.None)
                return true;

            if (Ability1Cooldown <= 0 && Player.controlUseItem)
            {
                AlienFormBuff alienBuff = GetActiveAlienBuff();
                if (alienBuff != null)
                {
                    alienBuff.OnAbility1(Player, this);
                    Ability1Cooldown = alienBuff.Ability1CooldownTicks;
                }
            }

            return false;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (CurrentForm == AlienType.None)
                return;

            if ((Ben10OmnitrixMod.Ability2Keybind?.JustPressed ?? false) && Ability2Cooldown <= 0)
            {
                AlienFormBuff alienBuff = GetActiveAlienBuff();
                if (alienBuff != null)
                {
                    alienBuff.OnAbility2(Player, this);
                    Ability2Cooldown = alienBuff.Ability2CooldownTicks;
                }
            }

            if ((Ben10OmnitrixMod.Ability3Keybind?.JustPressed ?? false) && Ability3Cooldown <= 0)
            {
                AlienFormBuff alienBuff = GetActiveAlienBuff();
                if (alienBuff != null)
                {
                    alienBuff.OnAbility3(Player, this);
                    Ability3Cooldown = alienBuff.Ability3CooldownTicks;
                }
            }
        }

        private AlienFormBuff GetActiveAlienBuff()
        {
            int buffId = GetBuffIDForAlien(CurrentForm);
            if (buffId == -1)
                return null;

            return ModContent.GetModBuff(buffId) as AlienFormBuff;
        }

        public override void PostUpdate()
        {
            if (CooldownTimer > 0)
                CooldownTimer--;

            if (Ability1Cooldown > 0)
                Ability1Cooldown--;

            if (Ability2Cooldown > 0)
                Ability2Cooldown--;

            if (Ability3Cooldown > 0)
                Ability3Cooldown--;

            // Keep the buff alive & refreshed for as long as CurrentForm holds a real alien -
            // this covers both the settled active-form window and the brief transforming-out
            // window, so the form's stats hold until the transition-out visually completes.
            if (CurrentForm != AlienType.None)
            {
                int buffId = GetBuffIDForAlien(CurrentForm);
                if (buffId != -1)
                    Player.AddBuff(buffId, 5);
            }

            if (IsTransforming)
            {
                TransitionTimer--;
                if (TransitionTimer <= 0)
                {
                    IsTransforming = false;
                    if (PendingForm != AlienType.None)
                    {
                        // Finished transforming IN.
                        CurrentForm = PendingForm;
                        PendingForm = AlienType.None;
                    }
                    else if (CurrentForm != AlienType.None)
                    {
                        // Finished transforming OUT.
                        EndFormCompletely();
                    }
                }
                return; // no energy drain/regen mid-transition
            }

            if (CurrentForm != AlienType.None)
            {
                OmnitrixEnergy = System.Math.Max(0f, OmnitrixEnergy - ENERGY_DRAIN_PER_TICK);
                if (OmnitrixEnergy <= 0f)
                {
                    BeginRevert(forced: true);
                }
            }
            else
            {
                OmnitrixEnergy = System.Math.Min(
                    MAX_ENERGY,
                    OmnitrixEnergy + ENERGY_REGEN_PER_TICK
                );
            }
        }

        private void EndFormCompletely()
        {
            AlienType endingForm = CurrentForm;
            int buffId = GetBuffIDForAlien(endingForm);
            if (buffId != -1)
            {
                ModBuff modBuff = ModContent.GetModBuff(buffId);
                if (modBuff is AlienFormBuff alienBuff)
                    alienBuff.OnBuffEnd(Player, this);
                Player.ClearBuff(buffId);
            }

            CurrentForm = AlienType.None;
            CooldownTimer = COOLDOWN_DURATION_TICKS;
        }

        // String-based lookup (not a direct class reference) so this file compiles standalone
        // before the 10 concrete AlienFormBuff subclasses exist - returns -1 gracefully until
        // they're registered, letting the rest of the state machine be testable incrementally.
        private int GetBuffIDForAlien(AlienType alien)
        {
            if (alien == AlienType.None)
                return -1;

            if (ModContent.TryFind<ModBuff>("Ben10OmnitrixMod", alien + "Buff", out ModBuff buff))
                return buff.Type;

            return -1;
        }

        private void PlayTransitionEffect()
        {
            // Placeholder flash + sound - the isolated swap point for a longer cinematic
            // sequence later. No custom art required (reuses vanilla dust/sound IDs).
            SoundEngine.PlaySound(SoundID.MaxMana, Player.position);

            for (int i = 0; i < 30; i++)
            {
                Vector2 dustVel = Main.rand.NextVector2CircularEdge(3f, 3f);
                Dust.NewDustPerfect(Player.Center, DustID.Torch, dustVel, 100, default, 1.5f);
            }
        }

        private void ShowCannotTransformMessage(string message)
        {
            Main.NewText(message, 255, 100, 100);
            SoundEngine.PlaySound(SoundID.MenuTick, Player.position);
        }

        public override void SaveData(TagCompound tag)
        {
            tag["Ben10_CurrentForm"] = CurrentForm.ToString();
            tag["Ben10_OmnitrixEnergy"] = OmnitrixEnergy;
            tag["Ben10_CooldownTimer"] = CooldownTimer;
            tag["Ben10_UnlockedAliens"] = UnlockedAliens.Select(a => a.ToString()).ToList();
        }

        public override void LoadData(TagCompound tag)
        {
            Initialize();

            if (
                tag.ContainsKey("Ben10_CurrentForm")
                && System.Enum.TryParse(tag.GetString("Ben10_CurrentForm"), out AlienType form)
            )
            {
                CurrentForm = form;
            }

            if (tag.ContainsKey("Ben10_OmnitrixEnergy"))
                OmnitrixEnergy = tag.GetFloat("Ben10_OmnitrixEnergy");

            if (tag.ContainsKey("Ben10_CooldownTimer"))
                CooldownTimer = tag.GetInt("Ben10_CooldownTimer");

            if (tag.ContainsKey("Ben10_UnlockedAliens"))
            {
                var names = tag.Get<List<string>>("Ben10_UnlockedAliens");
                UnlockedAliens.Clear();
                foreach (string name in names)
                {
                    if (System.Enum.TryParse(name, out AlienType alien))
                        UnlockedAliens.Add(alien);
                }
            }
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((byte)0); // Packet type 0 = player sync (Ben10OmnitrixMod's own packet namespace)
            packet.Write((byte)Player.whoAmI);
            packet.Write((byte)CurrentForm);
            packet.Write((byte)UnlockedAliens.Count);
            foreach (AlienType alien in UnlockedAliens)
            {
                packet.Write((byte)alien);
            }
            packet.Send(toWho, fromWho);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            Ben10Player clientModPlayer = clientPlayer as Ben10Player;

            if (
                CurrentForm != clientModPlayer.CurrentForm
                || UnlockedAliens.Count != clientModPlayer.UnlockedAliens.Count
                || !UnlockedAliens.SetEquals(clientModPlayer.UnlockedAliens)
            )
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)0);
                packet.Write((byte)Player.whoAmI);
                packet.Write((byte)CurrentForm);
                packet.Write((byte)UnlockedAliens.Count);
                foreach (AlienType alien in UnlockedAliens)
                {
                    packet.Write((byte)alien);
                }
                packet.Send();
            }
        }

        public override void CopyClientState(ModPlayer targetCopy)
        {
            Ben10Player clone = targetCopy as Ben10Player;
            clone.CurrentForm = CurrentForm;
            clone.UnlockedAliens = new HashSet<AlienType>(UnlockedAliens);
        }

        // Applies authoritative state received from a multiplayer sync packet (Ben10OmnitrixMod.cs's
        // HandlePacket). Separate from the RequestTransform/RequestRevert state machine entry
        // points, since this overwrites state directly rather than validating a transition.
        public void ApplySyncedState(AlienType currentForm, HashSet<AlienType> unlockedAliens)
        {
            CurrentForm = currentForm;
            UnlockedAliens = new HashSet<AlienType>(unlockedAliens);
        }
    }
}
