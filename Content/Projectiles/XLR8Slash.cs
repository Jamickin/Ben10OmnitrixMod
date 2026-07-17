using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class XLR8Slash : AlienMeleeProjectile
    {
        protected override Color TintColor => new Color(60, 130, 230);
        protected override int DustType => DustID.Iron;
        protected override float RangeFromPlayer => 38f;
        protected override int LifetimeTicks => 8;
    }
}
