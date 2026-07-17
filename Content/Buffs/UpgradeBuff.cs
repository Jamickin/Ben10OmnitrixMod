using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Galvanic Mechomorph living technology - fast piercing laser fire and an overload burst.
    public class UpgradeBuff : AlienFormBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Alien = AlienType.Upgrade;
        }

        public override int Ability1CooldownTicks => 8;
        public override int Ability2CooldownTicks => 220;

        public override void OnAbility1(Player player, Ben10Player modPlayer)
        {
            Vector2 direction = Main.MouseWorld - player.Center;
            direction = direction == Vector2.Zero ? new Vector2(player.direction, 0f) : direction;
            direction.Normalize();
            Vector2 spawnPos = player.Center + direction * 20f;

            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(9);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:UpgradeLaser"),
                spawnPos,
                direction * 16f,
                ModContent.ProjectileType<UpgradeLaser>(),
                damage,
                1f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item1, player.position);
        }

        public override void OnAbility2(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(45);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:UpgradeOverload"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<UpgradeOverload>(),
                damage,
                6f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }
    }
}
