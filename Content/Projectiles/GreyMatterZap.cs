using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class GreyMatterZap : AlienBoltProjectile
    {
        protected override Color TintColor => new Color(150, 150, 220);
        protected override int DustType => DustID.Iron;
        protected override int LifetimeTicks => 50;

        protected override void ApplyOnHitEffect(NPC target)
        {
            target.AddBuff(BuffID.Weak, 240);
        }
    }
}
