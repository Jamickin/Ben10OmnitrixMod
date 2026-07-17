using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Heatblast's deep-mechanics pass - the first alien to get a real kit beyond the generic
    // stat bonus. Combat replaces the equipped weapon entirely while transformed (see
    // Ben10Player.PreItemCheck): attack input throws a rapid-fire Fireball, the special-ability
    // keybind unleashes a slower, harder-hitting Flame Nova, and Heatblast is thematically
    // immune to fire/lava as a Pyronite.
    public class HeatblastBuff : AlienFormBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Alien = AlienType.Heatblast;
        }

        public override void ApplyUniqueEffect(Player player, Ben10Player modPlayer)
        {
            player.buffImmune[BuffID.OnFire] = true;
            player.lavaImmune = true;

            if (Main.rand.NextBool(6))
            {
                Dust.NewDustPerfect(
                    player.Center + Main.rand.NextVector2Circular(14f, 20f),
                    DustID.Torch,
                    new Vector2(0f, -1.2f),
                    100,
                    default,
                    0.9f
                );
            }
        }

        public override void OnAbility1(Player player, Ben10Player modPlayer)
        {
            Vector2 direction = Main.MouseWorld - player.Center;
            if (direction == Vector2.Zero)
                direction = new Vector2(player.direction, 0f);
            else
                direction.Normalize();

            Vector2 spawnPos = player.Center + direction * 20f;
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(16);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:HeatblastFireball"),
                spawnPos,
                direction * 11f,
                ModContent.ProjectileType<HeatblastFireball>(),
                damage,
                2f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item20, player.position);
        }

        public override void OnAbility2(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(36);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:HeatblastFlameNova"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<HeatblastFlameNova>(),
                damage,
                4f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }
    }
}
