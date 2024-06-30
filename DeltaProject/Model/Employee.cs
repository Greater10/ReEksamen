using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DeltaProject.Model
{
    public class Employee : IDataErrorInfo, IComparable<Employee>
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string WorkEmail { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Phone { get; set; }
        public string WorkPhone { get; set; }
        public string Password { get; set; }

        public Employee()
        {
            Name = "";
            Address = "";
            WorkEmail = "";
            SocialSecurityNumber = "";
            Phone = "";
            WorkPhone = "";
            Password = "";
        }

        public Employee(int employeeId, string name, string address, string workEmail, string socialSecurityNumber, string phone, string workPhone, string password)
        {
            EmployeeId = employeeId;
            Name = name;
            Address = address;
            WorkEmail = workEmail;
            SocialSecurityNumber = socialSecurityNumber;
            Phone = phone;
            WorkPhone = workPhone;
            Password = password;
        }

        // Implement comparison on SocialSecurityNumber
        public override bool Equals(object obj)
        {
            try
            {
                Employee employee = (Employee)obj;
                return SocialSecurityNumber.Equals(employee.SocialSecurityNumber);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return SocialSecurityNumber.GetHashCode();
        }

        // Implementerer ordning af adresse alene ud fra telefonnummeret
        public int CompareTo(Employee employee)
        {
            return SocialSecurityNumber.CompareTo(employee.SocialSecurityNumber);
        }

        // Validering af objektet.
        // Arrayet angiver hvilke properties, der skal valideres.
        private static readonly string[] validatedProperties = { "Name", "Address", "WorkEmail", "SocialSecurityNumber", "Phone", "WorkPhone", "Password" };

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
            get { return IsValid ? null : "Illegal Employee object"; }
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
                case "Address": return ValidateAddress();
                case "WorkEmail": return ValidateWorkEmail();
                case "SocialSecurityNumber": return ValidateSocialSecurityNumber();
                case "Phone": return ValidatePhone();
                case "WorkPhone": return ValidateWorkPhone();
                case "Password": return ValidatePassword();
            }
            return null;
        }

        private string ValidatePassword()
        {
            if (Password == null) return "Password can not be null";
            return null;
        }

        private string ValidateWorkPhone()
        {
            if (WorkPhone.Length != 8) return "WorkPhone must be a number of 8 digits";
            foreach (char c in WorkPhone) if (c < '0' || c > '9') return "WorkPhone must be a number of 8 digits";
            return null;
        }

        private string ValidatePhone()
        {
            if (Phone.Length != 8) return "Phone must be a number of 8 digits";
            foreach (char c in Phone) if (c < '0' || c > '9') return "Phone must be a number of 8 digits";
            return null;
        }

        private string ValidateSocialSecurityNumber()
        {
            if (SocialSecurityNumber == null) return "SocialSecurityNumber can not be null";
            return null;
        }

        private string ValidateWorkEmail()
        {
            if (WorkEmail == null) return "WorkEmail can not be null";
            if (WorkEmail.Length == 0) return null;
            return IsValidEmail(WorkEmail) ? null : "WorkEmail must be legal";
        }

        private string ValidateAddress()
        {
            if (Address == null) return "Address can not be null";
            return null;
        }

        private string ValidateName()
        {
            if (Name == null) return "Name can not be null";
            return null;
        }

        private static bool IsValidEmail(string email)
        {
            Regex rx = new Regex(@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return rx.IsMatch(email);
        }
    }
}
