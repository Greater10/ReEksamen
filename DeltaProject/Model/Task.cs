using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace DeltaProject.Model
{
    public class Task : IDataErrorInfo, IComparable<Task>
    {
        public int TaskId { get; set; }
        public string PatientSocialSecurityNumber { get; set; }
        public string Room { get; set; }
        public string Bed { get; set; }
        public bool Isolated { get; set; }
        public bool Deaf { get; set; }
        public bool Mute { get; set; }
        public bool Inactive { get; set; }
        public bool ForeignLanguage { get; set; }
        public bool SpecialMedication { get; set; }
        public int Priority { get; set; }
        public DateTime TaskDate { get; set; }
        public string Comments { get; set; }
        public string PatientName { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }

        // Properties not part of the database model
        public string DepartmentName { get; set; }
        public string AssignedEmployeeName { get; set; }

        // List of Tests
        public List<Test> Tests { get; set; }

        // Boolean properties for test types
        public bool HasBlodtest => Tests.Any(t => t.TestType == (int)TestType.Bloodtest);
        public bool HasEKG => Tests.Any(t => t.TestType == (int)TestType.EKG);
        public bool HasGlukoseCsv => Tests.Any(t => t.TestType == (int)TestType.GlukoseCsv);
        public bool HasProcPcr => Tests.Any(t => t.TestType == (int)TestType.ProcPcr);

        public Task()
        {
            PatientSocialSecurityNumber = "";
            Room = "";
            Bed = "";
            Comments = "";
            PatientName = "";
            TaskDate = DateTime.Now;
            Tests = new List<Test>();
        }

        public Task(int taskId, string patientSocialSecurityNumber, string room, string bed, bool isolated, bool deaf, bool mute, bool inactive, bool foreignLanguage, bool specialMedication, int priority, DateTime taskDate, string comments, string patientName, int departmentId, int employeeId, List<Test> tests)
        {
            TaskId = taskId;
            PatientSocialSecurityNumber = patientSocialSecurityNumber;
            Room = room;
            Bed = bed;
            Isolated = isolated;
            Deaf = deaf;
            Mute = mute;
            Inactive = inactive;
            ForeignLanguage = foreignLanguage;
            SpecialMedication = specialMedication;
            Priority = priority;
            TaskDate = taskDate;
            Comments = comments;
            PatientName = patientName;
            DepartmentId = departmentId;
            EmployeeId = employeeId;
            Tests = tests ?? new List<Test>();
        }

        public override bool Equals(object obj)
        {
            try
            {
                Task task = (Task)obj;
                return TaskId.Equals(task.TaskId);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return TaskId.GetHashCode();
        }

        public int CompareTo(Task task)
        {
            return TaskId.CompareTo(task.TaskId);
        }

        private static readonly string[] validatedProperties = { "PatientSocialSecurityNumber", "Room", "Bed", "Comments", "PatientName", "Priority", "TaskDate" };

        public bool IsValid
        {
            get
            {
                foreach (string property in validatedProperties)
                    if (GetError(property) != null) return false;
                return true;
            }
        }

        string IDataErrorInfo.Error
        {
            get { return IsValid ? null : "Illegal Task object"; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return Validate(propertyName); }
        }

        private string GetError(string name)
        {
            foreach (string property in validatedProperties)
                if (property.Equals(name)) return Validate(name);
            return null;
        }

        private string Validate(string name)
        {
            switch (name)
            {
                case "PatientSocialSecurityNumber": return ValidatePatientSocialSecurityNumber();
                case "Room": return ValidateRoom();
                case "Bed": return ValidateBed();
                case "Comments": return ValidateComments();
                case "PatientName": return ValidatePatientName();
                case "Priority": return ValidatePriority();
                case "TaskDate": return ValidateTaskDate();
            }
            return null;
        }

        private string ValidatePatientSocialSecurityNumber()
        {
            if (PatientSocialSecurityNumber == null) return "PatientSocialSecurityNumber can not be null";
            return null;
        }

        private string ValidateRoom()
        {
            if (Room == null) return "Room can not be null";
            return null;
        }

        private string ValidateBed()
        {
            if (Bed == null) return "Bed can not be null";
            return null;
        }

        private string ValidateComments()
        {
            if (Comments == null) return "Comments can not be null";
            return null;
        }

        private string ValidatePatientName()
        {
            if (PatientName == null) return "PatientName can not be null";
            return null;
        }

        private string ValidatePriority()
        {
            if (Priority < 0) return "Priority must be a positive number";
            return null;
        }

        private string ValidateTaskDate()
        {
            if (TaskDate == default(DateTime)) return "TaskDate can not be default";
            return null;
        }
    }
}
