using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    // Shared base for fast-travelling ranged primary-attack projectiles (Heatblast's Fireball,
    // Diamondhead's crystal shard, etc.) - one thin subclass per alien sets tint/debuff/pierce,
    // mirroring AlienFormBuff's one-thin-subclass-per-alien pattern. All share
    // PlaceholderProjectile.png (tinted via GetAlpha) until real art exists.
    public abstract class AlienBoltProjectile : ModProjectile
    {
        public override string Texture =>
            "Ben10OmnitrixMod/Content/Projectiles/PlaceholderProjectile";

        protected virtual Color TintColor => Color.White;
        protected virtual int DustType => DustID.Torch;
        protected virtual int Pierce => 1;
        protected virtual int LifetimeTicks => 90;
        protected virtual Vector3 LightColor => new Vector3(0.6f, 0.3f, 0.1f);

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = Pierce;
            Projectile.timeLeft = LifetimeTicks;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.light = LightColor.X;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, LightColor.X, LightColor.Y, LightColor.Z);

            if (Main.rand.NextBool(2))
            {
                Dust.NewDustPerfect(
                    Projectile.Center,
                    DustType,
                    -Projectile.velocity * 0.15f,
                    100,
                    default,
                    1.1f
                );
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ApplyOnHitEffect(target);
        }

        protected virtual void ApplyOnHitEffect(NPC target) { }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustPerfect(
                    Projectile.Center,
                    DustType,
                    Main.rand.NextVector2Circular(3f, 3f),
                    100,
                    default,
                    1.3f
                );
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return TintColor;
        }
    }
}
