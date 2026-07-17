using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Lepidopterran flight - softened falls and floaty descent, plus goo shots that bog
    // enemies down (Slow debuff).
    public class StinkflyBuff : AlienFormBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Alien = AlienType.Stinkfly;
        }

        public override int Ability1CooldownTicks => 18;
        public override int Ability2CooldownTicks => 200;

        public override void ApplyUniqueEffect(Player player, Ben10Player modPlayer)
        {
            player.noFallDmg = true;

            // Floaty descent - not full flight yet, just a softened fall.
            if (player.velocity.Y > 0f)
            {
                player.velocity.Y *= 0.94f;
            }
        }

        public override void OnAbility1(Player player, Ben10Player modPlayer)
        {
            Vector2 direction = Main.MouseWorld - player.Center;
            direction = direction == Vector2.Zero ? new Vector2(player.direction, 0f) : direction;
            direction.Normalize();
            Vector2 spawnPos = player.Center + direction * 18f;

            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(14);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:StinkflyGoo"),
                spawnPos,
                direction * 8f,
                ModContent.ProjectileType<StinkflyGoo>(),
                damage,
                2f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item1, player.position);
        }

        public override void OnAbility2(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(30);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:StinkflyGooBomb"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<StinkflyGooBomb>(),
                damage,
                3f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }
    }
}
