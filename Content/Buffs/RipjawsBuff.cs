using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Piscciss Volann aquatic physiology - breathes underwater, fast bite attack, spinning
    // fin-slash special.
    public class RipjawsBuff : AlienFormBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Alien = AlienType.Ripjaws;
        }

        public override int Ability1CooldownTicks => 16;
        public override int Ability2CooldownTicks => 180;

        public override void ApplyUniqueEffect(Player player, Ben10Player modPlayer)
        {
            player.gills = true;
        }

        public override void OnAbility1(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(20);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:RipjawsBite"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<RipjawsBite>(),
                damage,
                4f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item1, player.position);
        }

        public override void OnAbility2(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(34);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:RipjawsFinSlash"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<RipjawsFinSlash>(),
                damage,
                5f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }
    }
}
