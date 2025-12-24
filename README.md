# Devour the Earth

**Game link:** https://ecse-csds290.itch.io/team9-s2025

**Trailer video:** https://youtu.be/e8B69nNBfKg
[![Watch the video](https://img.youtube.com/vi/e8B69nNBfKg/maxresdefault.jpg)](https://youtu.be/e8B69nNBfKg)

*Devour the Earth* is a Space Invaders-inspired shooter built in Unity that focuses on persistent progression through failure. Players control disposable hive mind entities, assimilating enemy ships across runs to overwhelm an escalating planetary defense system gradually.

## Core Systems & Mechanics

In *Devour the Earth*, each run (invasion) is a single, expendable hive mind attempting to break through Earth's defenses. Individual entities are inherently weak, so failure is expected. The primary objective during an invasion is to damage the Earth by firing at it while surviving waves of defending ships. Earth's health is represented by a global HP bar.

### Assimilation

During each invasion, enemy ships, when destroyed, occasionally drop fragments, which can be collected and sent back to the hive mind base. These fragments are assimilated between invasions and converted into minions that persist across runs.

Before starting a new invasion, players can drag assimilated minions into a radar-like area. These minions will be deployed in the invasion to assist the next hive mind entity during combat. Once destroyed, minions are removed from the Hub to encourage experimentation and risk management.

### Enemy Phases

Earth's defenses escalate based on remaining HP => every 25% reduction introduces a new enemy type:

#### Basic Enemies 
Standard projectile-based units (with two variants).
![Basic enemy attack](https://github.com/user-attachments/assets/4a841089-54a1-4d8e-a6d5-380f7cf67321)

#### Kamikaze enemies
High-speed, homing units that self-destruct on contact.

#### Electric enemies
Area-control units that shoot electrical orbs, stunning any entities they hit.

#### Laser enemies
Precision-based units that charge high-damage laser attacks.

### Minion Control



## Technical Notes
- Built in **Unity**
- Target platforms: **WebGL** and **Windows**
- Due to a WebGL limitation, the electric enemy's orb SFX visual effect does not render correctly on itch.io. Hence, the Windows executable is recommended.

## Lessons Learned



## Special Thanks to:
- Foozle - Itch.io - Void ship collection
- JIK-A-4. Itch.io - Space Pixel Art Asset Pack
- Deep-Fold - Itch.io - Pixel Space Background Generator
- BDragon1727 - Itch.io - Basic Pixel Health bar and Scroll bar
- Particle Pack | VFX Particles | Unity Asset Store
