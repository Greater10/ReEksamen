using System;
using System.Windows;
using DeltaProject.Model;
using DeltaProject.ViewModel;

namespace DeltaProject.View
{
  public partial class UpdateWindow : Window
  {
    private ContactViewModel model;

    public UpdateWindow(Contact contact)
    {
      model = new ContactViewModel(contact, MainViewModel.repository);
      InitializeComponent();
      model.CloseHandler += delegate(object sender, EventArgs e) { Close(); };
      model.WarningHandler += delegate(object sender, MessageEventArgs e) { 
          MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); 
      };
      DataContext = model;
    }
  }
}