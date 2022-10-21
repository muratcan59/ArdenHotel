using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArdenHotel.Entities
{
    public class Comment
    {
        public Comment()
        {
            
        }
        public int CommentID { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
        public bool IsActive { get; set; }
        public int? ParentID { get; set; }
        public string Commentary { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public decimal? Rate { get; set; }
        public int? RoomID { get; set; }
        public bool IsMainPageShow { get; set; }
        
        [NotMapped]
        public CommentLikeStatus CommentLikeStatus { get; set; }

    }
}
