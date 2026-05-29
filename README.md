# Beneath Arcantum

**4th place — SkillsUSA State Game Development Competition, 2025**

A 3D action-platformer built in Unity over the course of my senior year. Dungeon crawler meets platformer — you move through underground environments, fight enemies, solve puzzles, and try not to fall into things.

This was the first project of **Hourhand Studios**, a team I assembled through SkillsUSA. We went from concept to a complete, submittable game in one competition cycle. Some mechanics are rough around the edges. 4th place in states anyway.

**Built with:** Unity 2021.3.42f1 · C# · NavMesh AI · TextMesh Pro

---

### How to Run

1. Install [Unity Hub](https://unity.com/download) and Unity **2021.3.42f1**
2. Clone the repo
3. Open `BeneathArcantum/` as a Unity project
4. Open `Assets/Scenes/StartingCutScene.unity`
5. Hit Play

---

### What We Built

**Core systems**

| System | Description |
|---|---|
| Player Controller | Movement, jumping, attacking, health, animations — 382 lines |
| Enemy AI | NavMesh patrol, sight/attack range detection, damage |
| Game Manager | Level flow, checkpoints, death screen, pause, completion |
| Camera System | Top-down and follow modes with smooth transitions |

**Mechanics**

Pressure plates, wire puzzles, breakable floors, moving platforms, climbable walls, ladders, melee + throw combat, weapon pickups, healing items, checkpoints, and 10 typewriter text effect variants for cutscene dialogue.

**Scenes (12 total)**

`StartingCutScene` → `GameMenuProto1` → `BunkerLevel` → `CaveScene` → `CliffScene` + prototype and test scenes

---

### By the Numbers

| | |
|---|---|
| C# scripts | 53 |
| Game scenes | 12 |
| Asset files | 844+ |
| Lines of C# | ~3,000 |
| PlayerController.cs | 382 lines |
| Typewriter variants | 10 |

---

### Project Structure
BeneathArcantum/
├── Assets/
│   ├── Scenes/         # 12 scenes
│   ├── Scripts/        # 53 C# scripts
│   │   ├── PlayerController.cs
│   │   ├── EnemyController.cs
│   │   ├── GameManager.cs
│   │   └── ...
│   └── TextMesh Pro/
├── ProjectSettings/
└── macOS build/        # Pre-built macOS executable

---

*Built by Sean Jenkins and Hourhand Studios — SkillsUSA 2025.*  
*"[Game done?](https://github.com/SeanJ07/BeneathArcantum/commit/ff5a0e2)" — yeah, I think so.*
