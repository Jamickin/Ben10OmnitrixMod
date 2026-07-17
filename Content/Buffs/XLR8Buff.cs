using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Kineceleran speed - rapid low-damage slashes, a heavier "sonic strike" follow-up, and
    // the shared Ability 3 dash (inherited as-is from AlienFormBuff - XLR8 doesn't need a
    // bespoke override, its whole kit is built around being fast).
    public class XLR8Buff : AlienFormBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Alien = AlienType.XLR8;
        }

        public override int Ability1CooldownTicks => 10;
        public override int Ability2CooldownTicks => 60;
        public override int Ability3CooldownTicks => 100;

        public override void ApplyUniqueEffect(Player player, Ben10Player modPlayer)
        {
            player.buffImmune[BuffID.Slow] = true;
        }

        public override void OnAbility1(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(10);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:XLR8Slash"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<XLR8Slash>(),
                damage,
                3f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item1, player.position);
        }

        public override void OnAbility2(Player player, Ben10Player modPlayer)
        {
            // Sonic Strike - a single heavier hit, same hitbox as the basic slash but stronger
            // and on a longer cooldown, rather than a bespoke projectile type.
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(26);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:XLR8SonicStrike"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<XLR8Slash>(),
                damage,
                6f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }
    }
}
