# VR Comfort System

A VR comfort settings system built in Unity, made to actually reduce motion sickness (not just toggle switches) through teleport/turn options, an adaptive comfort vignette, and speed control.

*(Türkçe açıklama için: [README.tr.md](README.tr.md))*

## About the project

This is my second portfolio project after [VR Escape Room](https://github.com/berlie-28/VR-Escape-Room). The goal wasn't just to build a settings menu. VR makes a lot of people motion sick because of something called vection, where your eyes see movement but your inner ear doesn't feel any. I wanted to understand why that happens and build a small system that actually addresses it, with a demo corridor to feel the difference for yourself.

I used AI assistance for this project, mainly for scripting and for debugging some tricky XR Interaction Toolkit internals. The Unity Editor work, testing, and the design decisions were mine.

## Features

- Movement mode: teleport or continuous walking, switchable at runtime (never both at once)
- Turn mode: snap turn (instant steps) or smooth turn
- Adaptive comfort vignette: darkens the edges of the screen based on how fast you're turning/moving, fades back out when you stop, instead of a static on/off effect
- Movement speed slider
- Settings are saved with PlayerPrefs and reload on next launch
- World-space menu you summon in front of you with a button press (rather than a screen overlay), stays where you placed it like a real object
- A small L-shaped demo corridor with a 90° turn, to actually feel the difference between the settings

## Why these settings help

- **Teleport** removes continuous movement entirely, so there's no "your eyes are moving but your body isn't" mismatch (vection) during locomotion.
- **Snap turn** does the same for rotation: it happens instantly instead of sweeping your view, so there's nothing for your eyes to disagree with your inner ear about.
- **The vignette** narrows your peripheral vision during motion, since peripheral vision is where vection is felt the strongest.
- **Lower speed** simply means less visual motion per second, which means less vection.

## Controls (Editor testing)

I don't own a VR headset and I'm on macOS, so I tested using XR Interaction Toolkit's built-in **XR Device Simulator** instead of real controllers:

- **T**: control the left controller (movement / teleport)
- **Y**: control the right controller (turn)
- **WASD**: move / turn, depending on which controller is active
- **M**: open/close the comfort menu
- **F1**: the simulator's own control reference overlay

The actual XR Interaction Toolkit + OpenXR setup is in the project and ready for a real headset, I just haven't had one to test on.

## Built with

- Unity 6 (6000.3.10f1)
- Universal Render Pipeline (URP)
- XR Interaction Toolkit 3.3.2
- OpenXR
- Unity Input System
- TextMeshPro

## A bug I ran into

Changing the turn setting in the menu sometimes did nothing, and there was no error at all. It turned out one of XR Interaction Toolkit's sample scripts decides on its own which controller moves and which one turns, and it kept resetting the setting I had just changed. I only figured this out by reading the package's own source code, it wasn't visible from the Inspector.

## Known limitations

- Never tested on real VR hardware, only through the XR Device Simulator described above.
- Built and tested on macOS.
- The demo corridor is minimal on purpose (just enough to feel snap vs. smooth turn and teleport vs. walking), not a full level.

## Running the project

1. Open the project with Unity **6000.3.10f1** (or close to it).
2. Open `Assets/Scenes/SampleScene.unity`.
3. Press Play. Use the controls above to move around, turn, and open the menu.
