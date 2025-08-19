using System.ComponentModel.DataAnnotations;
using dotnet_museum.Data;
using dotnet_museum.Models.MuseumEvents;

namespace dotnet_museum.Models.CustomValidations;

public class EventDateValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var ev = (EventModel)validationContext.ObjectInstance;

        if (ev.StartDate >= ev.EndDate)
        {
            return new ValidationResult(
                "Start Date must be smaller than End Date.",
                new[] { nameof(EventModel.EndDate) }
            );
        }
        
        return ValidationResult.Success;
    }
}