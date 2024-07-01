using DeltaProject.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DeltaProject.DataAccess
{
    public class DepartmentRepository : Repository, IEnumerable<Department>
    {
        private List<Department> list = new List<Department>();

        public IEnumerator<Department> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Search(int departmentId)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT DepartmentId, Name, LocationId FROM Department WHERE DepartmentId = @DepartmentId", connection);
                command.Parameters.Add(CreateParam("@DepartmentId", departmentId, SqlDbType.Int));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                    list.Add(new Department((int)reader["DepartmentId"], reader["Name"].ToString(), (int)reader["LocationId"]));
                OnChanged(DbOperation.SELECT, DbModeltype.Contact);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Department repository: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        public void Add(Department department)
        {
            string error = "";
            if (department.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Department (Name, LocationId) VALUES (@Name, @LocationId)", connection);
                    command.Parameters.Add(CreateParam("@Name", department.Name, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@LocationId", department.LocationId, SqlDbType.Int));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        list.Add(department);
                        list.Sort();
                        OnChanged(DbOperation.INSERT, DbModeltype.Contact);
                        return;
                    }
                    error = string.Format("Department could not be inserted in the database");
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
            else error = "Illegal value for department";
            throw new DbException("Error in Department repository: " + error);
        }

        public void Update(Department department)
        {
            string error = "";
            if (department.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Department SET Name = @Name, LocationId = @LocationId WHERE DepartmentId = @DepartmentId", connection);
                    command.Parameters.Add(CreateParam("@Name", department.Name, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@LocationId", department.LocationId, SqlDbType.Int));
                    command.Parameters.Add(CreateParam("@DepartmentId", department.DepartmentId, SqlDbType.Int));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        UpdateList(department);
                        OnChanged(DbOperation.UPDATE, DbModeltype.Contact);
                        return;
                    }
                    error = string.Format("Department could not be updated");
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
            else error = "Illegal value for department";
            throw new DbException("Error in Department repository: " + error);
        }

        private void UpdateList(Department department)
        {
            for (int i = 0; i < list.Count; ++i)
                if (list[i].DepartmentId.Equals(department.DepartmentId))
                {
                    list[i].Name = department.Name;
                    list[i].LocationId = department.LocationId;
                    break;
                }
        }

        public void Remove(Department department)
        {
            string error = "";
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Department WHERE DepartmentId = @DepartmentId", connection);
                command.Parameters.Add(CreateParam("@DepartmentId", department.DepartmentId, SqlDbType.Int));
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    list.Remove(department);
                    OnChanged(DbOperation.DELETE, DbModeltype.Contact);
                    return;
                }
                error = string.Format("Department could not be deleted");
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            throw new DbException("Error in Department repository: " + error);
        }
    }
}

