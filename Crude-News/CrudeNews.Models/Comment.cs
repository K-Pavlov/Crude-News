namespace CrudeNews.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int ID { get; set; }

        public string AuthorID { get; set; }

        public virtual User Author { get; set; }

        public int ArticleID { get; set; }

        [Required]
        public virtual Article Article { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
