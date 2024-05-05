using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AppointmentBooking.Customers.Requests
{
    public record CreateCustomerRequest : IValidatableObject
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        public required string EmailAddress { get; set; }

        public string? CompanyName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => Enumerable.Empty<ValidationResult>();
    }
}
