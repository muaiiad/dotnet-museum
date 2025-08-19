using System.ComponentModel.DataAnnotations;

namespace dotnet_museum.Models
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string confirmPassword { get; set; }
    }
}