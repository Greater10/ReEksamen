using DeltaProject.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace DeltaProject.View
{
    public partial class TasksWindow : Window
    {
        public TasksWindow()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as TasksViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedAssignedTo.Clear();
                foreach (var item in ((ListBox)sender).SelectedItems)
                {
                    viewModel.SelectedAssignedTo.Add(item.ToString());
                }
            }
        }

        private void DepartmentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as TasksViewModel;
            if (viewModel != null)
            {
                viewModel.SelectedDepartments.Clear();
                foreach (var item in ((ListBox)sender).SelectedItems)
                {
                    viewModel.SelectedDepartments.Add(item.ToString());
                }
            }
        }
    }
}
