using DeltaProject.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DeltaProject.DataAccess
{
    public class EmployeeRepository : Repository, IEnumerable<Employee>
    {
        private List<Employee> list = new List<Employee>();

        public IEnumerator<Employee> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Search(string socialSecurityNumber)
        {
            try
            {
                // TODO - Omskriv så der søges på alle felter lige som i ContractRepository
                SqlCommand command = new SqlCommand("SELECT EmployeeId, Name, Address, WorkEmail, SocialSecurityNumber, Phone, WorkPhone, Password FROM Employee WHERE SocialSecurityNumber = @SocialSecurityNumber", connection);
                command.Parameters.Add(CreateParam("@SocialSecurityNumber", socialSecurityNumber, SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read()) list.Add(new Employee((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString()));
                OnChanged(DbOperation.SELECT, DbModeltype.Contact);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Employee repository: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        public void Add(Employee employee)
        {
            string error = "";
            if (employee.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Employee (Name, Address, WorkEmail, SocialSecurityNumber, Phone, WorkPhone, Password) VALUES (@Name, @Address, @WorkEmail, @SocialSecurityNumber, @Phone, @WorkPhone, @Password)", connection);
                    command.Parameters.Add(CreateParam("@Name", employee.Name, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Address", employee.Address, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@WorkEmail", employee.WorkEmail, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@SocialSecurityNumber", employee.SocialSecurityNumber, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Phone", employee.Phone, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@WorkPhone", employee.WorkPhone, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Password", employee.Password, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        // TODO - How to get autoinc id
                        list.Add(employee);
                        list.Sort();
                        OnChanged(DbOperation.INSERT, DbModeltype.Employee);
                        return;
                    }
                    error = string.Format("Employee could not be inserted in the database");
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
            else error = "Illegal value for employee";
            throw new DbException("Error in Employee repository: " + error);
        }
        
        public void Update(Employee employee)
        {
            string error = "";
            if (employee.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Employee SET Name = @Name, Address = @Address, WorkEmail = @WorkEmail, Phone = @Phone, WorkPhone = @WorkPhone, Password = @Password, SocialSecurityNumber = @SocialSecurityNumber WHERE EmployeeId = @EmployeeId", connection);
                    command.Parameters.Add(CreateParam("@Name", employee.Name, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Address", employee.Address, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@WorkEmail", employee.WorkEmail, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Phone", employee.Phone, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@WorkPhone", employee.WorkPhone, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Password", employee.Password, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@SocialSecurityNumber", employee.SocialSecurityNumber, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        UpdateList(employee);
                        OnChanged(DbOperation.UPDATE, DbModeltype.Employee);
                        return;
                    }
                    error = string.Format("Contact could not be updated");
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
            else error = "Illegal value for employee";
            throw new DbException("Error in Employee repository: " + error);
        }

        private void UpdateList(Employee employee)
        {
            for (int i = 0; i < list.Count; ++i)
                if (list[i].EmployeeId.Equals(employee.EmployeeId))
                {
                    list[i].Name = employee.Name;
                    list[i].Address = employee.Address;
                    list[i].WorkEmail = employee.WorkEmail;
                    list[i].Phone = employee.Phone;
                    list[i].WorkPhone = employee.WorkPhone;
                    list[i].Password = employee.Password;
                    list[i].SocialSecurityNumber = employee.SocialSecurityNumber;
                    break;
                }
        }

        public void Remove(Employee employee)
        {
            string error = "";
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Employee WHERE EmployeeId = @EmployeeId", connection);
                command.Parameters.Add(CreateParam("@EmployeeId", employee.EmployeeId, SqlDbType.Int));
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    list.Remove(employee);
                    OnChanged(DbOperation.DELETE, DbModeltype.Employee);
                    return;
                }
                error = string.Format("Employee could not be deleted");
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            throw new DbException("Error in Employee repository: " + error);
        }
    }
}
