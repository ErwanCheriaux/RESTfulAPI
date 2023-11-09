using System.ComponentModel.DataAnnotations;

namespace MountainBike.Api.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class DateOnlyRangeAttribute : ValidationAttribute
{
    public DateOnly MinValue { get; }
    public DateOnly MaxValue { get; }

    public DateOnlyRangeAttribute(string minValue, string maxValue)
    {
        MinValue = DateOnly.Parse(minValue);
        MaxValue = DateOnly.Parse(maxValue);
    }

    public override bool IsValid(object? value)
    {
        if (value is DateOnly date)
        {
            return date >= MinValue && date <= MaxValue;
        }

        return false;
    }
}
