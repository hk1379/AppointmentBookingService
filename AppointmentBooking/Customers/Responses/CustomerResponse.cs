namespace AppointmentBooking.Customers.Responses
{
    public class CustomerResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public string? CompanyName { get; set; }
    }
}
