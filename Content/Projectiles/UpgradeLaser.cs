using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class UpgradeLaser : AlienBoltProjectile
    {
        protected override Color TintColor => new Color(120, 220, 100);
        protected override int DustType => DustID.Iron;
        protected override int Pierce => -1;
        protected override int LifetimeTicks => 40;
    }
}
