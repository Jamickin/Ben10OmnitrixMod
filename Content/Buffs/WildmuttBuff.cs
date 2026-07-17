using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Vaxasaurian feral predator - heightened senses feed straight into hunger; every hit
    // heals the player a little (lifesteal, applied in WildmuttClaw/WildmuttRoar).
    public class WildmuttBuff : AlienFormBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Alien = AlienType.Wildmutt;
        }

        public override int Ability1CooldownTicks => 14;
        public override int Ability2CooldownTicks => 200;

        public override void OnAbility1(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(22);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:WildmuttClaw"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<WildmuttClaw>(),
                damage,
                5f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item1, player.position);
        }

        public override void OnAbility2(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(40);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:WildmuttRoar"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<WildmuttRoar>(),
                damage,
                7f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }
    }
}
