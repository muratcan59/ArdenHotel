using System;
using System.Collections;
using System.Collections.Generic;

namespace ArdenHotel.Entities
{
    public class Reservation
    {
        public Reservation()
        {
            Rooms = new List<Room>();
        }
        public int ReservationID { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public bool IsActive { get; set; }
        public int RoomId { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
