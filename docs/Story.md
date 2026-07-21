# Story Bible

**Status: living document, actively being workshopped.** Nothing here is set in stone — expect revisions as design continues. This file exists so the foundation stays consistent across sessions and doesn't get re-derived (or re-argued) from scratch each time. When something here changes, update this file in the same pass so it never goes stale.

## Premise

This is a distinct Ben 10 multiverse — not the mainline-canon Ben. This is *this* universe's own Ben 10. That's a deliberate framing choice: it grants explicit creative license to deviate from show canon on roster, lore, and mechanics without it being "wrong," because it was never claiming to be the original in the first place.

- Target alien roster: ~18–19 aliens, mixing Classic-series and Omniverse designs.
- Aliens that don't fit this take can be cut outright — e.g. Upgrade is a likely cut. Full roster list is still TBD (see Open Questions).
- **Roster count is locked at 18, one alien per boss** — see "Boss → Alien Roster" below for the confirmed boss list and per-alien kit design. No Celestial Pillars or other non-character encounters; every alien's source boss has to actually be a character, not an elemental/construct encounter. (Originally 17, with Eater of Worlds and Brain of Cthulhu sharing one slot; they were later split into two dedicated aliens, landing back in the original ~18–19 target range.)

## Opening Sequence

Delivered as a full-screen panel sequence (see "Panels" below):

1. Ben, Gwen, and Max, in the RV (prebuilt location), talking.
2. A meteor is spotted incoming.
3. A Cannonbolt intercepts the meteor. This Cannonbolt is **future Ben** — a nod/cameo for eagle-eyed players, not a plot thread with mechanical follow-through. Nothing more is built around this; it's just a fun detail.
4. The meteor crashes; a crater forms with the Omnitrix inside.
5. Ben takes the Omnitrix. Gameplay begins.

## Central Conflict

**The antagonist is Moon Lord.** Not a herald, not a stage-one reveal before a "real" villain — Moon Lord himself is the antagonist. He is the one who stole every alien out of the Omnitrix and distributed them across vanilla Terraria's bosses. Defeating Moon Lord is simultaneously vanilla Terraria's normal endgame and this story's climax — one fight serves both purposes.

## Alien Acquisition

Every alien in the roster — **including Alien X, with no exception** — is acquired the same way: by defeating the vanilla Terraria boss holding that alien's essence.

The mechanic is deliberately not a show-canon-style non-destructive DNA scan. In this multiverse, the Omnitrix works differently:

1. Defeating the boss that holds an alien causes a visible creature sprite (representing that alien) to spawn.
2. Ben interacts with it.
3. The Omnitrix **absorbs** the sprite — destructive, consumes it — rather than scanning it non-destructively like the show's Omnitrix.

This is an intentional, explicit deviation from canon, justified by the multiverse framing above.

Unlock scope (decided earlier, still holds): unlocks are **world-shared** in multiplayer — once any player on a server defeats a boss, the alien unlocks for everyone, matching vanilla Terraria's own NPC/shop-unlock convention. Story/dialogue progress, separately, is **per-player**.

## Transformation Mechanics

These rules apply to every alien, on top of whatever's unique in its own kit.

### Per-Alien HP & Mana

Each alien has its own max HP and max Mana. While transformed, these **replace** the player's normal Terraria HP/Mana entirely — not an additive buff, an override. The player's real HP/Mana are **frozen exactly** the moment a transformation begins and are **restored untouched** the moment the player reverts (by any means) — nothing carries over or bleeds through in either direction.

If an alien's HP reaches 0 while transformed, the player is **force-reverted to human**, restored to whatever real HP/Mana were cached at the start of that transformation. This is not player death — the alien form "runs out," not Ben.

**Mana regeneration**: while transformed, Mana regenerates at **5 per 60 ticks (1 second)** — a general rule for every alien, explicitly a placeholder baseline to be tuned once actual playtesting happens, not a final balanced number.

### Azmuth Hardmode Upgrade

At the same moment Azmuth appears (see "Omnitrix Control — Post-Wall of Flesh" below), every alien obtained **before** hardmode starts gets a permanent power upgrade. This does **not** apply to Wall of Flesh's own alien reward or anything obtained after it (Queen Slime onward, including Alien X) — those are designed at hardmode power level from the start, since they're already obtained at or after the hardmode transition.

General formula:

- Max HP: **×2**
- Max Mana: **×2**
- Ability mana costs: **×1.5**
- Ability damage: **×1.5**
- Crit chance: **×2**
- Flight duration (where an alien has any): **×2**
- **Passives have no blanket formula** — each alien's passive upgrade is a specific, individually-decided number, not derived from a multiplier. (Goop is the first example: +100 Defense → **+175 Defense**, not the +200 a strict double would give.)
- An alien's Ability 3 can also get a **qualitative behavior change** instead of/alongside numeric scaling, decided per alien (Eye Guy's dash entirely stops dealing damage post-upgrade and becomes a pure flight toggle instead — see his entry below).
- The shared dash's **2-second cooldown does not change** at the upgrade, and does not vary per alien either — it's a strict constant across the whole roster, no exceptions.

Any given alien's damage/passive/ability-behavior progression is still individually discussed and locked per alien as we design them — this formula is the default starting point, not a rule that overrides specific per-alien decisions already made.

### Omnitrix Control — Early Game (Pre-Wall of Flesh)

Before Wall of Flesh is defeated, the Omnitrix is deliberately rigid, mirroring the original show's season-1 Omnitrix (no manual shutoff):

- A transformation always runs for a **fixed 20 seconds**, regardless of which alien. There is no manual revert during this window — the player is locked in for the full duration.
- After reverting (the 20 seconds elapsing, or a forced revert from alien HP hitting 0), a flat **60-second recharge lockout** applies before the player can transform into anything again.
- **Inventory access is fully locked while transformed** — the player cannot open the inventory screen at all, not just restricted from using items (which is already true via `PreItemCheck`).

### Omnitrix Control — Post-Wall of Flesh (Azmuth)

Defeating Wall of Flesh is a story turning point, not just a boss kill:

- **Azmuth** (the Galvan who created the Omnitrix — same species as Grey Matter) appears via a one-time, non-persistent panel immediately after the kill. He does not spawn as a persistent world NPC; he shows up once, delivers the moment, grants the upgrade, and that's it.
- This grants the player a **permanent, unremovable buff** marking the Omnitrix as unlocked — visible in the buff bar, cannot be cleared or cancelled.
- From this point on, the rigid 20s/60s system is replaced entirely: **the Omnitrix becomes a continuous energy meter** — it drains while transformed and regenerates while human, at some rate (exact drain/regen numbers still TBD — see Open Questions). The player can revert at any time, and can transform again as soon as there's enough charge, rather than waiting out a fixed lockout.
- **Inventory access is unlocked** from this point on — the pre-upgrade full inventory lock is lifted at the same moment as everything else Azmuth grants.

### Shared Dash (Ability 3 base template)

Ability 3 is a real, universal shared ability for **every** alien, no exceptions — it is never repurposed into something else (like a flight toggle) for any specific alien. See "Flight" below for how aliens that can fly actually do so — it's a separate system entirely, not tied to Ability 3.

- **2-second cooldown, always** — a strict constant for every alien. (Eye Guy's dash was briefly discussed at 4 seconds during design and was corrected back to the universal 2 seconds; his dash was also briefly redesigned as a flight toggle and that has since been reverted too — see his entry below.)
- **Every alien's dash deals damage, always** — never purely a mobility tool, though the damage amount is tuned per alien (e.g. Goop's is intentionally very low, 8–12, since his dash leans on utility/defense rather than damage).
- Grants **full invulnerability** for the duration of the dash, by default.
- Trajectory (arcing vs. straight-line) and whatever the dash visually reskins as are both per-alien.
- Can be **chargeable** — holding before releasing extends/modifies the dash. What the charge actually does is unique per alien (e.g. Goop's charge makes the dash go further).

### Flight (Native Mechanic)

Aliens that can fly do so using **Terraria's native wing-flight input** — hold jump/spacebar while airborne to fly, which consumes a flight-time budget, and touching the ground recharges it back to full. This is completely separate from Ability 3/the dash; flight is never toggled through the dash key.

- Each flying alien has its own max flight-time budget (seconds), covered by the general Azmuth Hardmode Upgrade's **×2 flight duration** rule like any other stat.
- How generous that budget is depends entirely on how central flight is to that alien's identity — e.g. Eye Guy (flight is a bonus trait) gets a short, real budget; Stinkfly (flight is his whole identity) gets an effectively unlimited one. Decided per alien, not a single shared number.

## Boss → Alien Roster

The confirmed 18-boss progression backbone (pre-hardmode through Moon Lord), one alien per boss — Eater of Worlds and Brain of Cthulhu each got their own dedicated alien rather than sharing one slot, bringing the total to 18:

1. King Slime → **Goop** (Locked — see kit below)
2. Eye of Cthulhu → **Eye Guy** (Locked — see kit below)
3. Eater of Worlds → **Armodrillo** (Locked — see kit below)
4. Brain of Cthulhu → **Brainstorm** (Locked — see kit below)
5. Queen Bee → **Stinkfly** (Locked — see kit below)
6. Skeletron → **Ghostfreak** (Locked — see kit below)
7. Deerclops → TBD
8. Wall of Flesh → TBD (**this kill also triggers the Azmuth story beat** — see "Transformation Mechanics" above; the alien unlock itself is separate from that)
9. Queen Slime → TBD
10. The Twins → TBD
11. The Destroyer → TBD
12. Skeletron Prime → TBD
13. Plantera → TBD
14. Golem → TBD
15. Duke Fishron → TBD
16. Empress of Light → TBD
17. Lunatic Cultist → TBD
18. Moon Lord → Alien X (implicit — Moon Lord is both final boss and antagonist; see "Alien X" section below for why his acquisition is otherwise unremarkable)

Every alien gets a 3-ability kit (Ability 1 = left-click, Ability 2 = E, Ability 3 = Q — matches the existing framework's convention) plus a passive, and its own HP/Mana pool (see "Transformation Mechanics" above). Ability 1 defaults to a flat-direct-damage hit unless a specific alien's kit genuinely doesn't fit that shape.

### 1. King Slime → Goop (Locked)

- **Stats**: 140 HP, 200 Mana pre-upgrade → 280 HP, 400 Mana post-Azmuth-upgrade (general ×2 rule).
- **Passive**: +100 Defense pre-upgrade → **+175 Defense post-upgrade** (explicit exception — not the +200 a strict double would give).
- **Ability 1 (left-click) — Goop Ball**: 150 ranged damage, costs 15 mana, 90-tick (1.5s) cooldown, arcing/lobbed projectile trajectory, 15% crit chance pre-upgrade → 225 damage, ~23 mana, 30% crit post-upgrade (general ×1.5 damage/cost, ×2 crit rules; cooldown unchanged). Confirmed as formula-default, no exception.
- **Ability 2 (E)** — arm-stretch guillotine sweep: 150 melee damage, −25% damage per additional enemy hit in the same swing (so roughly 150 / 112 / 84 / ... against multiple targets) pre-upgrade → 225 base damage post-upgrade (same ×1.5 rule, falloff rate unchanged); visual effect colored to match his arm color. Confirmed as formula-default, no exception.
- **Ability 3 (Q)** — shared dash (see base template above): 8–12 damage pre-upgrade → 12–18 post-upgrade (×1.5), 2s cooldown unchanged; his charge effect makes the dash go further. Confirmed as formula-default. **Artistic note**: the dash plays as an arcing leap/lunge (not a straight-line or rolling-along-the-ground motion) — Goop balls up into a goop-ball shape and lunges through that arc. Important to get this trajectory right visually whenever the animation/art pass happens.

### 2. Eye of Cthulhu → Eye Guy (Locked)

- **Stats**: 100 HP, 220 Mana pre-upgrade → 200 HP, 440 Mana post-upgrade.
- **Passive**: +15% crit chance pre-upgrade → +30% post-upgrade (general ×2 crit rule).
- **Ability 1 (left-click) — Eye Beam**: hitscan beam (straight-line, not lobbed), 120 damage, 10 mana, 45-tick (0.75s) cooldown pre-upgrade → 180 damage, 15 mana post-upgrade (cooldown unchanged). Additionally scales with active summoned eyes (see Ability 2): **+15% damage per active eye, cumulative** — so with all 3 eyes active post-upgrade, that's +45% on top of the above.
- **Ability 2 (E) — Eye Summon**: summons eye(s) that hold a static position above the player (matching player movement, not seeking targets independently), invulnerable, firing on their own. 1 eye pre-upgrade, dealing 45 damage every 60 ticks (1 sec) → 3 eyes post-upgrade, each dealing 90 damage every 60 ticks. Costs 20 mana to activate; once active, the summon lasts until the player reverts out of Eye Guy (no separate duration timer); pressing Ability 2 again while already active does nothing (not a refresh, no extra cost).
- **Ability 3 (Q)** — shared dash: straight-line (not arcing), invulnerable, 15 damage, 2s cooldown pre-upgrade → ~23 damage post-upgrade (general ×1.5 rule), cooldown unchanged. *(Superseded an earlier design where this became a flight toggle post-upgrade — reverted once flight moved to the native fly-key mechanic below; the dash is a normal damage dash for him at all times.)*
- **Flight**: separate from Ability 3 — uses the native fly-key mechanic (see "Flight" under Transformation Mechanics). A real but short budget, since flight is a bonus trait for him rather than his core identity: 1.3 seconds pre-upgrade → 2.6 seconds post-upgrade (general ×2 flight rule).

### 3. Eater of Worlds → Armodrillo (Locked)

The Talpaedan species — canonical burrowing/tunneling specialist (drill-arms, digs through solid rock at speed), chosen over an earlier Wildvine pick specifically because burrowing is Eater of Worlds' most central, immediately recognizable trait in the actual fight (constantly tunneling in and out of blocks), stronger than Wildvine's segment-splitting parallel.

- **Stats**: 160 HP, 180 Mana pre-upgrade — highest HP on the roster so far, leaning fully into "tank," with lower Mana reflecting a physical brawler rather than an ability-spam kit.
- **Passive**: Immune to knockback — sheer bulk/sturdiness, a boolean immunity rather than another numeric stat (distinct from Goop's flat defense and Eye Guy's crit chance).
- **Ability 1 (left-click) — Drill Punch**: melee, 130 damage, 12 mana, 60-tick (1s) cooldown. First melee-default Ability 1 on the roster (Goop/Eye Guy/Brainstorm are all ranged) — gives him a distinct close-range identity.
- **Ability 2 (E) — Seismic Slam**: ground-pound AOE, damages and knocks back all nearby enemies (notable since he himself is immune to knockback), 140 damage, 25 mana, 150-tick (2.5s) cooldown.
- **Ability 3 (Q) — Tunnel Dash**: shared dash reskinned as briefly burrowing underground and re-emerging further along — the dash's invulnerability doubles as literally being underground and untouchable. 15–20 damage, 2s cooldown (universal). Charge effect: emerging with a small shockwave burst dealing bonus AOE damage on arrival (rather than reusing Goop's "goes further").

### 4. Brain of Cthulhu → Brainstorm (Locked)

The Cerebrocrustacean species — crab-like body with a large exposed brain, electrokinesis and telekinesis. A near-literal match for Brain of Cthulhu (a giant floating exposed brain). Brainstorm has his **own unique hardmode-upgrade profile**, deviating from the general Azmuth formula in several places — noted inline below.

- **Stats**: 120 HP, 230 Mana pre-upgrade → 240 HP, 460 Mana post-upgrade (general ×2 rule).
- **Passive**: Mana regenerates at double the general baseline while transformed as him (10 per 60 ticks instead of 5) — represents his ability to generate/absorb electrical energy. Post-upgrade: ×1.5 **(alien-specific — the general formula has no blanket passive multiplier)** → 15 per 60 ticks.
- **Ability 1 (left-click) — Lightning Bolt**: 100 damage to primary target, chains to one additional nearby enemy for 50% (50) damage, 12 mana, 60-tick (1s) cooldown pre-upgrade → 200/100 damage (alien-specific ×2, not the general ×1.5), mana cost 18 (general ×1.5 default — not called out as an exception, flag if wrong), cooldown 66 ticks (alien-specific ×1.1).
- **Ability 2 (E) — Telekinetic Nova**: pulse around him, damages and knocks back all nearby enemies, 120 damage, 20 mana, 150-tick (2.5s) cooldown pre-upgrade → 240 damage (×2), 30 mana (general ×1.5 default), 165-tick cooldown (×1.1).
- **Ability 3 (Q)** — shared dash, reskinned as a short telekinetic teleport/blink (burst of electric arcs) rather than a physical dash: 12–15 damage, 2s cooldown pre-upgrade, charge effect leaves a residual damaging electric field at the departure point. Post-upgrade: 24–30 damage (×2), base teleport distance increases, cooldown 132 ticks / 2.2s (×1.1 — **an explicit exception to the "dash is always 2s, no exceptions" rule** written into the shared dash template above; noting the contradiction here deliberately rather than silently overriding it).

### 5. Queen Bee → Stinkfly (Locked)

The Lepidopterran species — a dragonfly/firefly-coded flying insectoid, one of the original 10 aliens from this project's pre-redesign standalone roster, kept on for the boss-tied roster too. Not a literal "bee" match (no canonical bee-specific Ben 10 alien exists) — picked as the strongest available flying-insect analog, and as a deliberate nod to the original 10.

- **Stats**: 110 HP, 210 Mana pre-upgrade — agile/evasive rather than tanky, since flight and evasion already give him a defensive edge.
- **Passive**: **Stinky** — enemies are less likely to target him (an aggro/threat-reduction effect), not damage mitigation or dodge. Distinct from every other locked alien's passive so far.
- **Ability 1 (left-click) — Goo Spit**: ranged, 110 damage, applies a brief slow debuff on hit (−20% move speed, ~2 sec), 10 mana, 50-tick cooldown.
- **Ability 2 (E) — Goo Bomb**: lobbed projectile, 100 damage on impact, leaves a sticky patch on the ground that continues slowing anything standing in it afterward — area denial, distinct from Ability 1's single-target poke.
- **Ability 3 (Q) — Goo-Slick Dash**: shared dash, real damage like every alien's (8–12, Goop's range, since he's agile/utility rather than a hard hitter), invulnerable, 2s cooldown. Leaves a lingering slick/sticky trail that slows enemies who walk through it afterward, tying into his goo identity.
- **Flight**: separate from Ability 3 — native fly-key mechanic, **effectively unlimited** flight-time budget. Flying is his core identity, not a bonus trait like Eye Guy's, so unlike Eye Guy he isn't meaningfully budget-constrained at all, pre- or post-upgrade.

### 6. Skeletron → Ghostfreak (Locked)

The Ectonurite species — intangible, invisible, capable of possession, deliberately picked over Frankenstrike (a closer literal "reanimated construct" analog) since it continues the "reuse an original 10 alien" pattern (like Stinkfly) and gives the strongest mechanically-distinct kit. Like Stinkfly, flight is core to his identity — **effectively unlimited flight-time budget**, no meaningful constraint pre- or post-upgrade, same treatment as Stinkfly rather than Eye Guy's limited version. He never walks.

- **Stats**: 115 HP, 220 Mana pre-upgrade.
- **Passive**: 15% chance to turn briefly intangible when hit, negating that hit entirely → **triples to 45% on Azmuth** (alien-specific exception, not the general no-blanket-formula default).
- **Ability 1 (left-click) — Shadow Slash**: melee (not ranged — corrected from an earlier draft for canon accuracy), 115 damage, 10 mana, 55-tick cooldown. Confirmed, no change from the earlier ranged-bolt draft's numbers.
- **Ability 2 (E) — Possession**: briefly take control of a non-boss enemy's body. Full piloting access to whatever that creature can natively do (attacks, movement, everything). Deals tick damage to the possessed creature the whole time: 15 damage per 30 ticks (0.5s) pre-upgrade → 120 damage per 60 ticks (1s) on Azmuth (a real per-second rate jump, ~30/sec → ~120/sec, not a clean multiplier — alien-specific, taken as intentional). Lasts 3 seconds pre-upgrade → 7.5 seconds on Azmuth (×2.5, alien-specific), cancelable at will. If the possessed creature dies while you're piloting it, heal 25% of Ghostfreak's own max HP pre-upgrade → 75% post-upgrade (percentage of his max HP, not a hardcoded number, so it scales automatically with his HP tuning). 115 mana to cast — a deliberately hefty cost, roughly half his pre-upgrade Mana pool, flagged as a placeholder to tune during actual playtesting. 15-second cooldown, unchanged at Azmuth (no scaling on the delay itself). **Documented fallback if possession turns out technically infeasible to build**: replace with an AOE scare effect instead, flavored as him tearing open his chest to reveal what's underneath (a classic show beat), frightening nearby enemies.
- **Ability 3 (Q) — Phase Dash**: shared dash, full intangibility including phase-through-solid-objects; if the dash would end with him still inside a block, he keeps getting pushed forward automatically until clear of it (prevents getting stranded inside terrain). 12–15 damage on emergence, 2s cooldown (universal). Charge effect: extends how long he stays intangible/invisible after the dash ends, a lingering stealth window. Confirmed.

## Alien X

**Acquisition is unremarkable** — same as every other alien: defeat the boss holding him (implicitly Moon Lord, as the final boss and antagonist), sprite spawns, Omnitrix absorbs it, done. **There is no coin-flip ritual to receive Alien X.** This was an early misunderstanding in design discussion and has been corrected — noting it here explicitly so it doesn't recur.

**Using Alien X is where he's unusual.** Once transformed into him:

- He starts fully inert — cannot do anything at all by default.
- Every *distinct action category* (movement, jump, fly, ability 1, ability 2, ability 3, etc.) is individually locked.
- Attempting a locked action triggers a **three-way coin flip** (heads/tails, not dice) between Ben and his two other heads. All three must land the same face for that action to unlock.
- Once an action category is unlocked, it's **permanent for that save file** — reverting to human and transforming back into Alien X later does not re-lock anything already earned.
- This applies per action *category*, not per use — e.g. once movement is unlocked, you don't re-flip every time you move.

**Alien X's kit (3 abilities, like every alien):**

- **Ability 1 — Block/recipe browser.** Full creative-mode-style access to browse and use all blocks, in the spirit of the existing `Cheat Sheet` tModLoader mod. (Cheat Sheet's license should be checked before using it as a technical reference — separate task, not yet done.)
- **Ability 2 — Reset World.** Deletes and regenerates the *current* world in place, same name and seed. **Singleplayer-only.** In multiplayer, this specific coin-flip can never resolve to a match — and this must be implemented as a hard, explicit, unconditional check ("if multiplayer, this action's flip always fails," full stop), not merely very-improbable odds, since probability alone is not a real guarantee no matter how small. The in-fiction explanation ("you could never land all the same face together in a co-op world") should match an actual code-level guarantee.
- **Ability 3 — TBD / work in progress.** Deliberately left open for now.

Alien X is meant to feel broken and cheeky once unlocked — the friction of earning each action is the point, not an oversight.

## Post-Game: Anihilaarg

A secret superboss, available after Alien X has been acquired. Anihilaarg is Alien X's canonical Nemetrix predator (the natural predator of Celestialsapiens in the source material). It appears for **pure predator-instinct reasons — purely because Alien X now exists in this world.** No connection to Moon Lord or the main antagonist plot.

## Parked Ideas (not dropped, not active)

- **Carnotrix twist**: the idea that the Omnitrix itself might secretly be evil, revealed as a late-game twist. Shelved for now; may resurface later. Do not build anything assuming this is active.

## Panels (Cutscene System)

Story beats are delivered as **full-screen, blocking panels** — not a smaller inset while gameplay continues. Visual-novel style: a full-screen image with a dialogue text box, advances on player input, simulation paused while a panel sequence is active.

Art production: panels are created by screenshotting the actual Terraria world and compositing additional art over it in post-production — not rendered in-engine. Placeholder plan for these (needed for development before final art exists) is still TBD.

## UI Direction

Keep the existing left/right cycle Omnitrix dial as-is. No redesign of the core selection UI.

### While Transformed

- **The alien's name displays where the held item's name would normally show** — that UI slot gets repurposed while transformed, rather than showing nothing or the (unusable, since item use is cancelled) equipped item's name.
- **Inventory access follows the pre/post-Wall-of-Flesh split** — see "Omnitrix Control" under Transformation Mechanics: fully locked pre-upgrade, unlocked post-upgrade.

### Omnitrix Battery Indicator

A visual charge/time indicator, part of the Omnitrix dial UI (in the middle of that display, roughly below the Mana display by default):

- Default look: a green hourglass that ticks down.
- Flashes red when charge is low.
- Fully configurable: resizable via in-game mod config settings, repositionable via right-click-drag in-game.
- Multiple visual styles supported, at least: the stylized hourglass, and a simpler plain-green bar style.
- Needs to represent whichever system is currently active — a literal countdown pre-Wall-of-Flesh (fixed 20s), and current charge level post-upgrade (continuous energy meter) — same indicator, different underlying meaning depending on which Omnitrix Control phase the player is in.

## Tone

Easter eggs and nods throughout, in the spirit of the future-Ben Cannonbolt cameo in the opening.

## Open Questions (not yet resolved)

- Remaining 12 boss→alien pairings and kits (Deerclops through Lunatic Cultist — see "Boss → Alien Roster" above for progress), plus Alien X's own kit numbers (his third ability is still open too — see "Alien X" above).
- Ghostfreak's Possession (Ability 2) has a real technical feasibility risk (piloting an NPC's AI isn't free in tModLoader) — a fallback AOE scare effect is documented in his entry if it doesn't pan out.
- Exact drain/regen rate for the post-Wall-of-Flesh continuous Omnitrix energy meter (the *model* is locked — see "Transformation Mechanics" — but not yet the actual numbers). Note this is separate from the per-alien Mana regen rate (5/60 ticks), which is resolved as a placeholder.
- Gwen and Max's role beyond the opening RV scene — recurring companions/NPCs, or a one-scene appearance?
- Panel placeholder-art plan for development before final art exists, and the technical format for panel content (image + text data, how a panel sequence is authored/triggered).
- Whether Carnotrix stays parked permanently or gets revisited.
