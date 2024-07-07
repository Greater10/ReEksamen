using System.Collections.Generic;

namespace DeltaProject.Model
{
    public class TaskFilter
    {
        public List<int> DepartmentIds { get; set; } = new List<int>();
        public List<int> EmployeeIds { get; set; } = new List<int>();
        public List<int> Priorities { get; set; } = new List<int>();
        public List<int> TestTypes { get; set; } = new List<int>();
    }
}
