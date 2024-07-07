using System;
using System.Collections.Generic;

namespace DeltaProject.Services
{
    public class UserService
    {
        private static UserService instance;

        public static UserService Instance
        {
            get
            {
                if (instance == null)
                { 
                    instance = new UserService();
                }
                return instance;
            }
        }

        public bool IsLoggedIn { get; private set; }
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public List<int> SelectedLocationIds { get; private set; }

        private UserService()
        {
            SelectedLocationIds = new List<int>();
        }

        public void Login(int userId, string name, string phoneNumber, List<int> selectedLocationIds)
        {
            UserId = userId;
            Name = name;
            PhoneNumber = phoneNumber;
            SelectedLocationIds = new List<int>(selectedLocationIds);
            IsLoggedIn = true;
        }

        public void Logout()
        {
            UserId = 0;
            Name = string.Empty;
            PhoneNumber = string.Empty;
            SelectedLocationIds.Clear();
            IsLoggedIn = false;
        }
    }
}
