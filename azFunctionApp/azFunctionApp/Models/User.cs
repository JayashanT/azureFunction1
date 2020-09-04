using System;
using System.Collections.Generic;
using System.Text;

namespace azFunctionApp.Models
{
    class User
    {
        public int Id {get;set;}
        public string FName { get; set; }
        public string LName { get; set; }
        public int RoleId { get; set; }
    }
}
