using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class HeatblastFlameNova : AlienNovaProjectile
    {
        protected override Color TintColor => Color.OrangeRed;
        protected override int DustType => DustID.Torch;

        protected override void ApplyOnHitEffect(NPC target)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }
    }
}
