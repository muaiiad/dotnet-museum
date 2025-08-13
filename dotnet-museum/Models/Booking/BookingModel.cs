using System;
using dotnet_museum.Models.MuseumEvents;
using dotnet_museum.Models.TourismCompany;

namespace dotnet_museum.Models.Booking;

public class BookingModel
{
    public int BookingId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhone { get; set; }
    public int NumberOfTickets { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime BookingDate { get; set; }
    public ReservationType ReservationType { get; set; }
    
    public int EventId { get; set; }
    public EventModel Event { get; set; }
    
    public int? TourismCompanyId { get; set; }
    public Company? TourismCompany { get; set; }
    
}

public enum ReservationType
{
    Regular, 
    TourismCompany
}