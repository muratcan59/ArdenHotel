using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArdenHotel.Entities
{
    public class Media
    {
        public int MediaID { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public DateTime? ModifiedDateUtc { get; set; }
        public string Url { get; set; }
        public string ResizedUrl { get; set; }
        public string Title { get; set; }
        public bool IsBanner { get; set; }
        public bool IsSectionOne { get; set; }
        public bool IsSectionTwo { get; set; }
        public bool IsSectionThree { get; set; }
        public bool IsSectionFour { get; set; }
        public bool IsSectionFive { get; set; }
        public int? RoomId { get; set; }
        [NotMapped]
        public Room Room { get; set; }
    }
}
