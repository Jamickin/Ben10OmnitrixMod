# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is **Ben10OmnitrixMod**, a tModLoader mod for Terraria implementing an Omnitrix-style alien transformation system, inspired by the original 2005 Ben 10 series. Built using C# and the tModLoader framework, structured following the sibling `MistbornModOwnBlend` project's conventions.

Current scope: the transformation *framework* (energy, cooldown, mutual exclusivity, save/load, multiplayer sync) working end-to-end for the classic 10 aliens (Heatblast, Four Arms, XLR8, Diamondhead, Grey Matter, Stinkfly, Ripjaws, Upgrade, Ghostfreak, Wildmutt), with a short placeholder transition effect. Each alien also has a full 3-ability combat kit (see below). No character art yet — UI and buff icons use placeholder assets.

### Controls

- **T** — open/confirm the Omnitrix dial (see `OmnitrixDialUI` below)
- **Left / Right arrow** — cycle the dial counterclockwise/clockwise (only while the dial is open)
- **R** — cancel the dial while open, or revert to human while transformed
- Attack input (left-click) — Ability 1
- **E** — Ability 2
- **Q** — Ability 3 (every alien shares a generic dash move here by default; see `AlienFormBuff.OnAbility3`)
- All normal weapon/item use is cancelled while transformed (`Ben10Player.PreItemCheck`) — aliens fight with their own powers, not gear.

## Development Commands

**Build the mod:**
```bash
dotnet build
```

This project's `Ben10OmnitrixMod.csproj` imports a workspace-root `../tModLoader.targets` (a sibling of both `Ben10OmnitrixMod/` and `MistbornModOwnBlend/`) rather than depending on tModLoader's `ModSources` directory — this avoids needing tModLoader's in-game "Developer Mode" GUI step. That file hardcodes the local Steam tModLoader install path; if tModLoader is reinstalled or moved, update the `tMLSteamPath`/`TerrariaSteamPath` properties there.

`dotnet build` triggers a *nested* second compile: the `BuildMod` target shells out to `dotnet tModLoader.dll -server -build ...`, which is tModLoader's own internal packaging pipeline that produces the actual `.tmod` file (found after a successful build at `~/Library/Application Support/Terraria/tModLoader/Mods/Ben10OmnitrixMod.tmod` — that's also where tModLoader's real save/mods directory lives on this Mac, confirmed empirically). That nested step needs tModLoader's bundled native libraries (FNA3D, SDL2) on `DYLD_LIBRARY_PATH`/`LD_LIBRARY_PATH`, which the normal `start-tModLoader.sh` launch script sets up but a bare `dotnet build` does not — `tModLoader.targets`'s `BuildMod` target sets these inline in the shell command as a workaround (MSBuild's `Exec` task `EnvironmentVariables` attribute did not reliably take effect on this SDK, so don't reintroduce it there without retesting).

**If tModLoader is running with this mod enabled, CLI builds fail** with `TML003: Please close tModLoader or disable the mod in-game to build mods directly` — the running game locks the `.tmod` file. Close tModLoader before building from the terminal (or use its own in-game "Build + Reload", which doesn't hit this since it's the process holding the lock).

Both `Ben10OmnitrixMod` and `MistbornModOwnBlend` are also symlinked into the real `ModSources` directory (`~/Library/Application Support/Terraria/tModLoader/ModSources/`) so tModLoader's own in-game "Mod Sources" build button works too — that directory's own generated `tModLoader.targets` is a simple absolute-path `<Import>` of `tMLMod.targets` (no native-library-path fix needed there, since an in-game build inherits the already-running game process's environment). Desktop remains the canonical, git-tracked location for both mods; the ModSources copies are symlinks only.

**Format code:**
```bash
dotnet csharpier .
```
Same convention as MistbornModOwnBlend: 4-space indentation, no tabs, `\n` line endings (CSharpier 0.30.6, pinned in `.config/dotnet-tools.json`).

## Project Architecture

### Core Components

**Ben10OmnitrixMod.cs**: Main mod class — registers all keybinds (`TransformKeybind`, `RevertKeybind`, `CycleLeftKeybind`, `CycleRightKeybind`, `Ability2Keybind`, `Ability3Keybind`), handles mod lifecycle, routes multiplayer sync packets.

**Player System (Common/Players/Ben10Player.cs)**: Central transformation state machine — `CurrentForm`/`PendingForm` (single value, enforcing mutual exclusivity by construction, unlike Mistborn's simultaneous-metal-burning model), `OmnitrixEnergy` (shared drain/regen pool), `CooldownTimer` (post-revert lockout), `UnlockedAliens`, `Ability1/2/3Cooldown`. `PreItemCheck` cancels all normal item use while transformed and routes attack input to Ability 1; `ProcessTriggers` routes `Ability2Keybind`/`Ability3Keybind` to Abilities 2/3.

**Buffs (Content/Buffs/)**: `AlienFormBuff` abstract base (mirrors Mistborn's `MetalBuff`) + one thin subclass per alien (`HeatblastBuff.cs` etc.), since buff icon textures resolve once at content-load and can't be swapped dynamically at runtime. All subclasses share one placeholder icon (`PlaceholderBuffIcon.png`) until real art exists. The base class also defines the `OnAbility1`/`OnAbility2`/`OnAbility3` hooks each alien overrides for its kit — `OnAbility3` has a generic default (a forward damaging dash, tinted per-alien) that most aliens just inherit as-is.

**Alien Roster (Content/CustomModType/AlienTypes.cs)**: `AlienType` enum, single source of truth, extension point for future rosters (Ultimate/Omniverse aliens, etc.).

**Alien Data (Common/Data/AlienDefinition.cs)**: Centralized per-alien stat table (one distinguishing passive stat bonus per alien, plus display name/wheel color) — ability damage is calculated as `player.GetDamage(DamageClass.Generic).ApplyTo(baseDamage)`, so each alien's kit scales with its own passive bonus.

**Ability Projectiles (Content/Projectiles/)**: Three shared abstract bases — `AlienBoltProjectile` (fast travelling shot), `AlienNovaProjectile` (stationary AOE burst), `AlienMeleeProjectile` (short-range hitbox that follows the player) — plus `AlienDashBolt` (the generic Ability 3 dash, tint passed via `Projectile.ai[0]` rather than needing a subclass per alien). Each alien gets a thin subclass of Bolt/Nova/Melee per ability that needs bespoke damage/debuff/tint, all sharing `PlaceholderProjectile.png`.

**UI (Common/UI/OmnitrixDialUI.cs)**: Hand-rolled `ModSystem` + `SpriteBatch`. Shows the selected alien large and centered with a ring of position markers around it (the actual show's dial, not a mouse-angle wheel) — Cycle Left/Right rotate the selection, Transform confirms, Revert cancels. No textures required (colored placeholder markers). Mirrors `DraggableMetalUI`'s `ModifyInterfaceLayers` hook pattern.

### Development Patterns

- Enums as the single source of truth for content variants (mirrors `MetalType`).
- Cooldown/timer state lives on `ModPlayer`, never on a `ModBuff` instance (buffs get recreated constantly — a documented pitfall in the sibling mod's bug history).
- Enums saved as strings in `TagCompound`, not ordinals.
- Multiplayer sync designed in from the start via `SyncPlayer`/`SendClientChanges`/`CopyClientState` + a manual `HandlePacket` packet type, rather than retrofitted later.
- Static caches cleared in `Mod.Unload()`.
