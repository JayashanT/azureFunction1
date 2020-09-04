using System.Collections.Generic;
using azFunctionApp.Models;

namespace azFunctionApp.Services
{
    interface IUserServices
    {
        User addUser(User user);
        bool deleteUser(int Id);
        List<User> getAllUsers();
        User getUserById(int Id);
        User updateUser(User user);
    }
}