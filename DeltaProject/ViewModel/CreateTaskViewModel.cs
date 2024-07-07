using DeltaProject.DataAccess;
using DeltaProject.Utilities;
using DeltaProject.View;
using DeltaProject.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DeltaProject.ViewModel
{
    public class CreateTaskViewModel : ViewModelBase
    {
        private TaskRepository taskRepository = new TaskRepository();
        private DepartmentRepository departmentRepository = new DepartmentRepository();
        private EmployeeRepository employeeRepository = new EmployeeRepository();

        public ObservableCollection<string> Departments { get; set; }
        public ObservableCollection<string> Employees { get; set; }

        public string SelectedDepartment { get; set; }
        public string SelectedEmployee { get; set; }
        public string PatientName { get; set; }
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
        public DateTime TaskDate { get; set; } = DateTime.Now;
        public string Comments { get; set; }

        public RelayCommand CreateCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public CreateTaskViewModel()
        {
            departmentRepository.GetAll();
            employeeRepository.GetAll();

            Departments = new ObservableCollection<string>(departmentRepository.Select(d => d.Name));
            Employees = new ObservableCollection<string>(employeeRepository.Select(e => e.Name));

            CreateCommand = new RelayCommand(p => Create());
            CancelCommand = new RelayCommand(p => Cancel());
        }

        private void Cancel()
        {
            WindowManager.CloseWindow<CreateTaskWindow>();
        }

        private void Create()
        {
            var selectedDepartment = departmentRepository.FirstOrDefault(d => d.Name == SelectedDepartment);
            var selectedEmployee = employeeRepository.FirstOrDefault(e => e.Name == SelectedEmployee);

            Task task = new Task
            {
                PatientName = PatientName,
                PatientSocialSecurityNumber = PatientSocialSecurityNumber,
                Room = Room,
                Bed = Bed,
                Isolated = Isolated,
                Deaf = Deaf,
                Mute = Mute,
                Inactive = Inactive,
                ForeignLanguage = ForeignLanguage,
                SpecialMedication = SpecialMedication,
                Priority = Priority,
                TaskDate = TaskDate,
                Comments = Comments,
                DepartmentId = selectedDepartment?.DepartmentId ?? 0,
                EmployeeId = selectedEmployee?.EmployeeId
            };

            taskRepository.Add(task);

            WindowManager.CloseWindow<CreateTaskWindow>();
        }
    }
}
