using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class WildmuttRoar : AlienNovaProjectile
    {
        protected override Color TintColor => new Color(230, 170, 60);
        protected override int DustType => DustID.Blood;
        protected override int RadiusSize => 240;

        protected override void ApplyOnHitEffect(NPC target)
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.active)
                owner.Heal(5);
        }
    }
}
