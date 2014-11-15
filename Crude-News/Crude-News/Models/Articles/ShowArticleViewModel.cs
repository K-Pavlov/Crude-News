namespace CrudeNews.Web.Models.Articles
{
    using System;
    using System.Collections.Generic;

    using CrudeNews.Models;
    using CrudeNews.Web.Infrastructure.Mapping;

    public class ShowArticleViewModel : IMapFrom<Article>
    {
        public User Author { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public string ImagePath { get; set; }

        public Category Category { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<ArticleLike> ArticleLikes { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
