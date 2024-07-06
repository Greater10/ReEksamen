using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using DeltaProject.Model;
using DeltaProject.Utilities;

namespace DeltaProject.ViewModel
{
    public class TasksViewModel : ViewModelBase
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public ObservableCollection<string> Departments { get; set; }
        public ObservableCollection<string> AssignedToList { get; set; }

        private string selectedDepartment;
        public string SelectedDepartment
        {
            get => selectedDepartment;
            set
            {
                selectedDepartment = value;
                OnPropertyChanged("SelectedDepartment");
            }
        }

        private string selectedAssignedTo;
        public string SelectedAssignedTo
        {
            get => selectedAssignedTo;
            set
            {
                selectedAssignedTo = value;
                OnPropertyChanged("SelectedAssignedTo");
            }
        }

        public bool FilterPriorityNormal { get; set; }
        public bool FilterPriorityUrgent { get; set; }
        public bool FilterPriorityCritical { get; set; }
        public bool FilterBlodtest { get; set; }
        public bool FilterEKG { get; set; }
        public bool FilterGlukosisCSV { get; set; }
        public bool FilterPROCPCR { get; set; }

        public ICommand AddTaskCommand { get; private set; }

        public TasksViewModel()
        {
            // Initialize collections and commands
            Tasks = new ObservableCollection<Task>
            {
                new Task { TaskId = 1, PatientName = "Jens Jensen", TaskDate = DateTime.Now, DepartmentName = "A1, mave-tarm-kirurgisk", Room = "101", Priority = 1, AssignedEmployeeName = "Dr. Hansen" },
                new Task { TaskId = 2, PatientName = "Mette Madsen", TaskDate = DateTime.Now.AddHours(1), DepartmentName = "S1, hjertemedicinsk", Room = "102", Priority = 2, AssignedEmployeeName = "Dr. Nielsen" },
                new Task { TaskId = 3, PatientName = "Søren Sørensen", TaskDate = DateTime.Now.AddHours(2), DepartmentName = "NHH, neuro-, hoved-, halskirurgisk", Room = "103", Priority = 3, AssignedEmployeeName = "Dr. Pedersen" },
                new Task { TaskId = 4, PatientName = "Pia Petersen", TaskDate = DateTime.Now.AddHours(3), DepartmentName = "O1, ortopædkirurgisk", Room = "104", Priority = 1, AssignedEmployeeName = "Dr. Rasmussen" },
                new Task { TaskId = 5, PatientName = "Ole Olesen", TaskDate = DateTime.Now.AddHours(4), DepartmentName = "Geriatrisk, ældremedicinsk", Room = "105", Priority = 2, AssignedEmployeeName = "Dr. Thomsen" },
                new Task { TaskId = 6, PatientName = "Lise Larsen", TaskDate = DateTime.Now.AddHours(5), DepartmentName = "A2, mave-tarm-kirurgisk", Room = "106", Priority = 3, AssignedEmployeeName = "Dr. Kristensen" },
                new Task { TaskId = 7, PatientName = "Hans Hansen", TaskDate = DateTime.Now.AddHours(6), DepartmentName = "T, Thoraxkirurgisk", Room = "107", Priority = 1, AssignedEmployeeName = "Dr. Hansen" },
                new Task { TaskId = 8, PatientName = "Karen Karlsen", TaskDate = DateTime.Now.AddHours(7), DepartmentName = "Notia, neuro- og traume-intensiv", Room = "108", Priority = 2, AssignedEmployeeName = "Dr. Nielsen" },
                new Task { TaskId = 9, PatientName = "Per Pedersen", TaskDate = DateTime.Now.AddHours(8), DepartmentName = "ATC, akut traume center/skadestuen", Room = "109", Priority = 3, AssignedEmployeeName = "Dr. Pedersen" },
                new Task { TaskId = 10, PatientName = "Birgitte Berg", TaskDate = DateTime.Now.AddHours(9), DepartmentName = "Grønt spor, patienter hvor det ikke haster, men de skal ses af læge", Room = "110", Priority = 1, AssignedEmployeeName = "Dr. Rasmussen" },
                new Task { TaskId = 11, PatientName = "Niels Nielsen", TaskDate = DateTime.Now.AddHours(10), DepartmentName = "Blåt spor, brækkede knogler", Room = "111", Priority = 2, AssignedEmployeeName = "Dr. Thomsen" },
                new Task { TaskId = 12, PatientName = "Ann Andersen", TaskDate = DateTime.Now.AddHours(11), DepartmentName = "AMA 1, akutmodtageafsnit", Room = "112", Priority = 3, AssignedEmployeeName = "Dr. Kristensen" },
                new Task { TaskId = 13, PatientName = "Kurt Knudsen", TaskDate = DateTime.Now.AddHours(12), DepartmentName = "AMA 2, akutmodtageafsnit", Room = "113", Priority = 1, AssignedEmployeeName = "Dr. Hansen" },
                new Task { TaskId = 14, PatientName = "Inger Iversen", TaskDate = DateTime.Now.AddHours(13), DepartmentName = "9Ø, gastromedicinsk", Room = "114", Priority = 2, AssignedEmployeeName = "Dr. Nielsen" },
                new Task { TaskId = 15, PatientName = "Bodil Bjerg", TaskDate = DateTime.Now.AddHours(14), DepartmentName = "6V, lungemedicinsk", Room = "115", Priority = 3, AssignedEmployeeName = "Dr. Pedersen" },
                new Task { TaskId = 16, PatientName = "Lars Larsen", TaskDate = DateTime.Now.AddHours(15), DepartmentName = "7V, hæmatologisk", Room = "116", Priority = 1, AssignedEmployeeName = "Dr. Rasmussen" },
                new Task { TaskId = 17, PatientName = "Grethe Gade", TaskDate = DateTime.Now.AddHours(16), DepartmentName = "7Ø, infektionsmedicinsk", Room = "117", Priority = 2, AssignedEmployeeName = "Dr. Thomsen" },
                new Task { TaskId = 18, PatientName = "Bjarne Bundgaard", TaskDate = DateTime.Now.AddHours(17), DepartmentName = "6Ø, apopleksi", Room = "118", Priority = 3, AssignedEmployeeName = "Dr. Kristensen" },
                new Task { TaskId = 19, PatientName = "Anette Andersen", TaskDate = DateTime.Now.AddHours(18), DepartmentName = "2Ø, intermediært", Room = "119", Priority = 1, AssignedEmployeeName = "Dr. Hansen" }
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
