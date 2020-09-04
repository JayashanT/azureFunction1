using azFunctionApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace azFunctionApp.Services
{
    class UserServices : IUserServices
    {
        private string str = "Server = tcp:jayashandbserver.database.windows.net,1433; Initial Catalog = azurefunctiondb; Persist Security Info = False; User ID = jayashan; Password = Jayash123+; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30";
        public UserServices()
        {

        }

        public List<User> getAllUsers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    var text = "Select * from users";

                    using (SqlCommand cmd = new SqlCommand(text, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<User> users = new List<User>();
                        while (reader.Read())
                        {
                            User user = new User()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FName = Convert.ToString(reader["FName"]),
                                LName = Convert.ToString(reader["LName"]),
                                RoleId=Convert.ToInt32(reader["RoleId"])
                            };
                            users.Add(user);
                        }
                        return users;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User getUserById(int Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    var text = "Select * from users where Id=@Id";

                    using (SqlCommand cmd = new SqlCommand(text, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", Id);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        User user = new User()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FName = Convert.ToString(reader["FName"]),
                            LName = Convert.ToString(reader["LName"]),
                            RoleId = Convert.ToInt32(reader["RoleId"])
                        };
                        return user;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool deleteUser(int Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    var text = "delete users where Id=@Id";

                    using (SqlCommand cmd = new SqlCommand(text, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", Id);
                        var result = cmd.ExecuteNonQuery();
                        if (result!=0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public User addUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    var text = "INSERT INTO Users (FName,LName,RoleId) output INSERTED.Id VALUES(@FName,@LName,@RoleId)";

                    using (SqlCommand cmd = new SqlCommand(text, conn))
                    {
                        cmd.Parameters.AddWithValue("@FName", user.FName);
                        cmd.Parameters.AddWithValue("@LName", user.LName);
                        cmd.Parameters.AddWithValue("@RoleId", user.RoleId);

                        int Id = (int)cmd.ExecuteScalar();
                        if (Id > 0)
                        {
                            user.Id = Id;
                            return user;
                        }
                        else
                            return null;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public User updateUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    var text = "UPDATE Users SET FName=@FName,LName=@LName,RoleId=@RoleId WHERE Id=@Id";

                    using (SqlCommand cmd = new SqlCommand(text, conn))
                    {
                        cmd.Parameters.AddWithValue("@FName", user.FName);
                        cmd.Parameters.AddWithValue("@LName", user.LName);
                        cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                        cmd.Parameters.AddWithValue("@Id", user.Id);

                        int Id = (int)cmd.ExecuteScalar();
                        if (Id > 0)
                        {
                            return user;
                        }
                        else
                            return null;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
