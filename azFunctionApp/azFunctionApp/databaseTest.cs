
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using azFunctionApp.Models;
using System.Collections.Generic;

namespace azFunctionApp
{
    public static class databaseTest
    {
        [FunctionName("databaseTest")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            //var str = Environment.GetEnvironmentVariable("sqldb_connection");
            try
            {
                var str = "Server = tcp:jayashandbserver.database.windows.net,1433; Initial Catalog = azurefunctiondb; Persist Security Info = False; User ID = jayashan; Password = Jayash123+; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30";
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
                                LName = Convert.ToString(reader["LName"])

                            };
                            users.Add(user);
                        }
                        return new OkObjectResult(users);
                    }
                }
            }
            catch (Exception e) {
                return new BadRequestObjectResult(e.Message);
            }
            }
        }
    }
