namespace Blogging_Application.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public string Content {  get; set; }

        public DateTime DateAdded { get; set; }
        

    }
}
