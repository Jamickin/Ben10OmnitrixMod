# Ben 10 Omnitrix Mod

A [tModLoader](https://tmodloader.net/) mod for Terraria that brings a story-based, immersive Omnitrix-style alien transformation system into the game, inspired by the classic 2005 *Ben 10* series.

**Status: bones/mechanics pass.** The full transformation framework and a 3-ability combat kit for all 10 classic aliens are working end-to-end. There is no character art yet — everything currently renders with placeholder colors/shapes. The story campaign, deeper progression, and richer combat/UI this project is building toward are still ahead — see "Project history" below for a note on how they're being built.

## Features

- **Real Omnitrix dial UI** — the selected alien is shown large and centered, not a mouse-angle radial menu. Cycle left/right to rotate through the roster like the actual show's dial.
- **Energy + cooldown system** — a shared Omnitrix energy meter drains while transformed and regenerates as human, with a post-revert cooldown lockout.
- **Mutual exclusivity by construction** — only one alien form can ever be active at a time.
- **Full multiplayer sync** — transformation state and unlocked aliens sync across clients.
- **A real 3-ability combat kit per alien** (Ability 1/2/3), not just a stat-swap — weapon/item use is disabled entirely while transformed, so aliens fight with their own powers.
- **The classic 10**: Heatblast, Four Arms, XLR8, Diamondhead, Grey Matter, Stinkfly, Ripjaws, Upgrade, Ghostfreak, Wildmutt.

## Story

This mod is built around an original campaign — its own multiverse take on Ben 10, not mainline canon. See [`docs/Story.md`](docs/Story.md) for the full narrative design (still evolving, actively being workshopped).

## Project history

An addon collaboration with a separate, pre-existing "Ben 10" tModLoader mod (Steam Workshop, by a different author) was explored and has been explicitly rejected. This project is fully independent — no code, architecture, assets, or design from that other mod is used or referenced. It was briefly evaluated as a potential collaboration, that evaluation is closed, and development here continues from scratch, on this project's own terms.

## Controls

| Key | Action |
|---|---|
| **T** | Open the Omnitrix dial / confirm the selected alien |
| **Left / Right arrow** | Cycle the dial while it's open |
| **R** | Cancel the dial while open, or revert to human while transformed |
| Attack (left-click) | Ability 1 |
| **E** | Ability 2 |
| **Q** | Ability 3 |

## Building

Requires the .NET 8 SDK and a local tModLoader install (via Steam). See [`CLAUDE.md`](./CLAUDE.md) for the full build setup, including a couple of real environment gotchas (native library paths, tModLoader's nested build step) that aren't obvious from tModLoader's own docs.

```bash
dotnet build
```

## Contributing

This is an active project — help is very welcome, especially with:

- **Sprites/art** — the biggest gap right now. Alien sprites, transformation VFX, UI art, buff icons.
- **Story/writing** — campaign structure, dialogue, quest pacing.
- **Sound design** — transformation sounds, ability sound effects.
- **Balance/gameplay feedback** — once you've tried the 10 kits in-game.

If you want to contribute, open an issue or PR, or reach out directly. See [`CLAUDE.md`](./CLAUDE.md) for the current architecture before diving into the code.

## Credits

- Mod created by James Wheeler ([@Jamickin](https://github.com/Jamickin))

## Related

- [MistbornModOwnBlend](https://github.com/Jamickin/MistbornModOwnBlend) — a Mistborn (Brandon Sanderson) Allomancy mod for Terraria, used as an architectural reference for this project.
