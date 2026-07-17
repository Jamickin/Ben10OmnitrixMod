using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Tetramand strength - overwhelming melee power, too heavy to be knocked around.
    public class FourArmsBuff : AlienFormBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Alien = AlienType.FourArms;
        }

        public override int Ability1CooldownTicks => 24;
        public override int Ability2CooldownTicks => 200;

        public override void ApplyUniqueEffect(Player player, Ben10Player modPlayer)
        {
            player.noKnockback = true;
        }

        public override void OnAbility1(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(30);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:FourArmsPunch"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<FourArmsPunch>(),
                damage,
                8f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item1, player.position);
        }

        public override void OnAbility2(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(50);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:FourArmsGroundPound"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<FourArmsGroundPound>(),
                damage,
                10f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }
    }
}
