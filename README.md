# Sped@home-Assignment



\# AR Body Tracking \& Mini-Game Assessment



\## Overview

This project contains two tasks combined into a single application:

1\. Task 1: Tracking Demo - Real-time upper-body joint tracking using webcam feed.

2\. Task 2: AR Game - An interactive falling-object catcher utilizing physics, object pooling, and event-driven architecture.



\## Installation \& Execution

\* Launch the 'Application.exe'.

\* Ensure your webcam is connected and unblocked by Windows security or any other blocker.

\* Use the Main Menu to navigate between tasks. Press Escape at any time to return to the menu.



\*\*NOTE- The tracker sometimes takes time to turn on so if it is not working immediately, wait or restart the task.



\## Technical Implementation \& Architecture



1. Tracking Pipeline (Task 1)

\* Tracking Engine: Utilizes BlazePose/Barracuda for real-time ML pose estimation. 



2\. Game Architecture (Task 2)

\* Decoupled Physics: ML inference runs in 'Update', while physical hand colliders use 'Rigidbody.MovePosition' inside 'FixedUpdate' to prevent physics desyncs and micro-stuttering.

\* Memory Management: 'SpawnManager' utilizes an Object Pool created at start with random items from prefabs.

\* Event-Driven UI: The 'FallingItem' script uses an 'OnCaught' Action event. The 'GameManager' listens for this event to update the score, completely decoupling the game logic from the physical 3D objects.

\* Scriptable Objects for Design: Game variables (fall speed, spawn intervals, score limits) are isolated in a 'GameSettingsSO' ScriptableObject, allowing designers to tune the game without touching code.



\*\*NOTE- For default game settings- each falling item gives 10 points and collect 100 points to WIN!!

