﻿namespace AppointmentBooking.Meetings.DTO
{
    public class MeetingResponse
    {
        public int Id { get; set; }

        public DateTime FromDateTime { get; set; }

        public int Duration { get; set; }

        public string Title { get; set; } = null!;

        public string Status { get; set; } = null!; // make enum for the different statuses

        public string Location { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool? ReservationPaid { get; set; } = null!;
    }
}
