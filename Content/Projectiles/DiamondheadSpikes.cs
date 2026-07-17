using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class DiamondheadSpikes : AlienNovaProjectile
    {
        protected override Color TintColor => new Color(140, 230, 235);
        protected override int DustType => DustID.Iron;
    }
}
