using System.ComponentModel.DataAnnotations;

namespace dotnet_museum.Models;

public class RoleViewModel
{
    [Required]
    public string Name { get; set; }
}