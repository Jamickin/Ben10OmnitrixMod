using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Petrosapien crystal body - piercing shard shots, an AOE spike burst, and too dense to
    // be knocked around.
    public class DiamondheadBuff : AlienFormBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Alien = AlienType.Diamondhead;
        }

        public override int Ability1CooldownTicks => 22;
        public override int Ability2CooldownTicks => 200;

        public override void ApplyUniqueEffect(Player player, Ben10Player modPlayer)
        {
            player.noKnockback = true;
        }

        public override void OnAbility1(Player player, Ben10Player modPlayer)
        {
            Vector2 direction = Main.MouseWorld - player.Center;
            direction = direction == Vector2.Zero ? new Vector2(player.direction, 0f) : direction;
            direction.Normalize();
            Vector2 spawnPos = player.Center + direction * 20f;

            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(20);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:DiamondheadShard"),
                spawnPos,
                direction * 13f,
                ModContent.ProjectileType<DiamondheadShard>(),
                damage,
                3f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item1, player.position);
        }

        public override void OnAbility2(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(38);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:DiamondheadSpikes"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<DiamondheadSpikes>(),
                damage,
                5f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }
    }
}
