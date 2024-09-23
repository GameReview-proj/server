using System.ComponentModel.DataAnnotations;

namespace GameReview.Services.Annotations;

[AttributeUsage(AttributeTargets.Parameter)]
public class ExclusiveBetween(string otherPropertyName) : ValidationAttribute
{
    private readonly string _otherPropertyName = otherPropertyName;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);

        var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance);

        if (value != null && otherPropertyValue != null)
        {
            return new ValidationResult($"Apenas {validationContext.DisplayName} OU {_otherPropertyName} podem ter um valor.");
        }

        return ValidationResult.Success;
    }
}