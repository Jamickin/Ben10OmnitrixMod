using System;
using System.Collections.Generic;
using System.Linq;
using Ben10OmnitrixMod.Common.Data;
using Ben10OmnitrixMod.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Ben10OmnitrixMod.Common.UI
{
    // The actual Omnitrix dial from the show, not a mouse-angle radial menu: the currently
    // selected alien is shown large and centered, and Cycle Left/Right rotate the dial
    // counterclockwise/clockwise through the roster one alien at a time. Press T to open,
    // press T again to confirm/transform, press R to cancel and close without transforming.
    // Hand-rolled ModSystem + raw SpriteBatch, mirroring MistbornMod's DraggableMetalUI
    // pattern - no textures required (colored placeholder markers), matching this pass's
    // "bones, no art yet" scope.
    public class OmnitrixDialUI : ModSystem
    {
        private static readonly AlienType[] RosterOrder = (
            (AlienType[])Enum.GetValues(typeof(AlienType))
        )
            .Where(a => a != AlienType.None)
            .ToArray();

        public bool IsOpen { get; private set; }
        private int _selectedIndex = 0;

        public AlienType SelectedAlien => RosterOrder[_selectedIndex];

        public override void PostUpdateEverything()
        {
            Player player = Main.LocalPlayer;
            if (player == null || !player.active)
            {
                IsOpen = false;
                return;
            }

            Ben10Player modPlayer = player.GetModPlayer<Ben10Player>();

            if (Ben10OmnitrixMod.TransformKeybind?.JustPressed ?? false)
            {
                if (!IsOpen)
                {
                    // Open the dial, starting on the currently-active form if there is one.
                    if (modPlayer.CurrentForm != AlienType.None)
                    {
                        int idx = Array.IndexOf(RosterOrder, modPlayer.CurrentForm);
                        if (idx >= 0)
                            _selectedIndex = idx;
                    }
                    IsOpen = true;
                }
                else
                {
                    // Confirm: attempt to transform into (or, per Ben10Player.RequestTransform's
                    // bones-scope rule, revert from) the currently selected alien.
                    modPlayer.RequestTransform(SelectedAlien);
                    IsOpen = false;
                }
                return;
            }

            if (IsOpen)
            {
                if (Ben10OmnitrixMod.RevertKeybind?.JustPressed ?? false)
                {
                    // Cancel - close the dial without transforming.
                    IsOpen = false;
                    return;
                }

                player.controlUseItem = false;

                if (Ben10OmnitrixMod.CycleLeftKeybind?.JustPressed ?? false)
                {
                    _selectedIndex = (_selectedIndex - 1 + RosterOrder.Length) % RosterOrder.Length;
                }

                if (Ben10OmnitrixMod.CycleRightKeybind?.JustPressed ?? false)
                {
                    _selectedIndex = (_selectedIndex + 1) % RosterOrder.Length;
                }
            }
            else if (Ben10OmnitrixMod.RevertKeybind?.JustPressed ?? false)
            {
                modPlayer.RequestRevert();
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer =>
                layer.Name.Equals("Vanilla: Mouse Text")
            );
            if (mouseTextIndex == -1)
                return;

            layers.Insert(
                mouseTextIndex,
                new LegacyGameInterfaceLayer(
                    "Ben10OmnitrixMod: Omnitrix Dial",
                    delegate
                    {
                        if (IsOpen && !Main.gameMenu && Main.LocalPlayer.active)
                        {
                            DrawDial(Main.spriteBatch);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI
                )
            );
        }

        private void DrawDial(SpriteBatch spriteBatch)
        {
            Vector2 center = new Vector2(Main.screenWidth / 2f, Main.screenHeight / 2f - 60f);
            int count = RosterOrder.Length;

            // Ring of small position markers around the dial - the highlighted one is the
            // current selection, everything else dims. Arranged like a clock face so cycling
            // left/right visibly "rotates" the selection around the ring.
            const float ringRadius = 100f;
            for (int i = 0; i < count; i++)
            {
                float angle = MathHelper.TwoPi * i / count - MathHelper.PiOver2;
                Vector2 dotPos =
                    center
                    + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * ringRadius;

                bool isSelected = i == _selectedIndex;
                AlienDefinition.Table.TryGetValue(RosterOrder[i], out AlienDefinition def);
                Color dotColor = def != null ? def.WheelColor : Color.Gray;
                if (!isSelected)
                    dotColor *= 0.4f;

                int size = isSelected ? 16 : 9;
                spriteBatch.Draw(
                    Terraria.GameContent.TextureAssets.MagicPixel.Value,
                    new Rectangle((int)dotPos.X - size / 2, (int)dotPos.Y - size / 2, size, size),
                    dotColor
                );
            }

            // Large centered badge for the selected alien.
            AlienDefinition.Table.TryGetValue(SelectedAlien, out AlienDefinition selectedDef);
            Color badgeColor = selectedDef != null ? selectedDef.WheelColor : Color.White;
            string name = selectedDef != null ? selectedDef.DisplayName : SelectedAlien.ToString();

            spriteBatch.Draw(
                Terraria.GameContent.TextureAssets.MagicPixel.Value,
                new Rectangle((int)center.X - 34, (int)center.Y - 34, 68, 68),
                badgeColor
            );

            Utils.DrawBorderStringFourWay(
                spriteBatch,
                Terraria.GameContent.FontAssets.MouseText.Value,
                name,
                center.X - 60f,
                center.Y + 46f,
                Color.White,
                Color.Black,
                Vector2.Zero,
                1.15f
            );

            // Dim previews of the immediate neighbors either side, reinforcing the "scrolling
            // through a dial" feel without needing an actual rotation animation.
            DrawNeighborLabel(spriteBatch, center, -1, -150f);
            DrawNeighborLabel(spriteBatch, center, 1, 150f);

            Utils.DrawBorderStringFourWay(
                spriteBatch,
                Terraria.GameContent.FontAssets.MouseText.Value,
                "Cycle Left/Right to rotate - Transform to confirm - Revert to cancel",
                center.X - 190f,
                center.Y + 130f,
                Color.Silver,
                Color.Black,
                Vector2.Zero,
                0.85f
            );
        }

        private void DrawNeighborLabel(
            SpriteBatch spriteBatch,
            Vector2 center,
            int offset,
            float xOffset
        )
        {
            int index = (_selectedIndex + offset + RosterOrder.Length) % RosterOrder.Length;
            AlienDefinition.Table.TryGetValue(RosterOrder[index], out AlienDefinition def);
            string name = def != null ? def.DisplayName : RosterOrder[index].ToString();

            Utils.DrawBorderStringFourWay(
                spriteBatch,
                Terraria.GameContent.FontAssets.MouseText.Value,
                name,
                center.X + xOffset - 30f,
                center.Y - 6f,
                Color.Gray,
                Color.Black,
                Vector2.Zero,
                0.75f
            );
        }
    }
}
