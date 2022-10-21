using ArdenHotel.Entities;
using System;
using System.Collections.Generic;

namespace ArdenHotel.Areas.SysAdmin.Models
{
    public class RoomMediaViewModel
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime? ModifiedDateUtc { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Capacity { get; set; }
        public string Type { get; set; }
        public int CategoryID { get; set; }
        public int? RoomCount { get; set; }
        public bool IsCheckOut { get; set; }
        public bool IsAvaliable { get; set; }
        public Category Category { get; set; }
        public Media Media { get; set; }
    }
}
                          