﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DeltaProject.Model;
using DeltaProject.DataAccess;

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

        public ICommand AddTaskCommand { get; set; }
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

            AddTaskCommand = new RelayCommand(AddTask);
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
            // Implement task filtering logic based on selected departments and assigned to
        }

        private void AddTask(object parameter)
        {
            // Add task logic
        }

        private void ResetDepartmentsFilter(object parameter)
        {
            SelectedDepartments.Clear();
        }
    }
}
