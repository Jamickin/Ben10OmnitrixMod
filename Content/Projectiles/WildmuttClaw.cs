using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    public class WildmuttClaw : AlienMeleeProjectile
    {
        protected override Color TintColor => new Color(230, 170, 60);
        protected override int DustType => DustID.Blood;
        protected override float RangeFromPlayer => 40f;

        protected override void ApplyOnHitEffect(NPC target)
        {
            Player owner = Main.player[Projectile.owner];
            if (owner.active)
                owner.Heal(3);
        }
    }
}
