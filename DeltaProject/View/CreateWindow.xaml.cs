using System;
using System.Windows;
using DeltaProject.ViewModel;
using DeltaProject.Model;

namespace DeltaProject.View
{
  public partial class CreateWindow : Window
  {
    private ContactViewModel model = new ContactViewModel(new Contact(), MainViewModel.repository);

    public CreateWindow()
    {
      InitializeComponent();
      model.CloseHandler += delegate(object sender, EventArgs e) { Close(); };
      model.WarningHandler += delegate(object sender, MessageEventArgs e) { 
          MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); 
      };
      DataContext = model;
    }    
  }
}