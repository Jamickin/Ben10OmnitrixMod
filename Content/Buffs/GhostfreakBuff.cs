using Ben10OmnitrixMod.Common.Players;
using Ben10OmnitrixMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ben10OmnitrixMod.Content.Buffs
{
    // Ectonurite intangibility - phases out of some hits entirely (reusing vanilla's Brain of
    // Confusion dodge mechanic, mirroring MistbornMod's AtiumBuff.cs confirmed pattern rather
    // than hand-rolled hit-modification hooks), too insubstantial to be knocked around.
    public class GhostfreakBuff : AlienFormBuff
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Alien = AlienType.Ghostfreak;
        }

        public override int Ability1CooldownTicks => 16;
        public override int Ability2CooldownTicks => 200;

        public override void ApplyUniqueEffect(Player player, Ben10Player modPlayer)
        {
            player.noKnockback = true;

            if (Main.rand.NextFloat() < 0.35f)
            {
                player.brainOfConfusionItem = new Item(ItemID.BrainOfConfusion);
            }
        }

        public override void OnAbility1(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(18);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:GhostfreakClaw"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<GhostfreakClaw>(),
                damage,
                3f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item1, player.position);
        }

        public override void OnAbility2(Player player, Ben10Player modPlayer)
        {
            int damage = (int)player.GetDamage(DamageClass.Generic).ApplyTo(32);

            Projectile.NewProjectile(
                player.GetSource_Misc("Ben10OmnitrixMod:GhostfreakDarkBurst"),
                player.Center,
                Vector2.Zero,
                ModContent.ProjectileType<GhostfreakDarkBurst>(),
                damage,
                4f,
                player.whoAmI
            );

            SoundEngine.PlaySound(SoundID.Item74, player.position);
        }
    }
}
