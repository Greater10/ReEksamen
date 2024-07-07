using System;
using System.Windows;
using DeltaProject.ViewModel;

namespace DeltaProject.View
{
  public partial class TaskDetailWindow : Window
  {
    private TaskDetailViewModel model = new TaskDetailViewModel();

    public TaskDetailWindow()
    {
      InitializeComponent();
      model.WarningHandler += delegate(object sender, MessageEventArgs e) { 
          MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); 
      };
      model.CloseHandler += delegate(object sender, EventArgs e) { Close(); };
      DataContext = model;
    }    
  }
}