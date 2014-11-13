namespace CrudeNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        public Article()
        {
            this.Tags = new HashSet<Tag>();
            this.Likes = new HashSet<Like>();
            this.Comments = new HashSet<Comment>();
        }

        [Key]
        public int ID { get; set; }

        public string AuthorID { get; set; }

        public virtual User Author { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        // TODO: [enable html]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Caterogy { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
