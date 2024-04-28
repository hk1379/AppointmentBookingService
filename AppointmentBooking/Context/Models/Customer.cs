using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppointmentBooking.Context.Models
{
    [Table(name: "Customers")]
    public class Customer
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("Name", Order = 1)]
        public required string Name { get; set; }

        [Required]
        [Column("PhoneNumber", Order = 2)]
        public required string PhoneNumber { get; set; }

        [Required]
        [Column("EmailAddress", Order = 3)]
        public required string EmailAddress { get; set; }

        [Column("CompanyName", Order = 4)]
        public string? CompanyName { get; set; }

        // stops the get all api from showing meetings
        [JsonIgnore]
        public List<Meeting>? Meetings { get; set; }
    }
}
