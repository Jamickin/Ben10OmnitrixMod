using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class HeatblastFireball : AlienBoltProjectile
    {
        protected override Color TintColor => new Color(255, 140, 40);
        protected override int DustType => DustID.Torch;

        protected override void ApplyOnHitEffect(NPC target)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
    }
}
