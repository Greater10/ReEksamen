using DeltaProject.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DeltaProject.DataAccess
{
    public class EmployeeRepository : Repository
    {
        public void ValidateLogin(string workEmail, string password)
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

                    return;
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
