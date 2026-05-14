# Changes

This file tracks notable changes made in this modified version of the project.
It is provided to make GPLv3 distribution compliance easier by documenting modifications and dates.

## 2026-05-13

- Added `CHANGES.md` to document modifications in this fork.

## 2026-05-14
- Added search/filter and double-click play for sounds

- UI: added a search box to the sound list view (`DCSB.Views/MainWindow/SoundListView.xaml`) above the DataGrid so you can type to filter sounds by name.
- UI: named the sounds grid `SoundsDataGrid` and hooked a `Loaded` handler to initialize filtering.
- Code: implemented case-insensitive filtering in `DCSB.Views/MainWindow/SoundListView.xaml.cs` using a `CollectionView` filter; the search updates on text changes and refreshes when presets change.
- Behavior: double-clicking a sound remains wired to the existing `PlayCommand` (no change to play logic).
- Files modified:
	- `DCSB.Views/MainWindow/SoundListView.xaml`
	- `DCSB.Views/MainWindow/SoundListView.xaml.cs`

- Upgraded to .NET Framework 4.8 and updated project/config files

- Build: updated all projects from .NET Framework 4.5.2 to 4.8.
- Config: updated `app.config`/`App.config` files to target .NET Framework 4.8.
- References: added `DCSB.Models` project reference and fixed `DCSB.Interactivity` GUID in `DCSB.Views/DCSB.Views.csproj`.
- Code: added missing `using` directives in `DCSB.Views/MainWindow/SoundListView.xaml.cs`.
- Resources: regenerated `DCSB/Properties/Resources.Designer.cs` with updated code generation metadata.
- Files modified:
	- `DCSB.Business/DCSB.Business.csproj`
	- `DCSB.Business/app.config`
	- `DCSB.Controls/DCSB.Controls.csproj`
	- `DCSB.Converters/DCSB.Converters.csproj`
	- `DCSB.Converters/app.config`
	- `DCSB.Icons/DCSB.Icons.csproj`
	- `DCSB.Input/DCSB.Input.csproj`
	- `DCSB.Interactivity/DCSB.Interactivity.csproj`
	- `DCSB.Models/DCSB.Models.csproj`
	- `DCSB.Models/app.config`
	- `DCSB.Sound/DCSB.SoundPlayer.csproj`
	- `DCSB.Sound/app.config`
	- `DCSB.Utils/DCSB.Utils.csproj`
	- `DCSB.ViewModels/DCSB.ViewModels.csproj`
	- `DCSB.ViewModels/app.config`
	- `DCSB.Views/CounterView.xaml.cs`
	- `DCSB.Views/DCSB.Views.csproj`
	- `DCSB.Views/MainWindow/SoundListView.xaml.cs`
	- `DCSB.Views/app.config`
	- `DCSB/App.config`
	- `DCSB/DCSB.csproj`
	- `DCSB/MainWindow.xaml`
	- `DCSB/Properties/Resources.Designer.cs`

	