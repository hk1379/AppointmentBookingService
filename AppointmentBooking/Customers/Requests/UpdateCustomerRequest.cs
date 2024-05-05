namespace AppointmentBooking.Customers.Requests;

using System.ComponentModel.DataAnnotations;
public record UpdateCustomerRequest : IValidatableObject
{
    [Required]
    public required int Id { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }

    public string? CompanyName { get; set; }

    int[]? MeetingIds { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => Enumerable.Empty<ValidationResult>();
}
