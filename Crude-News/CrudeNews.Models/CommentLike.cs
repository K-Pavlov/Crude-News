using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudeNews.Models
{
    public class CommentLike
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public virtual User User { get; set; }

        public int CommentID { get; set; }

        public virtual Comment Comment { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
