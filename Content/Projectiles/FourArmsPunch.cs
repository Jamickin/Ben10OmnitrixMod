using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class FourArmsPunch : AlienMeleeProjectile
    {
        protected override Color TintColor => new Color(210, 70, 50);
        protected override int DustType => DustID.Blood;
        protected override float RangeFromPlayer => 46f;
    }
}
