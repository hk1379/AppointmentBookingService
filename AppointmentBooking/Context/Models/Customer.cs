using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; } = null!;

        [Required]
        [Column("PhoneNumber", Order = 2)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [Column("EmailAddress", Order = 3)]
        public string EmailAddress { get; set; } = null!;

        [Column("CompanyName", Order = 4)]
        public string? CompanyName { get; set; }

        public virtual List<Meeting>? Meetings { get; set; }
    }
}
