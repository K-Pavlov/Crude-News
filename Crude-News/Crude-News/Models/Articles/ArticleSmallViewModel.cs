namespace CrudeNews.Models.Articles
{
    using System;
    using CrudeNews.Infrastructure.Mapping;

    public class ArticleSmallViewModel : IMapFrom<Article>
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string ImagePath { get; set; }

        public DateTime DateCreate { get; set; }
    }
}
