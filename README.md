# Bongo Empire - MVP

This package contains the core code and assets for the Bongo Empire MVP. Drop the `Assets/` folder into a Unity 2D project (recommended Unity 2022/2023 LTS) and open the `Main` scene or create one following the instructions below.

## What's included
- C# scripts for core systems: GameManager, ResourceSystem, BuildingSystem, MissionSystem, UIManager, WalletManager.
- ScriptableObject templates and example placeholders.
- A BONGO character image if attached to this chat (check Assets/Art/).
- README and .gitignore/.gitattributes for Git LFS usage.

## How to use
1. Create a new Unity 2D project (LTS recommended).
2. Copy the `Assets/` folder from this package into your Unity project's root.
3. In Unity, create an empty Scene named `Main` (or import the sample scene if you create one).
4. Create GameObjects for GameManager and systems (see the earlier chat for exact setup) and attach scripts.
5. Create BuildingData ScriptableObjects (right-click -> Create -> BongoEmpire -> BuildingData) and assign into BuildingSystem.buildingCatalog.
6. Create a BuildingPrefab using a SpriteRenderer and attach BuildingPrefabController.
7. Hook UI TextMeshPro fields to UIManager and wire buttons to UIManager.OnPlaceBuildingButton(index).

## Notes
- This is an MVP code package; you will still need to create Unity scene objects, prefabs, and link references in the Inspector.
- Keep sensitive keys out of repo and use GitHub Secrets for CI.
