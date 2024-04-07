using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        //[Required]
        //[Column("CompanyId", Order = 1)]
        //public int CompanyId { get; set; }

        [Required]
        [Column("FromDateTime", Order = 1)]
        public DateTime FromDateTime { get; set; }

        [Required]
        [Column("Duration", Order = 2)]
        public int Duration { get; set; }

        [Required]
        [Column("Title", Order = 3)]
        public required string Title { get; set; }

        [Required]
        [Column("Status", Order = 4)]
        public required string Status { get; set; }

        [Required]
        [Column("Location", Order = 5)]
        public required string Location { get; set; }

        [Required]
        [Column("Description", Order = 6)]
        public required string Description { get; set; }

        [Column("ReservationPaid", Order = 7)]
        public bool? ReservationPaid { get; set; }

        // stops the get all api from showing companies
        [JsonIgnore]
        public List<Company>? Companies { get; set; }

        // stops the get all api from showing customers
        [JsonIgnore]
        public List<Customer>? Customers { get; set; }
    }
}