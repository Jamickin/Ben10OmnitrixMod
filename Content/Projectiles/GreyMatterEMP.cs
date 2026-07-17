using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class GreyMatterEMP : AlienNovaProjectile
    {
        protected override Color TintColor => new Color(150, 150, 220);
        protected override int DustType => DustID.Iron;
        protected override int RadiusSize => 200;

        protected override void ApplyOnHitEffect(NPC target)
        {
            target.AddBuff(BuffID.Confused, 240);
        }
    }
}
