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

        public Employee GetById(int employeeId)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT EmployeeId, Name, Address, WorkEmail, SocialSecurityNumber, Phone, WorkPhone, Password FROM Employee WHERE EmployeeId = @EmployeeId", connection);
                command.Parameters.Add(CreateParam("@EmployeeId", employeeId, SqlDbType.Int));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    return new Employee((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString());
                }

                throw new DbException($"Employee with id {employeeId} not found");
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


        public void GetAll()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT EmployeeId, Name, Address, WorkEmail, SocialSecurityNumber, Phone, WorkPhone, Password FROM Employee ORDER BY Name", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                    list.Add(new Employee((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString()));
                OnChanged(DbOperation.SELECT, DbModeltype.Department);
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

        public Employee ValidateLogin(string workEmail, string password)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT EmployeeId, Name, Address, WorkEmail, SocialSecurityNumber, Phone, WorkPhone, Password FROM Employee WHERE WorkEmail = @WorkEmail AND Password = @Password", connection);
                command.Parameters.Add(CreateParam("@WorkEmail", workEmail, SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Password", password, SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // Employee exists
                    var employee = new Employee((int)reader[0], reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString());

                    OnChanged(DbOperation.SELECT, DbModeltype.Employee);

                    return employee;
                }

                // Employee does not exist
                throw new DbException("Error in Employee repository - username and password does not match");
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
    }
}
