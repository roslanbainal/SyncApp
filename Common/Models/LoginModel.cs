using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class LoginModel
    {
        public LoginModel()
        {
            username = "user@aemenersol.com";
            password = "Test@123";
        }
        public string username { get; set; } 
        public string password { get; set; } 
    }
}
