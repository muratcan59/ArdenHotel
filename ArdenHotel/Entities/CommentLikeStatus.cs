namespace ArdenHotel.Entities
{
    public class CommentLikeStatus
    {
        public CommentLikeStatus()
        {
            Comment = new Comment();
        }
        public int CommentLikeStatusID { get; set; }
        public int? CommentID { get; set; }
        public int? LikeCount { get; set; }
        public int? UnlikeCount { get; set; }

        public Comment Comment { get; set; }
    }
}
