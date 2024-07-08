using System.Windows;
using System.Windows.Controls;
using DeltaProject.ViewModel;
using DeltaProject.Model;

namespace DeltaProject
{
    public partial class MainWindow : Window
    {
        private MainViewModel model = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = model;
            model.WarningHandler += delegate (object sender, MessageEventArgs e) {
                MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;
            if (viewModel != null)
            {
                foreach (var item in e.AddedItems)
                {
                    var location = item as Location;
                    if (location != null)
                    {
                        location.IsSelected = true;
                    }
                }
                foreach (var item in e.RemovedItems)
                {
                    var location = item as Location;
                    if (location != null)
                    {
                        location.IsSelected = false;
                    }
                }
            }
        }
    }
}
