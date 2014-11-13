namespace CrudeNews.Models
{
    using System;

    public class Like
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public virtual User User { get; set; }

        public int ArticleID { get; set; }

        public virtual Article Article { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
