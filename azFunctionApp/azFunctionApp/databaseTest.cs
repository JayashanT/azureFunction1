
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
using azFunctionApp.Services;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace azFunctionApp
{
    public class databaseTest
    {
        private static IUserServices userServices = new UserServices();

        internal static IUserServices UserServices { get => userServices; set => userServices = value; }

        [FunctionName("getAllUsers")]
        public static IActionResult getAllUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "api/getAllUsers")]HttpRequest req, TraceWriter log)
        {
            var result=userServices.getAllUsers();
            if (result != null)
                return new OkObjectResult(result);
            else
                return new BadRequestObjectResult("Unable to get Users");
        }

        [FunctionName("getUserById")]
        public static IActionResult getUserById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "api/getUserById/{Id}")]HttpRequest req, TraceWriter log,string Id)
        {
            var result = userServices.getUserById(Convert.ToInt32(Id));
            if (result != null)
                return new OkObjectResult(result);
            else
                return new BadRequestObjectResult("Unable to get User");
        }

        [FunctionName("addUser")]
        public static IActionResult AddUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "api/AddUser")]HttpRequest req, TraceWriter log)
        {
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            User user = new User()
            {
                FName = data.FName,
                LName=data.LName,
                RoleId=data.RoleId
            };
            var result = UserServices.addUser(user);
            if (result != null)
                return new OkObjectResult(result);
            else
                return new BadRequestObjectResult("User Create Failed");
        }

        [FunctionName("deleteUser")]
        public static IActionResult deleteUser([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "api/deleteUser/{Id}")]HttpRequest req, TraceWriter log,string Id)
        {
            var result = userServices.deleteUser(Convert.ToInt32(Id));
            if (result)
                return new OkObjectResult("User Deleted");
            else
                return new BadRequestObjectResult("User Delete failed");
        }

        [FunctionName("updateUser")]
        public static IActionResult updateUser([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "api/updateUser")]HttpRequest req, TraceWriter log)
        {

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            User user = new User()
            {
                Id = data.Id,
                FName = data.FName,
                LName = data.LName,
                RoleId = data.RoleId
            };
            var result = UserServices.addUser(user);
            if (result != null)
                return new OkObjectResult(result);
            else
                return new BadRequestObjectResult("User update Failed");
        }
    }
    }
