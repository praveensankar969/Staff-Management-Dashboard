using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Model.DTO;
using PersistenceLayer.Interfaces;

namespace PersistenceLayer.DBHandler
{
    public class DataBaseHandler : IActions
    {
        SqlConnection connection;
        private string _config = @"Server=localhost\SQLEXPRESS;Database=TutorialDB;Trusted_Connection=True;";

        public void AddStaff<T>(T admin)
        {

            using (connection = new SqlConnection(_config))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                SqlCommand cmd = new SqlCommand(@"dbo.[InsertData]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = typeof(T).GetProperty("UserName").GetValue(admin);
                cmd.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = typeof(T).GetProperty("Password").GetValue(admin);
                cmd.Parameters.AddWithValue("@Experience", SqlDbType.Int).Value = typeof(T).GetProperty("Experience").GetValue(admin);
                cmd.Parameters.AddWithValue("@DateOfJoining", SqlDbType.DateTime).Value = typeof(T).GetProperty("DateOfJoining").GetValue(admin);
                cmd.Parameters.AddWithValue("@PhoneNumber", SqlDbType.NVarChar).Value = typeof(T).GetProperty("PhoneNumber").GetValue(admin);
                cmd.Parameters.AddWithValue("@Subject", SqlDbType.NVarChar).Value = typeof(T).GetProperty("Subject").GetValue(admin);
                cmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = typeof(T).GetProperty("Type").GetValue(admin);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void EditStaff(int id, StaffUpdateDTO admin)
        {
            using (connection = new SqlConnection(_config))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                SqlCommand cmd = new SqlCommand(@"dbo.[UpdateData]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = admin.UserName;
                cmd.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = admin.Password;
                cmd.Parameters.AddWithValue("@Experience", SqlDbType.Int).Value = admin.Experience;
                cmd.Parameters.AddWithValue("@DateOfJoining", SqlDbType.DateTime).Value = admin.DateOfJoining;
                cmd.Parameters.AddWithValue("@PhoneNumber", SqlDbType.NVarChar).Value = admin.PhoneNumber;
                cmd.Parameters.AddWithValue("@Subject", SqlDbType.NVarChar).Value = admin.Subject;
                cmd.Parameters.AddWithValue("@Type", SqlDbType.NVarChar).Value = admin.Type;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<Staff> GetAllStaff()
        {
            List<Staff> staffs = new List<Staff>();
            using (connection = new SqlConnection(_config))
            {
                DataTable table = new DataTable();
                connection.Open();
                SqlCommand cmd = new SqlCommand(@"dbo.[GetData]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                foreach (DataRow row in table.Rows)
                {
                    staffs.Add(
                        new Staff
                        {
                            Id = Convert.ToInt32(row["Id"].ToString()),
                            UserName = row["Username"].ToString(),
                            Password = row["Password"].ToString(),
                            Experience = Convert.ToInt32(row["Experience"].ToString()),
                            DateOfJoining = DateTime.Parse(row["DateOfJoining"].ToString()),
                            Subject = row["Subject"].ToString(),
                            PhoneNumber = row["PhoneNumber"].ToString(),
                            Type = row["Type"].ToString()
                        }
                    );
                }
            }
            return staffs;
        }

        public User Login(LoginDTO login)
        {
            using (connection = new SqlConnection(_config))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                SqlCommand cmd = new SqlCommand(@"dbo.[Login]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = login.UserName;
                cmd.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = login.Password;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    return new User { Id = reader.GetInt32(0), Type = reader.GetString(7) };
                }
                else
                {
                    return new User { Id = -1, Type = " " };
                }
            }
        }

        public Staff GetStaff(int id)
        {
            using (connection = new SqlConnection(_config))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                SqlCommand cmd = new SqlCommand(@"dbo.[GetWithId]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    Console.WriteLine("Name: " + reader.GetString(1) + " \tDate of Joining: " + reader.GetDateTime(4) + " \tExperience: " + reader.GetInt32(3) + " \tPhone: " + reader.GetString(5) + " \tSubject: " + reader.GetString(6) + " \tStaff Type: " + reader.GetString(7));
                    return new Staff
                    {
                        Id = reader.GetInt32(0),
                        UserName = reader.GetString(1),
                        DateOfJoining = reader.GetDateTime(4),
                        Experience = reader.GetInt32(3),
                        PhoneNumber = reader.GetString(5),
                        Subject = reader.GetString(6),
                        Type = reader.GetString(7)
                    };
                }
                else
                {
                    return null;
                }

            }
        }

        public void DeleteStaff(int id)
        {
            using (connection = new SqlConnection(_config))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                SqlCommand cmd = new SqlCommand(@"dbo.[DeleteWithId]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}