using DeltaProject.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DeltaProject.DataAccess
{
    public class TaskRepository : Repository, IEnumerable<Task>
    {
        private List<Task> list = new List<Task>();
        private DepartmentRepository departmentRepository;
        private EmployeeRepository employeeRepository;
        private TestRepository testRepository;

        public TaskRepository()
        {
            departmentRepository = new DepartmentRepository();
            employeeRepository = new EmployeeRepository();
            testRepository = new TestRepository();
        }

        public IEnumerator<Task> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void GetAll()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT TaskId, PatientSocialSecurityNumber, Room, Bed, Isolated, Deaf, Mute, Inactive, ForeignLanguage, SpecialMedication, Priority, TaskDate, Comments, PatientName, DepartmentId, EmployeeId FROM Task", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    var task = new Task(
                        reader.GetInt32(reader.GetOrdinal("TaskId")),
                        reader.IsDBNull(reader.GetOrdinal("PatientSocialSecurityNumber")) ? null : reader["PatientSocialSecurityNumber"].ToString(),
                        reader.IsDBNull(reader.GetOrdinal("Room")) ? null : reader["Room"].ToString(),
                        reader.IsDBNull(reader.GetOrdinal("Bed")) ? null : reader["Bed"].ToString(),
                        reader.IsDBNull(reader.GetOrdinal("Isolated")) ? false : reader.GetBoolean(reader.GetOrdinal("Isolated")),
                        reader.IsDBNull(reader.GetOrdinal("Deaf")) ? false : reader.GetBoolean(reader.GetOrdinal("Deaf")),
                        reader.IsDBNull(reader.GetOrdinal("Mute")) ? false : reader.GetBoolean(reader.GetOrdinal("Mute")),
                        reader.IsDBNull(reader.GetOrdinal("Inactive")) ? false : reader.GetBoolean(reader.GetOrdinal("Inactive")),
                        reader.IsDBNull(reader.GetOrdinal("ForeignLanguage")) ? false : reader.GetBoolean(reader.GetOrdinal("ForeignLanguage")),
                        reader.IsDBNull(reader.GetOrdinal("SpecialMedication")) ? false : reader.GetBoolean(reader.GetOrdinal("SpecialMedication")),
                        reader.IsDBNull(reader.GetOrdinal("Priority")) ? 0 : reader.GetInt32(reader.GetOrdinal("Priority")),
                        reader.IsDBNull(reader.GetOrdinal("TaskDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("TaskDate")),
                        reader.IsDBNull(reader.GetOrdinal("Comments")) ? null : reader["Comments"].ToString(),
                        reader.IsDBNull(reader.GetOrdinal("PatientName")) ? null : reader["PatientName"].ToString(),
                        reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? 0 : reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                        reader.IsDBNull(reader.GetOrdinal("EmployeeId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                        new List<Test>()
                    );
                    PopulateReferences(task);
                    list.Add(task);
                }
                OnChanged(DbOperation.SELECT, DbModeltype.Task);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Task repository: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        public List<Task> Search(TaskFilter filter)
        {
            // Retrieve all tasks
            GetAll();

            // Apply in-memory filtering
            var filteredTasks = list.AsQueryable();

            if (filter.DepartmentIds.Any())
            {
                filteredTasks = filteredTasks.Where(t => filter.DepartmentIds.Contains(t.DepartmentId));
            }

            if (filter.EmployeeIds.Any())
            {
                filteredTasks = filteredTasks.Where(t => t.EmployeeId != null && filter.EmployeeIds.Contains((int)t.EmployeeId));
            }

            if (filter.Priorities.Any())
            {
                filteredTasks = filteredTasks.Where(t => filter.Priorities.Contains(t.Priority));
            }

            if (filter.TestTypes.Any())
            {
                filteredTasks = filteredTasks.Where(t => t.Tests.Any(test => filter.TestTypes.Contains(test.TestType)));
            }

            return filteredTasks.ToList();
        }

        private void PopulateReferences(Task task)
        {
            if (task.DepartmentId > 0)
            {
                task.Department = departmentRepository.GetById(task.DepartmentId);
            }

            if (task.EmployeeId != null)
            {
                task.AssignedTo = employeeRepository.GetById((int)task.EmployeeId);
            }

            // Fetch and assign tests
            testRepository.GetByTask(task.TaskId);
            foreach (var test in testRepository)
            {
                task.Tests.Add(test);
            }
        }

        public void Add(Task task)
        {
            string error = "";
            if (task.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Task (PatientSocialSecurityNumber, Room, Bed, Isolated, Deaf, Mute, Inactive, ForeignLanguage, SpecialMedication, Priority, TaskDate, Comments, PatientName, DepartmentId, EmployeeId) VALUES (@PatientSocialSecurityNumber, @Room, @Bed, @Isolated, @Deaf, @Mute, @Inactive, @ForeignLanguage, @SpecialMedication, @Priority, @TaskDate, @Comments, @PatientName, @DepartmentId, @EmployeeId); SELECT SCOPE_IDENTITY()", connection);
                    command.Parameters.Add(CreateParam("@PatientSocialSecurityNumber", task.PatientSocialSecurityNumber, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Room", task.Room, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Bed", task.Bed, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Isolated", task.Isolated, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@Deaf", task.Deaf, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@Mute", task.Mute, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@Inactive", task.Inactive, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@ForeignLanguage", task.ForeignLanguage, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@SpecialMedication", task.SpecialMedication, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@Priority", task.Priority, SqlDbType.Int));
                    command.Parameters.Add(CreateParam("@TaskDate", task.TaskDate, SqlDbType.DateTime));
                    command.Parameters.Add(CreateParam("@Comments", task.Comments, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@PatientName", task.PatientName, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@DepartmentId", task.DepartmentId, SqlDbType.Int));
                    command.Parameters.Add(CreateParam("@EmployeeId", task.EmployeeId.HasValue ? (object)task.EmployeeId.Value : DBNull.Value, SqlDbType.Int));
                    connection.Open();
                    int newTaskId = Convert.ToInt32(command.ExecuteScalar());
                    task.TaskId = newTaskId;

                    // Add tests
                    foreach (var test in task.Tests)
                    {
                        test.TaskId = newTaskId;
                        testRepository.Add(test);
                    }

                    list.Add(task);
                    list.Sort();
                    OnChanged(DbOperation.INSERT, DbModeltype.Task);
                    return;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
            }
            else
            {
                error = "Illegal value for task";
            }
            throw new DbException("Error in Task repository: " + error);
        }

        public void Update(Task task)
        {
            string error = "";
            if (task.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Task SET PatientSocialSecurityNumber = @PatientSocialSecurityNumber, Room = @Room, Bed = @Bed, Isolated = @Isolated, Deaf = @Deaf, Mute = @Mute, Inactive = @Inactive, ForeignLanguage = @ForeignLanguage, SpecialMedication = @SpecialMedication, Priority = @Priority, TaskDate = @TaskDate, Comments = @Comments, PatientName = @PatientName, DepartmentId = @DepartmentId, EmployeeId = @EmployeeId WHERE TaskId = @TaskId", connection);
                    command.Parameters.Add(CreateParam("@TaskId", task.TaskId, SqlDbType.Int));
                    command.Parameters.Add(CreateParam("@PatientSocialSecurityNumber", task.PatientSocialSecurityNumber, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Room", task.Room, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Bed", task.Bed, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Isolated", task.Isolated, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@Deaf", task.Deaf, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@Mute", task.Mute, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@Inactive", task.Inactive, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@ForeignLanguage", task.ForeignLanguage, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@SpecialMedication", task.SpecialMedication, SqlDbType.Bit));
                    command.Parameters.Add(CreateParam("@Priority", task.Priority, SqlDbType.Int));
                    command.Parameters.Add(CreateParam("@TaskDate", task.TaskDate, SqlDbType.DateTime));
                    command.Parameters.Add(CreateParam("@Comments", task.Comments, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@PatientName", task.PatientName, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@DepartmentId", task.DepartmentId, SqlDbType.Int));
                    command.Parameters.Add(CreateParam("@EmployeeId", task.EmployeeId.HasValue ? (object)task.EmployeeId.Value : DBNull.Value, SqlDbType.Int));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        // Update tests
                        var existingTests = testRepository.Where(t => t.TaskId == task.TaskId).ToList();
                        foreach (var test in task.Tests)
                        {
                            if (!existingTests.Any(t => t.TestType == test.TestType))
                            {
                                testRepository.Add(test);
                            }
                        }
                        foreach (var test in existingTests)
                        {
                            if (!task.Tests.Any(t => t.TestType == test.TestType))
                            {
                                testRepository.Remove(test);
                            }
                        }

                        UpdateList(task);
                        OnChanged(DbOperation.UPDATE, DbModeltype.Task);
                        return;
                    }
                    error = "Task could not be updated";
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
            }
            else
            {
                error = "Illegal value for task";
            }
            throw new DbException("Error in Task repository: " + error);
        }

        private void UpdateList(Task task)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].TaskId.Equals(task.TaskId))
                {
                    list[i].PatientSocialSecurityNumber = task.PatientSocialSecurityNumber;
                    list[i].Room = task.Room;
                    list[i].Bed = task.Bed;
                    list[i].Isolated = task.Isolated;
                    list[i].Deaf = task.Deaf;
                    list[i].Mute = task.Mute;
                    list[i].Inactive = task.Inactive;
                    list[i].ForeignLanguage = task.ForeignLanguage;
                    list[i].SpecialMedication = task.SpecialMedication;
                    list[i].Priority = task.Priority;
                    list[i].TaskDate = task.TaskDate;
                    list[i].Comments = task.Comments;
                    list[i].PatientName = task.PatientName;
                    list[i].DepartmentId = task.DepartmentId;
                    list[i].EmployeeId = task.EmployeeId;
                    PopulateReferences(list[i]);
                    break;
                }
            }
        }

        public void Remove(Task task)
        {
            string error = "";
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Task WHERE TaskId = @TaskId", connection);
                command.Parameters.Add(CreateParam("@TaskId", task.TaskId, SqlDbType.Int));
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    // Remove tests
                    var existingTests = testRepository.Where(t => t.TaskId == task.TaskId).ToList();
                    foreach (var test in existingTests)
                    {
                        testRepository.Remove(test);
                    }

                    list.Remove(task);
                    OnChanged(DbOperation.DELETE, DbModeltype.Task);
                    return;
                }
                error = "Task could not be deleted";
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            throw new DbException("Error in Task repository: " + error);
        }
    }
}
