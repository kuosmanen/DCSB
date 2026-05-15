using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Windows.Data;
using System.Windows;
using DCSB.Models;
using DCSB.ViewModels;

namespace DCSB.Views.MainWindow
{
    /// <summary>
    /// Interaction logic for SoundListView.xaml
    /// </summary>
    public partial class SoundListView : UserControl
    {
        private enum SearchScope
        {
            CurrentPreset,
            AllPresets
        }

        private readonly ObservableCollection<Sound> _allSounds = new ObservableCollection<Sound>();
        private readonly Dictionary<Sound, Preset> _soundOwners = new Dictionary<Sound, Preset>();
        private ICollectionView _view;
        private ConfigurationModel _configurationModel;
        private SearchScope _searchScope = SearchScope.CurrentPreset;
        private bool _suppressSelectionChanged = false;
        private readonly System.Windows.Threading.DispatcherTimer _searchTimer = new System.Windows.Threading.DispatcherTimer();

        public SoundListView()
        {
            InitializeComponent();
            DataContextChanged += SoundListView_DataContextChanged;

            //0.1 second delay after user stops typing before refreshing filter to avoid high CPU usage on large sound lists
            _searchTimer.Interval = System.TimeSpan.FromMilliseconds(100);
            _searchTimer.Tick += (s, e) =>
            {
                _searchTimer.Stop();
                RefreshFilter();
            };
        }

        private void SoundsDataGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            HookConfigurationModel();
            UpdateSearchScopeButton();
            EnsureView();
            RefreshSoundSource();
        }

        private void SoundListView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UnhookConfigurationModel();
            HookConfigurationModel();
            RefreshSoundSource();
        }

        private void HookConfigurationModel()
        {
            if (_configurationModel != null || !(DataContext is ViewModel viewModel)) return;

            _configurationModel = viewModel.ConfigurationModel;
            if (_configurationModel == null) return;

            _configurationModel.PropertyChanged += ConfigurationModel_PropertyChanged;
            _configurationModel.PresetCollection.CollectionChanged += PresetCollection_CollectionChanged;

            foreach (Preset preset in _configurationModel.PresetCollection)
            {
                preset.SoundCollection.CollectionChanged += SoundCollection_CollectionChanged;
            }
        }

        private void UnhookConfigurationModel()
        {
            if (_configurationModel == null) return;

            _configurationModel.PropertyChanged -= ConfigurationModel_PropertyChanged;
            _configurationModel.PresetCollection.CollectionChanged -= PresetCollection_CollectionChanged;

            foreach (Preset preset in _configurationModel.PresetCollection)
            {
                preset.SoundCollection.CollectionChanged -= SoundCollection_CollectionChanged;
            }

            _configurationModel = null;
        }

        private void ConfigurationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ConfigurationModel.SelectedPreset))
            {
                if (_searchScope == SearchScope.CurrentPreset)
                {
                    // This refresh should make the sound blue when clicked in the list even if it's in a different preset
                    RefreshSoundSource();
                }
            }
        }

        private void PresetCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Preset preset in e.OldItems)
                {
                    preset.SoundCollection.CollectionChanged -= SoundCollection_CollectionChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (Preset preset in e.NewItems)
                {
                    preset.SoundCollection.CollectionChanged += SoundCollection_CollectionChanged;
                }
            }

            RefreshSoundSource();
        }

        private void SoundCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new System.Action(() =>
            {
                RefreshSoundSource();
            }));
        }

        private void EnsureView()
        {
            if (SoundsDataGrid == null) return;
            _view = CollectionViewSource.GetDefaultView(SoundsDataGrid.ItemsSource);
            if (_view != null)
            {
                _view.Filter = FilterSound;
            }
        }

        private bool FilterSound(object obj)
        {
            if (obj is Sound sound)
            {
                string query = SearchBox?.Text;
                if (string.IsNullOrWhiteSpace(query)) return true;
                return sound.Name?.IndexOf(query, System.StringComparison.OrdinalIgnoreCase) >= 0;
            }
            return true;
        }

        private void RefreshSoundSource()
        {
            if (SoundsDataGrid == null || _configurationModel == null) return;

            Sound selectedSound = SoundsDataGrid.SelectedItem as Sound;

            RebuildAllSounds();

            var newSource = _searchScope == SearchScope.AllPresets
                ? (IEnumerable)_allSounds
                : _configurationModel.SelectedPreset.SoundCollection;

            if (SoundsDataGrid.ItemsSource != newSource)
            {
                SoundsDataGrid.ItemsSource = newSource;
            }

            EnsureView();
            RefreshFilter();
            UpdateSearchScopeButton();

            if (selectedSound != null)
            {
                try
                {
                    // Guarding with suppression to avoid recursive refresh-selection loops that cause StackOverflow when selecting a sound in "all presets" mode
                    _suppressSelectionChanged = true;
                    SoundsDataGrid.SelectedItem = selectedSound;
                }
                finally
                {
                    _suppressSelectionChanged = false;
                }
            }
        }

        private void RebuildAllSounds()
        {
            _allSounds.Clear();
            _soundOwners.Clear();

            foreach (Preset preset in _configurationModel.PresetCollection)
            {
                foreach (Sound sound in preset.SoundCollection)
                {
                    _allSounds.Add(sound);
                    _soundOwners[sound] = preset;
                }
            }
        }

        private void RefreshFilter()
        {
            _view = CollectionViewSource.GetDefaultView(SoundsDataGrid?.ItemsSource);
            if (_view != null)
            {
                _view.Filter = FilterSound;
                _view.Refresh();
            }
        }

        private void UpdateSearchScopeButton()
        {
            if (SearchScopeButton == null) return;

            SearchScopeButton.Content = _searchScope == SearchScope.CurrentPreset
                ? "current preset"
                : "all presets";
        }

        private void SearchScopeButton_Click(object sender, RoutedEventArgs e)
        {
            _searchScope = _searchScope == SearchScope.CurrentPreset
                ? SearchScope.AllPresets
                : SearchScope.CurrentPreset;

            RefreshSoundSource();
        }

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private void SoundsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_suppressSelectionChanged) return;
            if (_configurationModel == null || !(SoundsDataGrid.SelectedItem is Sound selectedSound)) return;

            Preset selectedPreset;
            if (!_soundOwners.TryGetValue(selectedSound, out selectedPreset))
            {
                selectedPreset = _configurationModel.SelectedPreset;
            }

            selectedPreset.SelectedSound = selectedSound;
            _configurationModel.ActiveSound = selectedSound;
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Double-click is already wired via Interactivity to PlayCommand on the DataContext,
            // but keep this handler if row-level wiring is preferred in future.
        }
    }
}
