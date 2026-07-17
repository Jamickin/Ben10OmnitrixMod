using Ben10OmnitrixMod.Common.Data;
using Microsoft.Xna.Framework;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    // Shared Ability 3 (dash) projectile for every alien - unlike Ability 1/2, the dash's
    // mechanics are identical across the whole roster, only its color varies, so one class
    // suffices. The spawning alien is passed via Projectile.ai[0] (set as AlienFormBuff's
    // OnAbility3 spawn call) rather than needing a subclass per alien.
    public class AlienDashBolt : AlienBoltProjectile
    {
        protected override int Pierce => -1;
        protected override int LifetimeTicks => 15;

        public override Color? GetAlpha(Color lightColor)
        {
            AlienType alien = (AlienType)(int)Projectile.ai[0];
            return AlienDefinition.Table.TryGetValue(alien, out AlienDefinition def)
                ? def.WheelColor
                : Color.White;
        }
    }
}
