using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Ben10OmnitrixMod.Common.Data
{
    // Centralized per-alien stat table, mirroring MistbornMod's "one place, not scattered"
    // convention (Content/Recipes/AllMetalRecipes.cs). Bones-scope: exactly one distinguishing
    // stat bonus per alien so the transformation system is provably different per-form; full
    // unique ability kits are a later content pass, not this one. Bonus fields follow
    // MistbornMod's PewterBuff convention (additive fractions on top of the game's baseline
    // 1.0, e.g. player.moveSpeed += 0.5f is a +50% bonus), not raw multipliers.
    public class AlienDefinition
    {
        public AlienType Alien { get; }
        public string DisplayName { get; }
        public Color WheelColor { get; }
        public float MoveSpeedBonus { get; }
        public float DamageBonus { get; }
        public int DefenseBonus { get; }

        public AlienDefinition(
            AlienType alien,
            string displayName,
            Color wheelColor,
            float moveSpeedBonus = 0f,
            float damageBonus = 0f,
            int defenseBonus = 0
        )
        {
            Alien = alien;
            DisplayName = displayName;
            WheelColor = wheelColor;
            MoveSpeedBonus = moveSpeedBonus;
            DamageBonus = damageBonus;
            DefenseBonus = defenseBonus;
        }

        public static readonly Dictionary<AlienType, AlienDefinition> Table = new Dictionary<
            AlienType,
            AlienDefinition
        >
        {
            [AlienType.Heatblast] = new AlienDefinition(
                AlienType.Heatblast,
                "Heatblast",
                new Color(255, 90, 20),
                damageBonus: 0.3f
            ),
            [AlienType.FourArms] = new AlienDefinition(
                AlienType.FourArms,
                "Four Arms",
                new Color(200, 60, 40),
                damageBonus: 0.6f
            ),
            [AlienType.XLR8] = new AlienDefinition(
                AlienType.XLR8,
                "XLR8",
                new Color(50, 110, 220),
                moveSpeedBonus: 1.5f
            ),
            [AlienType.Diamondhead] = new AlienDefinition(
                AlienType.Diamondhead,
                "Diamondhead",
                new Color(120, 220, 230),
                defenseBonus: 25
            ),
            [AlienType.GreyMatter] = new AlienDefinition(
                AlienType.GreyMatter,
                "Grey Matter",
                new Color(150, 150, 165),
                moveSpeedBonus: -0.3f
            ),
            [AlienType.Stinkfly] = new AlienDefinition(
                AlienType.Stinkfly,
                "Stinkfly",
                new Color(90, 200, 90),
                moveSpeedBonus: 0.4f
            ),
            [AlienType.Ripjaws] = new AlienDefinition(
                AlienType.Ripjaws,
                "Ripjaws",
                new Color(50, 140, 160),
                defenseBonus: 10
            ),
            [AlienType.Upgrade] = new AlienDefinition(
                AlienType.Upgrade,
                "Upgrade",
                new Color(120, 220, 100),
                defenseBonus: 15
            ),
            [AlienType.Ghostfreak] = new AlienDefinition(
                AlienType.Ghostfreak,
                "Ghostfreak",
                new Color(90, 60, 120),
                moveSpeedBonus: 0.3f
            ),
            [AlienType.Wildmutt] = new AlienDefinition(
                AlienType.Wildmutt,
                "Wildmutt",
                new Color(230, 170, 60),
                damageBonus: 0.4f
            ),
        };
    }
}
