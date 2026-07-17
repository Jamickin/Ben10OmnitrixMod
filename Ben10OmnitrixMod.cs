using System.IO;
using Ben10OmnitrixMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod
{
    public class Ben10OmnitrixMod : Mod
    {
        // Omnitrix dial UI: press to open (shows the currently-selected alien centered on
        // screen), Cycle Left/Right rotate the dial counterclockwise/clockwise through the
        // roster, press again to confirm/transform. Matches the show's actual dial-and-slap
        // interaction rather than a mouse-angle radial menu.
        public static ModKeybind TransformKeybind { get; private set; }

        // Reverts when transformed; also cancels the open dial (without transforming) when
        // browsing it as human.
        public static ModKeybind RevertKeybind { get; private set; }

        public static ModKeybind CycleLeftKeybind { get; private set; }
        public static ModKeybind CycleRightKeybind { get; private set; }

        // Ability 1 has no dedicated keybind - it's attack input (left-click/controlUseItem),
        // which also replaces normal weapon/item use entirely while transformed (see
        // Ben10Player.PreItemCheck). Abilities 2 and 3 are dedicated keys, matching a classic
        // Ben 10 game's multi-button power scheme.
        public static ModKeybind Ability2Keybind { get; private set; }
        public static ModKeybind Ability3Keybind { get; private set; }

        public override void Load()
        {
            TransformKeybind = KeybindLoader.RegisterKeybind(this, "Open Omnitrix", "T");
            RevertKeybind = KeybindLoader.RegisterKeybind(this, "Revert Alien Form", "R");
            CycleLeftKeybind = KeybindLoader.RegisterKeybind(this, "Cycle Alien Left", "Left");
            CycleRightKeybind = KeybindLoader.RegisterKeybind(this, "Cycle Alien Right", "Right");
            Ability2Keybind = KeybindLoader.RegisterKeybind(this, "Alien Ability 2", "E");
            Ability3Keybind = KeybindLoader.RegisterKeybind(this, "Alien Ability 3", "Q");
        }

        public override void Unload()
        {
            TransformKeybind = null;
            RevertKeybind = null;
            CycleLeftKeybind = null;
            CycleRightKeybind = null;
            Ability2Keybind = null;
            Ability3Keybind = null;
        }

        // Multiplayer packet handling, mirroring MistbornMod.cs's manual byte-tagged router.
        // Ben10OmnitrixMod has its own independent packet-type namespace (packet routing is scoped per
        // Mod instance), so this starts fresh at 0 rather than continuing Mistborn's numbering.
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            byte packetType = reader.ReadByte();

            switch (packetType)
            {
                case 0: // Player sync (CurrentForm + UnlockedAliens)
                    HandlePlayerSyncPacket(reader, whoAmI);
                    break;
            }
        }

        private void HandlePlayerSyncPacket(BinaryReader reader, int whoAmI)
        {
            byte playerIndex = reader.ReadByte();
            if (playerIndex >= Main.maxPlayers)
                return;

            Player player = Main.player[playerIndex];
            if (player == null || !player.active)
                return;

            Ben10Player modPlayer = player.GetModPlayer<Ben10Player>();
            if (modPlayer == null)
                return;

            AlienType currentForm = (AlienType)reader.ReadByte();
            byte unlockedCount = reader.ReadByte();
            var unlocked = new System.Collections.Generic.HashSet<AlienType>();
            for (int i = 0; i < unlockedCount; i++)
            {
                unlocked.Add((AlienType)reader.ReadByte());
            }

            modPlayer.ApplySyncedState(currentForm, unlocked);

            // If we're the server, forward this to all other clients.
            if (Main.netMode == Terraria.ID.NetmodeID.Server)
            {
                ModPacket packet = GetPacket();
                packet.Write((byte)0);
                packet.Write(playerIndex);
                packet.Write((byte)currentForm);
                packet.Write((byte)unlocked.Count);
                foreach (AlienType alien in unlocked)
                {
                    packet.Write((byte)alien);
                }
                packet.Send(-1, whoAmI); // Send to everyone except sender
            }
        }
    }
}
