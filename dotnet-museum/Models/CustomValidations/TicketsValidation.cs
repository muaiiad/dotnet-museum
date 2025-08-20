using System.ComponentModel.DataAnnotations;
using dotnet_museum.Data;
using dotnet_museum.Models.Booking;

namespace dotnet_museum.Models.CustomValidations;

public class TicketsValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var booking = (BookingModel)validationContext.ObjectInstance;
        var context = (AppDbContext)validationContext.GetService(typeof(AppDbContext));
        var ev = context.Events.FirstOrDefault(e => e.EventId == booking.EventId);
        
        if(booking.NumberOfTickets > ev.Capacity)
        {
            return new ValidationResult("The number of tickets exceed the capacity of the event.");
        }
        else if (booking.NumberOfTickets < 1)
        {
            return new ValidationResult("The number of tickets must be greater than or equal to 1.");
        }
        return ValidationResult.Success;
    }
}