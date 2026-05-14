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