using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    // Shared base for stationary AOE special-ability bursts (Heatblast's Flame Nova, etc.) -
    // damages everything it overlaps once via ordinary collision (localNPCHitCooldown spans
    // the whole lifetime), mirroring SteelAnchorBullet.cs's confirmed-working ModProjectile
    // hooks rather than a hand-rolled NPC-loop damage helper.
    public abstract class AlienNovaProjectile : ModProjectile
    {
        public override string Texture =>
            "Ben10OmnitrixMod/Content/Projectiles/PlaceholderProjectile";

        protected virtual Color TintColor => Color.White;
        protected virtual int DustType => DustID.Torch;
        protected virtual int RadiusSize => 220;
        protected virtual int LifetimeTicks => 18;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = RadiusSize;
            Projectile.height = RadiusSize;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = LifetimeTicks;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = LifetimeTicks;
            Projectile.light = 1f;
            Projectile.alpha = 120;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.9f, 0.6f, 0.3f);

            if (Main.rand.NextBool(2))
            {
                Dust.NewDustPerfect(
                    Projectile.Center
                        + Main.rand.NextVector2Circular(RadiusSize * 0.4f, RadiusSize * 0.4f),
                    DustType,
                    Vector2.Zero,
                    100,
                    default,
                    1.6f
                );
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ApplyOnHitEffect(target);
        }

        protected virtual void ApplyOnHitEffect(NPC target) { }

        public override Color? GetAlpha(Color lightColor)
        {
            return TintColor;
        }
    }
}
