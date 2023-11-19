using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppointmentBooking.Context.Models
{
    [Table(name: "Meetings")]
    public class Meeting
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("Name", Order = 1)]
        public string MeetingName { get; set; } = string.Empty;

        [Required]
        [Column("HostPhoneNumber", Order = 2)]
        public string? CustomerPhoneNumber { get; set; }

        [Required]
        [Column("AttendeePhoneNumber", Order = 3)]
        public string? CompanyPhoneNumber { get; set; }

        [Required]
        [Column("HostEmailAddress", Order = 4)]
        public string? CustomerEmailAddress { get; set; }

        [Required]
        [Column("AttendeeEmailAddress", Order = 5)]
        public string? CompanyEmailAddress { get; set; }

        public List<Customer>? Customers { get; set; }
    }
}
