Title: Puzzle Room 

Overview
This project is a single‑room escape puzzle built in Unity. The player enters a room and the door locks behind them, player solves five puzzles, and the door opens upon success.

Game Concept
The player explores one room containing interactive props. Each puzzle changes the room state and increments the puzzle progress. When all five puzzles are solved, the exit door unlocks and opens so the player can leave.

Puzzles

Color Pad
Place the cube on a glowing floor pad to activate it.

Button Sequence
Press a set of wall buttons in the correct order. A wrong order resets that puzzle and counts as a failure.

Bookshelf Rotation
Rotate two books on a shelf to match a hidden target orientation.

Light Pattern
Toggle a set of lights or switches into the correct on and off pattern.

Weight Plate
Snap a heavy object onto a floor plate to apply enough weight and complete the last puzzle.

Controls

Move: WASD

Look: Mouse

Interact: E

Quit: Escape

Puzzle Progress UI
The top of the wall near the door shows puzzle progress as:
Puzzle Progress: X / 5
This number updates whenever a puzzle becomes solved or unsolved.

Win and Fail Conditions

Win: All five puzzles are solved. The door unlocks, opens, and the player can walk out of the room.

Fail: Pressing the button puzzle sequence in the wrong order triggers a failure for that puzzle, clearly logged and resetting its state. The player must correct mistakes before the puzzle can be solved.

Scripts
The project uses custom C# scripts to handle interaction, puzzle logic, progress tracking, and door control. Key scripts include:

PuzzleManager: Tracks each puzzle’s solved state, updates the progress UI, and controls the door when all puzzles are solved.

ColorCube and ColorPad: Handle the cube placement puzzle.

ButtonInteract and ButtonPuzzle: Handle the button sequence puzzle and its fail/reset behavior.

BookRotate and BookPuzzle: Handle the bookshelf rotation puzzle.

LightSwitchInteract and LightPuzzle: Handle the light pattern puzzle.

HeavyObject and WeightPlate: Handle the weight plate puzzle using a snap mechanic.

DoorController and DoorTrigger: Handle the automatic door open and close behavior based on puzzle progress and player position.

Assets
All visible environment and puzzle props are sourced from Kenney: 
Kenney Furniture Kit : 
Kenney Building Kit