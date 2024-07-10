using DeltaProject.DataAccess;
using DeltaProject.Utilities;
using DeltaProject.View;
using DeltaProject.Model;
using System;
using System.Collections.ObjectModel;
using DeltaProject.Services;
using System.Linq;

namespace DeltaProject.ViewModel
{
    public class TaskDetailsViewModel : ViewModelBase
    {
        private TaskRepository taskRepository = new TaskRepository();
        private UserService userService = UserService.Instance;
        private Task currentTask;

        public string PatientName { get; set; }
        public string PatientSocialSecurityNumber { get; set; }
        public string SelectedDepartment { get; set; }
        public string Room { get; set; }
        public string Bed { get; set; }
        public bool Isolated { get; set; }
        public bool SpecialMedication { get; set; }
        public bool Inactive { get; set; }
        public int Priority { get; set; }
        public DateTime TaskDate { get; set; }
        public string Comments { get; set; }

        // Test properties
        public bool BloodTest { get; set; }
        public bool EKG { get; set; }
        public bool GlukoseCSV { get; set; }
        public bool POCTPCR { get; set; }
        public bool SpecialObservation { get; set; }
        public bool MedicineDependentTest { get; set; }

        public RelayCommand AssignToMeCommand { get; private set; }
        public RelayCommand UnassignCommand { get; private set; }
        public RelayCommand CloseWindowCommand { get; private set; }

        public TaskDetailsViewModel(Task task)
        {
            currentTask = task;
            LoadTaskDetails(task);

            AssignToMeCommand = new RelayCommand(p => AssignToMe(), p => CanAssignToMe());
            UnassignCommand = new RelayCommand(p => Unassign(), p => CanUnassign());
            CloseWindowCommand = new RelayCommand(p => CloseWindow());
        }

        private void LoadTaskDetails(Task task)
        {
            PatientName = task.PatientName;
            PatientSocialSecurityNumber = task.PatientSocialSecurityNumber;
            SelectedDepartment = task.Department?.Name;
            Room = task.Room;
            Bed = task.Bed;
            Isolated = task.Isolated;
            SpecialMedication = task.SpecialMedication;
            Inactive = task.Inactive;
            Priority = task.Priority;
            TaskDate = task.TaskDate;
            Comments = task.Comments;

            BloodTest = task.Tests.Any(t => t.TestType == (int)TestType.Bloodtest);
            EKG = task.Tests.Any(t => t.TestType == (int)TestType.EKG);
            GlukoseCSV = task.Tests.Any(t => t.TestType == (int)TestType.GlukoseCsv);
            POCTPCR = task.Tests.Any(t => t.TestType == (int)TestType.ProcPcr);
            // SpecialObservation = task.s
            // MedicineDependentTest = task.Tests.Any(t => t.TestType == (int)TestType.MedicineDependentTest);
        }

        private void AssignToMe()
        {
            var currentUser = userService.UserId;
            currentTask.EmployeeId = currentUser;
            taskRepository.Update(currentTask);
            WindowManager.CloseWindow<TaskDetailsWindow>();
        }

        private void Unassign()
        {
            currentTask.EmployeeId = null;
            taskRepository.Update(currentTask);
            WindowManager.CloseWindow<TaskDetailsWindow>();
        }

        private bool CanUnassign()
        {
            return currentTask.EmployeeId == userService.UserId;
        }

        private bool CanAssignToMe()
        {
            return currentTask.EmployeeId == null;
        }

        private void CloseWindow()
        {
            WindowManager.CloseWindow<TaskDetailsWindow>();
        }
    }
}
