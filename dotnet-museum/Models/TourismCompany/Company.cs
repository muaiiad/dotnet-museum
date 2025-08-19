using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using dotnet_museum.Models.Booking;
using Microsoft.AspNetCore.Identity;

namespace dotnet_museum.Models.TourismCompany;

public class Company
{
    public int CompanyId { get; set; }
    public string RegistrationNumber {get; set;}
    public string Name {get; set;}
    public string Address {get; set;}
    [EmailAddress]
    public string Email {get; set;}
    public string Phone {get; set;}
    public int MaxNumberOfTickets {get; set;}
    public decimal DiscountPercentage {get; set;}

    public ICollection<BookingModel> Bookings { get; set; } = [];
}