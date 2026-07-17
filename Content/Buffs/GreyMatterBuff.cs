using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Galvan intellect - weak but rapid-fire zaps that outsmart enemies (Weak debuff), plus an
    // EMP burst that scrambles their senses (Confused debuff).
    public class GreyMatterBuff : AlienFormBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Alien = AlienType.GreyMatter;
        }

        public override int Ability1CooldownTicks => 12;
        public override int Ability2CooldownTicks => 160;

        public override void OnAbility1(Player player, Ben10Player modPlayer)
        {
            Vector2 direction = Main.MouseWorld - player.Center;
            direction = direction == Vector2.Zero ? new Vector2(player.direction, 0f) : direction;
            direction.Normalize();
            Vector2 spawnPos = player.Center + direction * 16f;

            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(8);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:GreyMatterZap"),
                spawnPos,
                direction * 9f,
                ModContent.ProjectileType<GreyMatterZap>(),
                damage,
                1f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item1, player.position);
        }

        public override void OnAbility2(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(18);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:GreyMatterEMP"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<GreyMatterEMP>(),
                damage,
                2f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }
    }
}
