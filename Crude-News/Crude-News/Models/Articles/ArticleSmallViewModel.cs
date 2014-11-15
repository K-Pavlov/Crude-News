namespace CrudeNews.Web.Models.Articles
{
    using System;
    using CrudeNews.Web.Infrastructure.Mapping;
    using CrudeNews.Models;

    public class ArticleSmallViewModel : IMapFrom<Article>
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
