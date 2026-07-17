using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class FourArmsGroundPound : AlienNovaProjectile
    {
        protected override Color TintColor => new Color(180, 60, 40);
        protected override int DustType => DustID.Blood;
        protected override int RadiusSize => 260;
    }
}
