# 🏰 BeneathArcantum

**SkillsUSA 2025 — Game Development Competition Entry**

A 3D action-platformer built in Unity. Explore ancient ruins, solve puzzles, fight enemies, and uncover what lies beneath.

---

## 🎮 What This Is

Built for the SkillsUSA game development competition. BeneathArcantum is a fully playable 3D game featuring multiple levels, enemy AI, puzzle mechanics, and a full game loop — from starting cutscene to level completion.

### Built in Unity 2021.3.42f1 with C#

---

## 🛠️ What I Built

### Core Systems

| System | What It Does |
|--------|-------------|
| **Player Controller** | Full 3D movement, jumping, attacking, health system, animations |
| **Enemy AI** | NavMesh-based patrol, sight/attack range detection, damage system |
| **Game Manager** | Level management, checkpoints, death screen, pause, level completion |
| **Camera System** | Top-down and follow camera modes, smooth transitions |

### Mechanics

| Mechanic | Scripts |
|----------|---------|
| 🧩 **Puzzles** | Pressure plates, wire matching, breakable floors |
| 🚪 **Doors & Platforms** | Moving platforms, door opening, climbable walls, ladders |
| ⚔️ **Combat** | Melee attacks, throw attacks, weapon pickup, damage cooldown |
| ❤️ **Health** | Health bar UI, healing items, death/respawn, checkpoints |
| 🎬 **Cutscenes** | 10 typewriter text effect variants for dialogue sequences |
| 🏁 **Progression** | Checkpoints, end points, level completion screen |

### Levels (12 scenes)

| Scene | Type |
|-------|------|
| `StartingCutScene` | Opening cinematic |
| `GameMenuProto1` | Main menu |
| `BunkerLevel` | Full level |
| `CaveScene` | Full level |
| `CliffScene` | Full level |
| `PrototypeLevels/` | 3 prototype/test levels |
| `Sandbox` | Testing ground |
| `Testing`, `CameraSysTest` | Development test scenes |

---

## 📊 By The Numbers

| Metric | Count |
|--------|-------|
| Custom C# scripts | 53 |
| Game scenes | 12 |
| Asset files | 844+ |
| Lines of C# | 3,000+ (estimated) |
| PlayerController | 382 lines |
| Typewriter text effects | 10 variants |

---

## 🏆 SkillsUSA Context

This was built for the SkillsUSA game development competition. The requirements pushed me to build a complete game loop — not just a demo, but start-to-finish gameplay with mechanics, UI, progression, and polish.

The commit message "game done?" wasn't uncertainty about the build — it was the moment I realized I'd actually shipped a full game on a competition deadline.

---

## 🔧 How To Open

1. Install Unity Hub + Unity 2021.3.42f1
2. Clone this repo
3. Open `BeneathArcantum/` as a Unity project
4. Open `Assets/Scenes/StartingCutScene.unity` to start from the beginning
5. Press Play

---

## 🎯 What I'd Do Differently

- Add screenshots to this README (coming soon)
- Refactor the 10 TypeWriterEffect scripts into one parameterized class
- Add a proper build to the Releases section
- Clean up test scenes (Sandbox, Testing)

---

## 📂 Project Structure

```
BeneathArcantum/
├── Assets/
│   ├── Scenes/           # 12 game scenes
│   ├── Scripts/          # 53 C# scripts
│   │   ├── PlayerController.cs    # Core player (382 lines)
│   │   ├── EnemyController.cs     # Enemy AI (135 lines)
│   │   ├── GameManager.cs         # Game state (132 lines)
│   │   └── ...                    # 50+ more scripts
│   └── TextMesh Pro/     # UI text rendering
├── ProjectSettings/      # Unity project config
└── macOS build/          # Pre-built macOS executable
```

---

*Built by Sean Jenkins — SkillsUSA 2025*
*"[Game done?](https://github.com/SeanJ07/BeneathArcantum/commit/ff5a0e2)" — yeah, I think so.*
