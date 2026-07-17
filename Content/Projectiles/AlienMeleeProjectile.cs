using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Projectiles
{
    // Shared base for melee-style primary attacks (Four Arms' punch, XLR8's slash, etc.) - an
    // invisible-ish hitbox that follows the player briefly in front of them toward the cursor,
    // the standard "melee swing via projectile" tModLoader pattern.
    public abstract class AlienMeleeProjectile : ModProjectile
    {
        public override string Texture =>
            "Ben10OmnitrixMod/Content/Projectiles/PlaceholderProjectile";

        protected virtual Color TintColor => Color.White;
        protected virtual int DustType => DustID.Torch;
        protected virtual int LifetimeTicks => 12;
        protected virtual float RangeFromPlayer => 40f;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = LifetimeTicks;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = LifetimeTicks;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.active)
            {
                Projectile.Kill();
                return;
            }

            Vector2 direction = Main.MouseWorld - player.Center;
            direction = direction == Vector2.Zero ? new Vector2(player.direction, 0f) : direction;
            direction.Normalize();

            Projectile.Center = player.Center + direction * RangeFromPlayer;

            if (Main.rand.NextBool(2))
            {
                Dust.NewDustPerfect(Projectile.Center, DustType, Vector2.Zero, 100, default, 1.2f);
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
