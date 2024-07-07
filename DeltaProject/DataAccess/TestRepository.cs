using DeltaProject.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DeltaProject.DataAccess
{
    public class TestRepository : Repository, IEnumerable<Test>
    {
        private List<Test> list = new List<Test>();

        public IEnumerator<Test> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void GetByTask(int taskId)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT TestId, TestType, TaskId FROM Test WHERE TaskId = @TaskId", connection);
                command.Parameters.Add(CreateParam("@TaskId", taskId, SqlDbType.Int));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    list.Add(new Test((int)reader[0], (int)reader[1], (int)reader[2]));
                }
                OnChanged(DbOperation.SELECT, DbModeltype.Contact);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Test repository: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        public void Add(Test test)
        {
            string error = "";
            if (test.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Test (TestType, TaskId) VALUES (@TestType, @TaskId); SELECT SCOPE_IDENTITY()", connection);
                    command.Parameters.Add(CreateParam("@TestType", test.TestType, SqlDbType.Int));
                    command.Parameters.Add(CreateParam("@TaskId", test.TaskId, SqlDbType.Int));
                    connection.Open();
                    int newTestId = Convert.ToInt32(command.ExecuteScalar());
                    test.TestId = newTestId;
                    list.Add(test);
                    list.Sort();
                    OnChanged(DbOperation.INSERT, DbModeltype.Contact);
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
                error = "Illegal value for test";
            }
            throw new DbException("Error in Test repository: " + error);
        }

        public void Update(Test test)
        {
            string error = "";
            if (test.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Test SET TestType = @TestType, TaskId = @TaskId WHERE TestId = @TestId", connection);
                    command.Parameters.Add(CreateParam("@TestType", test.TestType, SqlDbType.Int));
                    command.Parameters.Add(CreateParam("@TaskId", test.TaskId, SqlDbType.Int));
                    command.Parameters.Add(CreateParam("@TestId", test.TestId, SqlDbType.Int));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        UpdateList(test);
                        OnChanged(DbOperation.UPDATE, DbModeltype.Contact);
                        return;
                    }
                    error = "Test could not be updated";
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
                error = "Illegal value for test";
            }
            throw new DbException("Error in Test repository: " + error);
        }

        private void UpdateList(Test test)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].TestId.Equals(test.TestId))
                {
                    list[i].TestType = test.TestType;
                    list[i].TaskId = test.TaskId;
                    break;
                }
            }
        }

        public void Remove(Test test)
        {
            string error = "";
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Test WHERE TestId = @TestId", connection);
                command.Parameters.Add(CreateParam("@TestId", test.TestId, SqlDbType.Int));
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    list.Remove(test);
                    OnChanged(DbOperation.DELETE, DbModeltype.Contact);
                    return;
                }
                error = "Test could not be deleted";
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            throw new DbException("Error in Test repository: " + error);
        }
    }
}

