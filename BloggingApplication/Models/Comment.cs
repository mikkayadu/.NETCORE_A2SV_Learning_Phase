namespace BloggingApplication.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt {  get; set; }

        public int PostId { get; set; }
        public virtual Post? Post { get; set; }
    }
}
