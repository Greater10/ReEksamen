using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DeltaProject.Model;
using DeltaProject.DataAccess;
using DeltaProject.Utilities;
using DeltaProject.View;

namespace DeltaProject.ViewModel
{
    public class TasksViewModel : ViewModelBase
    {
        public ObservableCollection<Task> Tasks { get; set; }
        public ObservableCollection<string> Departments { get; set; }
        public ObservableCollection<string> AssignedToList { get; set; }

        private ObservableCollection<string> _selectedDepartments;
        public ObservableCollection<string> SelectedDepartments
        {
            get { return _selectedDepartments; }
            set
            {
                _selectedDepartments = value;
                OnPropertyChanged(nameof(SelectedDepartments));
                FilterTasks();
            }
        }

        private ObservableCollection<string> _selectedAssignedTo;
        public ObservableCollection<string> SelectedAssignedTo
        {
            get { return _selectedAssignedTo; }
            set
            {
                _selectedAssignedTo = value;
                OnPropertyChanged(nameof(SelectedAssignedTo));
                FilterTasks();
            }
        }

        private bool _filterPriorityNormal;
        public bool FilterPriorityNormal
        {
            get { return _filterPriorityNormal; }
            set
            {
                _filterPriorityNormal = value;
                OnPropertyChanged(nameof(FilterPriorityNormal));
                FilterTasks();
            }
        }

        private bool _filterPriorityUrgent;
        public bool FilterPriorityUrgent
        {
            get { return _filterPriorityUrgent; }
            set
            {
                _filterPriorityUrgent = value;
                OnPropertyChanged(nameof(FilterPriorityUrgent));
                FilterTasks();
            }
        }

        private bool _filterPriorityCritical;
        public bool FilterPriorityCritical
        {
            get { return _filterPriorityCritical; }
            set
            {
                _filterPriorityCritical = value;
                OnPropertyChanged(nameof(FilterPriorityCritical));
                FilterTasks();
            }
        }

        private bool _filterBlodtest;
        public bool FilterBlodtest
        {
            get { return _filterBlodtest; }
            set
            {
                _filterBlodtest = value;
                OnPropertyChanged(nameof(FilterBlodtest));
                FilterTasks();
            }
        }

        private bool _filterEKG;
        public bool FilterEKG
        {
            get { return _filterEKG; }
            set
            {
                _filterEKG = value;
                OnPropertyChanged(nameof(FilterEKG));
                FilterTasks();
            }
        }

        private bool _filterGlukosisCSV;
        public bool FilterGlukosisCSV
        {
            get { return _filterGlukosisCSV; }
            set
            {
                _filterGlukosisCSV = value;
                OnPropertyChanged(nameof(FilterGlukosisCSV));
                FilterTasks();
            }
        }

        private bool _filterPROCPCR;
        public bool FilterPROCPCR
        {
            get { return _filterPROCPCR; }
            set
            {
                _filterPROCPCR = value;
                OnPropertyChanged(nameof(FilterPROCPCR));
                FilterTasks();
            }
        }

        public ICommand CreateTaskCommand { get; set; }
        public ICommand ResetDepartmentsFilterCommand { get; set; }

        public static DepartmentRepository departmentRepository = new DepartmentRepository();
        public static EmployeeRepository employeeRepository = new EmployeeRepository();
        public static TaskRepository taskRepository = new TaskRepository();

        public TasksViewModel()
        {
            Tasks = new ObservableCollection<Task>();

            departmentRepository.RepositoryChanged += RefreshDepartments;
            departmentRepository.GetAll();

            employeeRepository.RepositoryChanged += RefreshAssignedToList;
            employeeRepository.GetAll();

            taskRepository.RepositoryChanged += RefreshTasks;
            taskRepository.GetAll();

            _selectedDepartments = new ObservableCollection<string>();
            _selectedDepartments.CollectionChanged += (s, e) => FilterTasks();

            _selectedAssignedTo = new ObservableCollection<string>();
            _selectedAssignedTo.CollectionChanged += (s, e) => FilterTasks();

            CreateTaskCommand = new RelayCommand(CreateTask);
            ResetDepartmentsFilterCommand = new RelayCommand(ResetDepartmentsFilter);
        }

        private void RefreshDepartments(object sender, DbEventArgs e)
        {
            Departments = new ObservableCollection<string>(departmentRepository.Select(d => d.Name));
            OnPropertyChanged(nameof(Departments));
        }

        private void RefreshAssignedToList(object sender, DbEventArgs e)
        {
            AssignedToList = new ObservableCollection<string>(employeeRepository.Select(em => em.Name));
            OnPropertyChanged(nameof(AssignedToList));
        }

        private void RefreshTasks(object sender, DbEventArgs e)
        {
            Tasks = new ObservableCollection<Task>(taskRepository);
            OnPropertyChanged(nameof(Tasks));
        }

        private void FilterTasks()
        {
            var filter = new TaskFilter();

            // If any department is selected, add the corresponding IDs to the filter
            if (SelectedDepartments.Any())
            {
                filter.DepartmentIds = departmentRepository
                    .Where(d => SelectedDepartments.Contains(d.Name))
                    .Select(d => d.DepartmentId)
                    .ToList();
            }

            // If any employee is selected, add the corresponding IDs to the filter
            if (SelectedAssignedTo.Any())
            {
                filter.EmployeeIds = employeeRepository
                    .Where(em => SelectedAssignedTo.Contains(em.Name))
                    .Select(em => em.EmployeeId)
                    .ToList();
            }

            // Apply priority filters
            if (FilterPriorityNormal)
            {
                filter.Priorities.Add(1);
            }
            if (FilterPriorityUrgent)
            {
                filter.Priorities.Add(2);
            }
            if (FilterPriorityCritical)
            {
                filter.Priorities.Add(3);
            }

            // Apply test filters
            if (FilterBlodtest) filter.TestTypes.Add((int)TestType.Bloodtest);
            if (FilterEKG) filter.TestTypes.Add((int)TestType.EKG);
            if (FilterGlukosisCSV) filter.TestTypes.Add((int)TestType.GlukoseCsv);
            if (FilterPROCPCR) filter.TestTypes.Add((int)TestType.ProcPcr);

            // Use the TaskRepository's Search method to filter tasks based on the criteria
            var filteredTasks = taskRepository.Search(filter);

            // Update the Tasks collection
            Tasks.Clear();
            foreach (var task in filteredTasks)
            {
                Tasks.Add(task);
            }
        }

        private void CreateTask(object parameter)
        {
            WindowManager.OpenWindow<CreateTaskWindow, CreateTaskViewModel>(new CreateTaskViewModel());
        }

        private void ResetDepartmentsFilter(object parameter)
        {
            SelectedDepartments.Clear();
        }
    }
}
