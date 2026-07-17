using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class UpgradeOverload : AlienNovaProjectile
    {
        protected override Color TintColor => new Color(120, 220, 100);
        protected override int DustType => DustID.Iron;
        protected override int RadiusSize => 240;
    }
}
