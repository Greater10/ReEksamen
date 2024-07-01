using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DeltaProject.Model
{
    public class Department : IDataErrorInfo, IComparable<Department>
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }


        public Department()
        {
            Name = "";

        }

        public Department(int departmentId, string name, int locationId)
        {
            DepartmentId = departmentId;
            Name = name;
            LocationId = locationId;
           
        }

        
        

        public override int GetHashCode()
        {
            return DepartmentId.GetHashCode();
        }

        // Implementerer ordning af adresse alene ud fra telefonnummeret
        public int CompareTo(Department department)
        {
            return DepartmentId.CompareTo(department.DepartmentId);
        }

        // Validering af objektet.
        // Arrayet angiver hvilke properties, der skal valideres.
        private static readonly string[] validatedProperties = { "Name", "DepartmentId", "LocationId"};

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
            get { return IsValid ? null : "Invalid DepartmentId"; }
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
                case "DepartmentId": return ValidateDepartmentId();
                case "LocationId": return ValidateLocationId();
              
            }
            return null;
        }

        private string ValidateDepartmentId()
        {
            if (DepartmentId == 0) return "DepartmentId can not be less or equal to 0";
            return null;
        }
  

        private string ValidateLocationId()
        {
            if (LocationId == 0) return "LocationId can not be less or equal to 0";
            return null;
        }


        private string ValidateName()
        {
            if (Name == null) return "Name can not be null";
            return null;
        }

    }
}