using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DeltaProject.Model
{
    public class Test : IDataErrorInfo, IComparable<Test>
    {
        public int TestId { get; set; }
        public int TestType { get; set; }
        public int TaskId { get; set; }

        public Test()
        {
            // Initialize properties with default values if needed
        }

        public Test(int testId, int testType, int taskId)
        {
            TestId = testId;
            TestType = testType;
            TaskId = taskId;
        }

        // Implement comparison on TestId
        public override bool Equals(object obj)
        {
            try
            {
                Test test = (Test)obj;
                return TestId.Equals(test.TestId);
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return TestId.GetHashCode();
        }

        // Implement comparison on TaskId
        public int CompareTo(Test test)
        {
            return TaskId.CompareTo(test.TaskId);
        }

        // Validation properties
        private static readonly string[] validatedProperties = { "TestType", "TaskId" };

        public bool IsValid
        {
            get
            {
                foreach (string property in validatedProperties)
                {
                    if (GetError(property) != null) return false;
                }
                return true;
            }
        }

        string IDataErrorInfo.Error
        {
            get { return IsValid ? null : "Illegal Test object"; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return Validate(propertyName); }
        }

        private string GetError(string name)
        {
            foreach (string property in validatedProperties)
            {
                if (property.Equals(name)) return Validate(name);
            }
            return null;
        }

        private string Validate(string name)
        {
            switch (name)
            {
                case "TestType": return ValidateTestType();
                case "TaskId": return ValidateTaskId();
                default: return null;
            }
        }

        private string ValidateTestType()
        {
            if (TestType <= 0) return "TestType must be a positive number";
            return null;
        }

        private string ValidateTaskId()
        {
            if (TaskId <= 0) return "TaskId must be a positive number";
            return null;
        }
    }
}

