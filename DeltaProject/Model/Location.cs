using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DeltaProject.Model
{
    public class Location : IDataErrorInfo, IComparable<Location>, INotifyPropertyChanged
    {
        private bool isSelected;

        public int LocationId { get; set; }
        public string Name { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public Location()
        {
            Name = "";
        }

        public Location(int locationId, string name, bool isSelected = false)
        {
            LocationId = locationId;
            Name = name;
            IsSelected = isSelected;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Implement comparison on SocialSecurityNumber
        public override bool Equals(object obj)
        {
            try
            {
                Location location = (Location)obj;
                return LocationId.Equals(location.LocationId);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return LocationId.GetHashCode();
        }

        // Implementerer ordning af adresse alene ud fra telefonnummeret
        public int CompareTo(Location location)
        {
            return LocationId.CompareTo(location.LocationId);
        }

        // Validering af objektet.
        // Arrayet angiver hvilke properties, der skal valideres.
        private static readonly string[] validatedProperties = { "Name", "LocationId" };

        public bool IsValid
        {
            get
            {
                foreach (string property in validatedProperties) if (GetError(property) != null) return false;
                return true;
            }
        }

        string IDataErrorInfo.Error
        {
            get { return IsValid ? null : "Illegal Location object"; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return Validate(propertyName); }
        }

        private string GetError(string name)
        {
            foreach (string property in validatedProperties) if (property.Equals(name)) return Validate(name);
            return null;
        }

        private string Validate(string name)
        {
            switch (name)
            {
                case "Name": return ValidateName();
                case "LocationId": return ValidateLocationId();
            }
            return null;
        }

        private string ValidateLocationId()
        {
            if (LocationId == 0) return "LocationId can not be less than or equal to 0";
            return null;
        }

        private string ValidateName()
        {
            if (Name == null) return "Name can not be null";
            return null;
        }
    }
}
