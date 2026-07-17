using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class GhostfreakClaw : AlienMeleeProjectile
    {
        protected override Color TintColor => new Color(90, 60, 120);
        protected override int DustType => DustID.Blood;
        protected override float RangeFromPlayer => 38f;
    }
}
