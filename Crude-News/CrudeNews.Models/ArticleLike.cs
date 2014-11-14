namespace CrudeNews.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ArticleLike
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public virtual User User { get; set; }

        public int ArticleID { get; set; }

        public virtual Article Article { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
