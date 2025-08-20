using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_museum.Models
{
    public class RegisterViewModel
    {
        [Remote(action: "IsUsernameAvailable", controller: "Register")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string confirmPassword { get; set; }
    }
}