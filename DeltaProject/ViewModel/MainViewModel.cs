using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DeltaProject.Model;
using DeltaProject.DataAccess;
using DeltaProject.View;
using DeltaProject.Utilities;
using DeltaProject.Services;

namespace DeltaProject.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const string CredentialsFilePath = "credentials.txt";

        public RelayCommand LoginCommand { get; private set; }

        public static EmployeeRepository employeeRepository = new EmployeeRepository();
        public static LocationRepository locationRepository = new LocationRepository();

        private ObservableCollection<Location> locations;
        private string email = "";
        private string password = "";
        private string phone = "";
        private bool rememberMe;

        public MainViewModel()
        {
            LoginCommand = new RelayCommand(p => Login(), p => CanLogin());
            locations = new ObservableCollection<Location>(locationRepository);
            locationRepository.RepositoryChanged += RefreshLocations;
            locationRepository.GetAll();
            LoadCredentials();
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

        public ObservableCollection<Location> SelectedLocations
        {
            get { return new ObservableCollection<Location>(locations.Where(l => l.IsSelected)); }
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

        public bool RememberMe
        {
            get { return rememberMe; }
            set
            {
                if (rememberMe != value)
                {
                    rememberMe = value;
                    OnPropertyChanged("RememberMe");
                }
            }
        }

        private void Clear()
        {
            Email = "";
            Password = "";
            Phone = "";
        }

        private void Login()
        {
            try
            {
                var employee = employeeRepository.ValidateLogin(email, password);

                if (employee == null)
                {
                    throw new InvalidOperationException("Forkert brugernavn eller kodeord");
                }

                // Successful login
                UserService.Instance.Login(employee.EmployeeId, employee.Name, phone, SelectedLocations.Select(l => l.LocationId).ToList());

                if (RememberMe)
                {
                    SaveCredentials();
                }
                else
                {
                    ClearCredentials();
                }

                WindowManager.OpenWindow<TasksWindow, TasksViewModel>(new TasksViewModel());
                WindowManager.CloseWindow<MainWindow>();
            }
            catch (Exception ex)
            {
                // Login failed
                OnWarning(ex.Message);
            }
        }

        private bool CanLogin()
        {
            return !(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(phone) || !SelectedLocations.Any());
        }

        private void LoadCredentials()
        {
            if (File.Exists(CredentialsFilePath))
            {
                var credentials = File.ReadAllLines(CredentialsFilePath);
                if (credentials.Length == 3)
                {
                    Email = credentials[0];
                    Password = credentials[1];
                    Phone = credentials[2];
                    RememberMe = true;
                }
            }
        }

        private void SaveCredentials()
        {
            File.WriteAllLines(CredentialsFilePath, new[] { Email, Password, Phone });
        }

        private void ClearCredentials()
        {
            if (File.Exists(CredentialsFilePath))
            {
                File.Delete(CredentialsFilePath);
            }
        }
    }
}
