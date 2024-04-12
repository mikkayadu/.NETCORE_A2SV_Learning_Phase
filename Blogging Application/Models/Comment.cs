using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Blogging_Application.Models
{
    public class Comment:BaseEntity
    {
        public int PostId { get; set; }
        public string Text { get; set; }

        public virtual Post Post { get; set; }

        
    }
}
