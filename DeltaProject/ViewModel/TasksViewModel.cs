using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DeltaProject.Model;

namespace DeltaProject.ViewModel
{
    public class TasksViewModel : ViewModelBase
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public ObservableCollection<string> Departments { get; set; }
        public ObservableCollection<string> AssignedToList { get; set; }

        public ICommand AddTaskCommand { get; set; }

        public TasksViewModel()
        {
            Tasks = new ObservableCollection<Task>
            {
                new Task
                {
                    TaskId = 1, PatientName = "Jens Jensen", TaskDate = DateTime.Now, DepartmentName = "A1, mave-tarm-kirurgisk", Room = "101", Priority = 1, AssignedEmployeeName = "Dr. Hansen",
                    Tests = new List<Test>
                    {
                        new Test { TestId = 1, TestType = (int)TestType.Bloodtest, TaskId = 1 },
                        new Test { TestId = 2, TestType = (int)TestType.EKG, TaskId = 1 },
                        new Test { TestId = 3, TestType = (int)TestType.ProcPcr, TaskId = 1 }
                    }
                },
                new Task
                {
                    TaskId = 2, PatientName = "Mette Madsen", TaskDate = DateTime.Now.AddHours(1), DepartmentName = "S1, hjertemedicinsk", Room = "102", Priority = 2, AssignedEmployeeName = "Dr. Nielsen",
                    Tests = new List<Test>
                    {
                        new Test { TestId = 4, TestType = (int)TestType.EKG, TaskId = 2 },
                        new Test { TestId = 5, TestType = (int)TestType.GlukoseCsv, TaskId = 2 }
                    }
                },
                new Task
                {
                    TaskId = 3, PatientName = "Søren Sørensen", TaskDate = DateTime.Now.AddHours(2), DepartmentName = "NHH, neuro-, hoved-, halskirurgisk", Room = "103", Priority = 3, AssignedEmployeeName = "Dr. Pedersen",
                    Tests = new List<Test>
                    {
                        new Test { TestId = 6, TestType = (int)TestType.Bloodtest, TaskId = 3 },
                        new Test { TestId = 7, TestType = (int)TestType.GlukoseCsv, TaskId = 3 }
                    }
                },
                new Task
                {
                    TaskId = 4, PatientName = "Pia Petersen", TaskDate = DateTime.Now.AddHours(3), DepartmentName = "O1, ortopædkirurgisk", Room = "104", Priority = 1, AssignedEmployeeName = "Dr. Rasmussen",
                    Tests = new List<Test>
                    {
                        new Test { TestId = 8, TestType = (int)TestType.ProcPcr, TaskId = 4 }
                    }
                },
                // Add more tasks as needed...
            };

            Departments = new ObservableCollection<string> { "", "X", "Y", "Z" };
            AssignedToList = new ObservableCollection<string> { "", "Ikke tildelt", "Alice", "Bob", "Charlie" };

            AddTaskCommand = new RelayCommand(AddTask);
        }

        private void AddTask(object parameter)
        {
            // Add task logic
        }
    }
}
