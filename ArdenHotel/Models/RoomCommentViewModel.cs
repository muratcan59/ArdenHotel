using ArdenHotel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArdenHotel.Models
{
    public class RoomCommentViewModel
    {
        public RoomCommentViewModel()
        {
            Comments = new List<Comment>();
            Comment = new Comment();
        }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        public Room Room { get; set; }
    }
}