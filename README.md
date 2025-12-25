# Devour the Earth

**Game link:** https://ecse-csds290.itch.io/team9-s2025

**Trailer video:** https://youtu.be/e8B69nNBfKg
[![Watch the video](https://img.youtube.com/vi/e8B69nNBfKg/maxresdefault.jpg)](https://youtu.be/e8B69nNBfKg)

*Devour the Earth* is a Space Invaders-inspired shooter built in Unity that focuses on persistent progression through failure. Players control disposable hive mind entities, assimilating enemy ships across runs to overwhelm an escalating planetary defense system gradually.

## Core Systems & Mechanics

In *Devour the Earth*, each run (invasion) is a single, expendable hive mind entity attempting to break through Earth's defenses. Individual entities are inherently weak, so failure is expected. The primary objective during an invasion is to damage the Earth by firing at it while surviving waves of defending ships. Earth's health is represented by a global HP bar.

### Assimilation

During each invasion, enemy ships, when destroyed, occasionally drop fragments, which can be collected and sent back to the hive mind base. These fragments are assimilated between invasions and converted into minions that persist across runs.

Before starting a new invasion, players can drag assimilated minions into a radar-like area. These minions will be deployed in the invasion to assist the next hive mind entity during combat. Once destroyed, minions are removed from the Hub to encourage experimentation and risk management.

### Enemy Phases

Earth's defenses escalate based on remaining HP. Every 25% reduction, including the start, introduces a new enemy type:

#### Basic Enemies 
Standard projectile-based units (with two variants).

![Basic enemy attack](https://github.com/user-attachments/assets/4a841089-54a1-4d8e-a6d5-380f7cf67321)

#### Kamikaze Enemies
High-speed, homing units that self-destruct on contact.

![Kamikaze enemy attack](https://github.com/user-attachments/assets/1780b660-1b7f-4d99-ad02-66f5617ffe73)

#### Electric Enemies
Area-control units that shoot electrical orbs, stunning any entities they hit.

![Electric enemy attack](https://github.com/user-attachments/assets/c6a63ad4-e4ec-4b70-9f07-1ea010d893d6)

#### Laser Enemies
Precision-based units that charge high-damage laser attacks.

![Laser enemy attack](https://github.com/user-attachments/assets/c996e041-54a3-4d6a-b2d9-ce6d59c8b5ff)

(The gif does not represent the true damage of the enemies, as the player's health is increased by 3 - 4 times for recording purposes)

### Minion Control

Different minion types require different player inputs to activate their abilities. **Basic** and **Electric** minions fire using the player shooting input (left mouse click), **Kamikaze** enemies launch when the '1' key is pressed, and **Laser** enemies charge while holding the spacebar and fire upon release.

This input differentiation gives each minion type a distinct tactical role and forces players to make active decisions during combat.

## Technical Notes

![Unity 6](https://img.shields.io/badge/Engine-Unity%206-black)
![C#](https://img.shields.io/badge/Language-C%23-blue)
![Platform](https://img.shields.io/badge/Platform-WebGL%20%7C%20Windows-green)

Due to a WebGL limitation, the electric enemy's orb VFX does not render correctly on itch.io. Hence, the Windows executable is recommended.

## Lessons Learned

- Designed a fail-to-win progression system, centered around the assimilation mechanic
- Implemented acceleration-based, space-like movement to achieve smooth yet slippery movement
- Balanced player, minion, and enemy strengths to maintain a consistent difficulty
- Built a state-based enemy AI system that idles around the Earth and chases or attacks players and minions accordingly
- Learned that the hierarchical enemy/minion class structure (base entity => enemy/minion => specific types) significantly simplifies logic term logic

## Contributors

- [@nirow1010](https://github.com/nirow1010) - player movement design, entity class hierarchy, and enemy/minion ability design & implementation (including particles and VFX)
- [@Aisuirocellist](https://github.com/Aisuirocellist) - main theme composition, sound effects, menu UI, and CONTROLS scene
- [@James-275](https://github.com/James-275) - assimilation system, enemy AI & pathfinding, and minion auto-targeting
- [@TheSpoon7784](https://github.com/TheSpoon7784) - enemy/minion sprites, health bars, and enemy spawn design

Each member contributed support across other areas as needed, as well as to the game documentation and core concept.

## Special Thanks to:

- [Foozle - Itch.io](https://itch.io/c/2713136/void) - Void ship collection
- [JIK-A-4. Itch.io](https://jik-a-4.itch.io/freepixel) - Space Pixel Art Asset Pack
- [Deep-Fold - Itch.io](https://deep-fold.itch.io/space-background-generator) - Pixel Space Background Generator
- [BDragon1727 - Itch.io](https://bdragon1727.itch.io/basic-pixel-health-bar-and-scroll-bar) - Basic Pixel Health bar and Scroll bar
- [Unity Technologies - Unity Asset Store](https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325?srsltid=AfmBOorUvxGWc_Orc7YUt4505scjlHTOX9dPqB1CNnUJddw99QUadq82) - Particle Pack | VFX Particles
