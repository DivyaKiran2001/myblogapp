using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class BlogModel
    {
        public int BlogId { get; set; }
        public string Title { get;set; }
        
        public string Author { get; set; }

        public string Category { get; set; }

        public string FilePath {get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; }

    }
}
