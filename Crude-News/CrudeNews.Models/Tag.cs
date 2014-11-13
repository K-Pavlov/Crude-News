namespace CrudeNews.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        public Tag()
        {
            this.Articles = new HashSet<Article>();
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
