using System;
using System.Windows;
using DeltaProject.ViewModel;
using DeltaProject.Model;
using DeltaProject.DataAccess;

namespace DeltaProject.View
{
  public partial class CreateWindow : Window
  {
    private ContactViewModel model = new ContactViewModel(new Contact(), new ContactRepository());

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