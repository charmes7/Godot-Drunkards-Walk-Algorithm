# Drunkard's Walk Dungeon Generator (C#)

## This project uses a Drunkard's Walk Algorithm to create randomized, fully connected 2D tilemaps within the Godot Engine.

---

## Screenshots
| Random Generation A | Random Generation B |
| :---: | :---: |
| <img src="https://github.com/user-attachments/assets/cb5b74b1-89bf-421a-bfaa-773546cf0010" width="600" /> | <img src="https://github.com/user-attachments/assets/726d8b13-99b2-4860-9c21-f4f457a7ce1c" width="600" /> |

---

## What I learnt
### 1. Data Structures & Time Complexity O(1)
For memory efficiency, I used a **HashSet<Vector2I>** for storing the coordinates, allowing for O(1) time complexity during spatial lookups. As the dungeon size scales, the checking for existing floor tiles remain instant.
### 2. Clean Code & Modular Design
I split the dungeon generation into distinct functions and ensure the code remains readable and easy to debug. I also learnt to separate the Data Logic (coords) and Rendering Logic (drawing tiles).

---

## Tech Stack
* **Engine:** Godot 4.6.1
* **Language:** C# (.NET)

## How to Run
1. **Clone** this repository.
2. **Open** the project in Godot 4
3. **Press F5** (Run) and click the **"Generate Dungeon"** button to trigger a new layout.

## Credits
Tile Art Asset by Fedor Kochikov
https://xvideosman.itch.io/cave-tileset
