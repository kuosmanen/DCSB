# Deathcounter and Soundboard

Deathcounter and Soundboard allows you to create keyboard shortcuts to trigger sound effects and keep count of something (for example deaths in game). Key shortcuts are recognized even if application is not focused.

![Application screenshot](screenshot.png?raw=true "Main Window")

## For each counter you can set:
- *Name* - to easily identify counters
- *Path to text file* - file where current count is stored and can be used to display it on stream
- *Count*
- *Increment* - number that is added/subtracted
- *Format* - allows you to add custom text to accompany count number

## For each sound you can set:
-	*Name* - to easily identify different sounds
-	*Path to one or more sound files*, random one will be played any time you hit specified key. Supported file formats are: wma, mp3, wav, ogg, m4a, aiff, and flac
-	*Key or key combination* - to play this sound
-	*Volume* - for this sound
-	*Loop* - whether or not to loop this sound

## Presets
You can create Presets to quickly switch between lists of counters and sounds for different situations, even with keyboard shortcut.

## Application allows you to set some more keyboard shortcut:
-	Select next counter
-	Select previous counter
-	Increment counter
-	Decrement counter
-	Reset counter
-	Pause all playing sounds
-	Continue playing sound
-	Stop all playing sounds

## Additional settings:
-	Overlap sounds
-	Select one or two specific sound output devices
-	Enable/disable counters or sounds
-	Display/hide counters or sounds
-	Minimize to tray

## Development workflow in this fork
This repository contains a classic .NET Framework WPF solution, so the development flow is a little different from a modern .NET project.

### Building with VS Code
- Use the VS Code build task or Visual Studio MSBuild to build the solution.
- In VS Code, press `Ctrl+Shift+B` or run the build task named `Build solution (MSBuild)`.
- The task resolves MSBuild through `vswhere` first, and falls back to `msbuild` on your `PATH`.
- This is preferred over `dotnet build` for this solution, because the project uses legacy WPF build targets!

### Run
- After building, start the application from the generated executable at `DCSB/bin/Debug/DCSB.exe`.
- If you build Release, the executable will be at `DCSB/bin/Release/DCSB.exe`.
- You can launch it from PowerShell with `Start-Process .\DCSB\bin\Debug\DCSB.exe`.

### Notes
- If the build task cannot find MSBuild, install Visual Studio Build Tools or open the repo from a Visual Studio Developer Command Prompt.
- If VS Code still shows an older task, reload the window so it picks up the latest `.vscode/tasks.json`.

## Please read:
If you come across any bug, something stops working or the program crashes please create issue here. Include as much information as you can provide (any error messages, what stopped working, what were you doing when it happened, what version you are using...).
Usually restarting the program/running it as an administrator helps.
