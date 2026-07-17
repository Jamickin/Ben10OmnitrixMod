using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class StinkflyGoo : AlienBoltProjectile
    {
        protected override Color TintColor => new Color(90, 200, 90);
        protected override int DustType => DustID.Blood;
        protected override int LifetimeTicks => 70;

        protected override void ApplyOnHitEffect(NPC target)
        {
            target.AddBuff(BuffID.Slow, 180);
        }
    }
}
