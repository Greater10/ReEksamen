﻿using System;
using System.Collections.ObjectModel;
using DeltaProject.Model;
using DeltaProject.DataAccess;
using DeltaProject.View;

namespace DeltaProject.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand LoginCommand { get; private set; }

        public static EmployeeRepository employeeRepository = new EmployeeRepository();
        public static LocationRepository locationRepository = new LocationRepository();

        private ObservableCollection<Location> locations;

        private string email = "";
        private string password = "";
        private string phone = "";

        public MainViewModel()
        {
            LoginCommand = new RelayCommand(p => Login(), p => CanLogin());
            locations = new ObservableCollection<Location>(locationRepository);
            locationRepository.RepositoryChanged += RefreshLocations;
            locationRepository.GetAll();
        }

        private void RefreshLocations(object sender, DbEventArgs e)
        {
            Locations = new ObservableCollection<Location>(locationRepository);
        }

        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set
            {
                if (!locations.Equals(value))
                {
                    locations = value;
                    OnPropertyChanged("Locations");
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (!email.Equals(value))
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (!password.Equals(value))
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                if (!phone.Equals(value))
                {
                    phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }

        private void Clear()
        {
            email = "";
            password = "";
            phone = "";
        }

        private void Login()
        {
            try
            {
                employeeRepository.ValidateLogin(email, password);
            }
            catch (Exception ex)
            {
                OnWarning(ex.Message);
            }
        }

        private bool CanLogin()
        {
            return !(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phone));
        }
    }
}