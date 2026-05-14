using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using DCSB.Models;

namespace DCSB.Views.MainWindow
{
    /// <summary>
    /// Interaction logic for SoundListView.xaml
    /// </summary>
    public partial class SoundListView : UserControl
    {
        private ICollectionView _view;

        public SoundListView()
        {
            InitializeComponent();
        }

        private void SoundsDataGrid_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            EnsureView();
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

        private void SearchBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // Reacquire view in case ItemsSource changed (e.g., preset switch)
            _view = CollectionViewSource.GetDefaultView(SoundsDataGrid?.ItemsSource);
            if (_view != null)
            {
                _view.Filter = FilterSound;
                _view.Refresh();
            }
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Double-click is already wired via Interactivity to PlayCommand on the DataContext,
            // but keep this handler if row-level wiring is preferred in future.
        }
    }
}
