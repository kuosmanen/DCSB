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

- Added portable VS Code MSBuild build task

- Tooling: added a default VS Code build task in `.vscode/tasks.json` that builds `DCSB.sln` using MSBuild.
- Portability: removed machine-specific MSBuild path and added runtime MSBuild resolution using `vswhere` with a `msbuild`-from-PATH fallback.
- Behavior: keeps `Ctrl+Shift+B`/Run Build Task on MSBuild (instead of `dotnet build`) for this legacy .NET Framework WPF solution.
- Validation: task command flow was executed and build succeeded.
- Added documentation on how to build and run this project in VS Code 
- Files modified:
	- `.vscode/tasks.json`
	- `README.md`

- Refined the sound search bar layout and placeholder text

- UI: added a gray `Search...` placeholder to the sound search box so the empty state is clearer.
- UI: centered the search box, `in` label, and scope button within the top row between the `Sounds` title and the volume slider.
- Behavior: the search scope toggle remains between `current preset` and `all presets`.
- Files modified:
	- `DCSB.Views/MainWindow/SoundListView.xaml`
	- `DCSB.Views/MainWindow/SoundListView.xaml.cs`
	- `DCSB.Views\DCSB.Views.csproj`

- Resolved GalaSoft.MvvmLight reference warnings

- Build: aligned `DCSB.Converters` to use `GalaSoft.MvvmLight` version `5.4.1.0` to match the rest of the solution and reduce assembly version mismatch warnings.
- References: replaced the old implicit `5.3.0.19026` reference with an explicit reference and `HintPath` to `packages/MvvmLightLibs.5.4.1/lib/net45/GalaSoft.MvvmLight.dll`.
- Validation: rebuilt `DCSB.sln` with MSBuild and confirmed successful build.
- Files modified:
	- `DCSB.Converters/DCSB.Converters.csproj`

- Normalized remaining package/framework metadata to .NET Framework 4.8 to fix warnings

- Packages: updated all `packages.config` `targetFramework` values from `net45`/`net452`/`net461` to `net48`.
- References: updated `DCSB.Sound/DCSB.SoundPlayer.csproj` to use `GalaSoft.MvvmLight` `5.4.1.0` with explicit `HintPath` (replacing stale `5.3.0.19026`).
- Validation: solution builds successfully with MSBuild after these updates.
- Files modified:
	- `DCSB/packages.config`
	- `DCSB.Business/packages.config`
	- `DCSB.Interactivity/packages.config`
	- `DCSB.Models/packages.config`
	- `DCSB.Sound/packages.config`
	- `DCSB.ViewModels/packages.config`
	- `DCSB.Sound/DCSB.SoundPlayer.csproj`

- Audited and aligned remaining package version references

- Audit: checked all `packages.config`, project reference versions, and app `bindingRedirect` entries for stale package versions.
- Config: updated stale `GalaSoft.MvvmLight` binding redirects from `5.3.0.19026` to `5.4.1.0`.
- Scope: local `packages/` contains one installed version per package ID, so no additional in-repo package version bumps were possible without fetching newer packages.
- Validation: MSBuild verification completed with `BUILD_EXIT=0`.
- Files modified:
	- `DCSB.Converters/app.config`
	- `DCSB.Sound/app.config`
	- `DCSB.Views/app.config`


## 2026-05-15

- Fixed sound selection behavior in "all presets" mode

- Bug: clicking a sound in "all presets" mode that belonged to a different preset would require two clicks to visually select. The first click was swallowed because changing the active preset rebuilt the `AllPresets` item list and cleared the DataGrid selection.
- Fix: updated `ConfigurationModel_PropertyChanged` in `DCSB.Views/MainWindow/SoundListView.xaml.cs` to only rebuild the item list and refresh the view if the search scope is `CurrentPreset`. When in `AllPresets` mode, the list is not unnecessarily rebuilt when the active preset changes.
- Changed behavior: selecting a sound in "all presets" mode no longer changes the currently active preset.
- Files modified:
	- `DCSB.Views/MainWindow/SoundListView.xaml.cs`
	- `DCSB.Models\ObservableObjectCollection.cs`
- Bug: typing in the search box triggered a filter refresh on every keystroke, which could get CPU intensive and laggy on large sound collections.
- Fix: implemented debouncing using `DispatcherTimer` in `DCSB.Views/MainWindow/SoundListView.xaml.cs`. The search filtering now waits for 100 milliseconds of typing inactivity before refreshing the list.
- Files modified:
	- `DCSB.Views/MainWindow/SoundListView.xaml.cs`


