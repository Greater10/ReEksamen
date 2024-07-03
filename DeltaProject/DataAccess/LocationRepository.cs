using DeltaProject.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DeltaProject.DataAccess
{
    public class LocationRepository : Repository, IEnumerable<Location>
    {
        private List<Location> list = new List<Location>();

        public IEnumerator<Location> GetEnumerator()
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
                SqlCommand command = new SqlCommand("SELECT LocationId, Name FROM Location", connection);                
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                    list.Add(new Location((int)reader["LocationId"], reader["Name"].ToString()));
                OnChanged(DbOperation.SELECT, DbModeltype.Location);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Location repository: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        public void Add(Location location)
        {
            string error = "";
            if (location.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Location (Name) VALUES (@Name)", connection);
                    command.Parameters.Add(CreateParam("@Name", location.Name, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        list.Add(location);
                        list.Sort();
                        OnChanged(DbOperation.INSERT, DbModeltype.Location);
                        return;
                    }
                    error = string.Format("Location could not be inserted in the database");
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
            else error = "Illegal value for location";
            throw new DbException("Error in Location repository: " + error);
        }

        public void Update(Location location)
        {
            string error = "";
            if (location.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Location SET Name = @Name WHERE LocationId = @LocationId", connection);
                    command.Parameters.Add(CreateParam("@Name", location.Name, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@LocationId", location.LocationId, SqlDbType.Int));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        UpdateList(location);
                        OnChanged(DbOperation.UPDATE, DbModeltype.Location);
                        return;
                    }
                    error = string.Format("Location could not be updated");
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
            else error = "Illegal value for location";
            throw new DbException("Error in Location repository: " + error);
        }

        private void UpdateList(Location location)
        {
            for (int i = 0; i < list.Count; ++i)
                if (list[i].LocationId.Equals(location.LocationId))
                {
                    list[i].Name = location.Name;
                    break;
                }
        }

        public void Remove(Location location)
        {
            string error = "";
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Location WHERE LocationId = @LocationId", connection);
                command.Parameters.Add(CreateParam("@LocationId", location.LocationId, SqlDbType.Int));
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    list.Remove(location);
                    OnChanged(DbOperation.DELETE, DbModeltype.Location);
                    return;
                }
                error = string.Format("Location could not be deleted");
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            throw new DbException("Error in Location repository: " + error);
        }
    }
}

