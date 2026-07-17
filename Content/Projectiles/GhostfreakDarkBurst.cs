using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class GhostfreakDarkBurst : AlienNovaProjectile
    {
        protected override Color TintColor => new Color(90, 60, 120);
        protected override int DustType => DustID.Blood;

        protected override void ApplyOnHitEffect(NPC target)
        {
            target.AddBuff(BuffID.Weak, 240);
        }
    }
}
