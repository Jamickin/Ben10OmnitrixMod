using Ben10OmnitrixMod.Common.Data;
using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Abstract base mirroring MistbornMod's MetalBuff pattern. One thin subclass per alien
    // (Phase 4) rather than a single dynamic buff - tModLoader resolves a ModBuff's icon texture
    // once at content-load time, not per-frame, so a single buff can't swap icons at runtime
    // without extra plumbing. Ten subclasses sharing one placeholder texture costs ~5 lines each
    // now and gives real per-alien icon differentiation for free once art exists, with zero
    // added complexity - CurrentForm being a single value on Ben10Player still structurally
    // forbids more than one form (and therefore more than one of these buffs) being active.
    public abstract class AlienFormBuff : ModBuff
    {
        public AlienType Alien { get; protected set; }

        public override string Texture => "Ben10OmnitrixMod/Content/Buffs/PlaceholderBuffIcon";

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Ben10Player modPlayer = player.GetModPlayer<Ben10Player>();

            if (modPlayer.CurrentForm == Alien)
            {
                ApplyStatEffects(player);
                ApplyUniqueEffect(player, modPlayer);
            }

            // Dim the icon during the brief transition-out window as a visual cue that the
            // form is settling back to human, mirroring MetalBuff's buffAlpha convention.
            Main.buffAlpha[Type] = modPlayer.IsTransforming ? 0.5f : 1f;
        }

        // Applies this alien's one distinguishing stat bonus from the centralized
        // AlienDefinition table - generic, so subclasses don't need to repeat this logic.
        private void ApplyStatEffects(Player player)
        {
            if (!AlienDefinition.Table.TryGetValue(Alien, out AlienDefinition def))
                return;

            player.moveSpeed += def.MoveSpeedBonus;
            player.GetDamage(DamageClass.Generic) += def.DamageBonus;
            player.statDefense += def.DefenseBonus;
        }

        // Virtual hook for continuous per-tick unique effects (immunities, ambient VFX, etc.).
        // No-op until an alien's deep-mechanics pass gives it a body.
        public virtual void ApplyUniqueEffect(Player player, Ben10Player modPlayer) { }

        // Three dedicated ability slots, matching a classic Ben 10 game control scheme:
        // Ability 1 = attack input (left-click/controlUseItem, via Ben10Player.PreItemCheck,
        // which also cancels all normal weapon/item use while transformed - abilities replace
        // gear entirely). Ability 2 = Ben10OmnitrixMod.Ability2Keybind. Ability 3 = Ben10OmnitrixMod.Ability3Keybind,
        // implemented once here (a forward damaging dash) rather than per-alien, since the
        // mechanic itself is identical across the whole roster - only its tint/damage varies,
        // read from the centralized AlienDefinition table. Subclasses can still override
        // OnAbility3 later to replace the shared dash with something bespoke.
        public virtual int Ability1CooldownTicks => 20;
        public virtual int Ability2CooldownTicks => 180;
        public virtual int Ability3CooldownTicks => 140;

        public virtual void OnAbility1(Player player, Ben10Player modPlayer) { }

        public virtual void OnAbility2(Player player, Ben10Player modPlayer) { }

        public virtual void OnAbility3(Player player, Ben10Player modPlayer)
        {
            Vector2 direction = Main.MouseWorld - player.Center;
            direction = direction == Vector2.Zero ? new Vector2(player.direction, 0f) : direction;
            direction.Normalize();

            player.velocity = direction * 22f;

            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(24);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:AlienDash"),
                player.Center,
                direction * 20f,
                ModContent.ProjectileType<AlienDashBolt>(),
                damage,
                5f,
                player.whoAmI,
                (float)(int)Alien
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            if (player == null)
                return;

            Ben10Player modPlayer = player.GetModPlayer<Ben10Player>();
            if (modPlayer == null)
                return;

            tip += $"\nOmnitrix Energy: {(int)modPlayer.OmnitrixEnergy}%";
        }

        // Called once the transition-out actually completes and CurrentForm reverts to None.
        public virtual void OnBuffEnd(Player player, Ben10Player modPlayer) { }
    }
}
