﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
      //DataContext = model;
      //model.WarningHandler += delegate(object sender, MessageEventArgs e) { 
      //    MessageBox.Show(e.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning); 
      //};
    }

    private void grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      //if (sender != null)
      //{
      //  try
      //  {
      //    DataGridRow row = sender as DataGridRow;
      //    Contact contact = (Contact)row.Item;
      //    model.UpdateContact(contact);
      //  }
      //  catch
      //  {
      //  }
      //}
    }
  }
}
