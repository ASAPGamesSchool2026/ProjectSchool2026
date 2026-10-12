# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

Unity teaching project for a school course: a small top-down 2D exploration game ("Экспедиция: Тропа Дерсу"). The player moves with WASD/arrows, the camera follows, and touching artifacts increments a `N/M` counter on a Canvas. Lesson notes (in Russian) live in `LESSON_01.md` and describe the intended scene setup, behaviour checklist and homework. Comments and in-game strings are in Russian. Keep code at a level suitable for teaching (simple C#, no advanced patterns).

- Unity `6000.6.0f1`, URP 2D `17.6.0`, Input System `1.20.0`, uGUI/TextMeshPro `2.6.0`, 2D feature set (tilemaps, sprite atlas).
- DOTween (Demigiant) is vendored in `Assets/Plugins/Demigiant` (not a UPM package) — don't edit it.
- `com.coplaydev.unity-mcp` (MCP for Unity) is installed, so the Unity Editor can be driven via MCP when it's running.

## Building / running

There is no CLI build, lint, or test setup and no test assemblies. Work happens in the Unity Editor: open the project, load `Assets/_ProjectSchool2026/Scenes/SampleScene.unity` (the only scene in Build Settings; `TileMap.unity` is a tilemap work scene) and press Play. Verify against the "Проверка финального варианта" checklist in `LESSON_01.md`; Console must have no compile errors, Missing Script, or exceptions.

`Packages/manifest.json` and `Packages/packages-lock.json` must be committed together; after changing the manifest let Unity resolve packages and update the lock file before committing.

## Branching / commits

Each lesson has a `LessonN_Start` (student starting point) and `LessonN_Final` (reference solution) branch, plus `main`. Commit messages are prefixed with the branch name, e.g. `[Lesson2_Final] Implemented Dotween`. Don't replace working Final code with student stubs; producing a Start variant (emptied method bodies, see `LESSON_01.md` "Учебный вариант") is a separate, explicitly agreed step.

## Architecture

All game code is in `Assets/_ProjectSchool2026/Scripts`, namespace `ProjectSchool2026`, default `Assembly-CSharp` (no asmdefs). Much of the wiring is done in the Inspector/scene, so changes often require matching scene/prefab edits.

- **Input → movement:** `PlayerInput` on the root `Player` uses `Assets/InputSystem_Actions.inputactions` (map `Player`) with Behavior = *Invoke Unity Events*; the Move event is bound in the Inspector to `PlayerMovement.OnMove`. `OnMove` only stores the `Vector2` (all phases, so `canceled` resets to zero); `FixedUpdate` sets `Rigidbody2D.linearVelocity` from the normalized direction.
- **Player hierarchy:** root `Player` has `Rigidbody2D` + `PlayerMovement` + `PlayerInput`; the collider, `SpriteRenderer`, `Animator` and `PlayerAnimation` sit on the child `Player/Visual`. `PlayerMovement` finds `PlayerAnimation` via `GetComponentInChildren` and forwards input to it.
- **Animation:** `PlayerAnimation` drives Animator triggers (`runRight`, `runDown`, `runUp`, `idle`; left = `runRight` + `flipX`) and a looping DOTween scale "breathing" sequence that plays only while idle. Animator controllers/clips are in `Assets/_ProjectSchool2026/Animations/Player`.
- **Collection:** `ArtifactItem` (trigger collider, no Rigidbody) detects the player via `other.attachedRigidbody` having `PlayerMovement` (because the player's collider is on a child), then calls `ExplorationManager.Instance.CollectArtifact(this)` and disables itself on success.
- **ExplorationManager:** scene-scoped singleton (not `DontDestroyOnLoad`). `_requiredArtifacts` (Inspector `List`) defines the goal set and the total; collected items go in a `HashSet` so duplicates and items not in the list are ignored. An empty list never counts as complete. It pushes progress to `ArtifactCounterView`, which only formats `collected/total` into a TMP text.
- `CameraFollow` lerps the camera toward the target in `LateUpdate` with offset `(0,0,-10)`.

## Code style

Private serialized fields are `[SerializeField] private` with `_camelCase`; Animator parameters are cached with `Animator.StringToHash` in `static readonly` fields. Most scripts use tab indentation and Allman braces — match the file you're editing.
