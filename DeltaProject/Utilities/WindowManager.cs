using DeltaProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DeltaProject.Utilities
{
    /// <summary>
    /// Helper utility for managing windows
    /// </summary>
    public static class WindowManager
    {
        public static void OpenWindow<TWindow, TViewModel>(TViewModel viewModel, Action<TWindow> configure = null)
            where TWindow : Window, new()
            where TViewModel : ViewModelBase
        {
            var window = new TWindow();
            if (viewModel != null)
            {
                viewModel.WarningHandler += (sender, e) =>
                {
                    MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                };
                window.DataContext = viewModel;
            }
            configure?.Invoke(window);
            window.Show();
        }

        public static void CloseWindow<T>() where T : Window
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is T)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
