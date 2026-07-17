using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class RipjawsFinSlash : AlienNovaProjectile
    {
        protected override Color TintColor => new Color(50, 140, 160);
        protected override int DustType => DustID.Iron;
        protected override int RadiusSize => 200;
    }
}
