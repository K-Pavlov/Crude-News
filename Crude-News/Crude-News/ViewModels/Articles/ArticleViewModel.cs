﻿namespace CrudeNews.Web.ViewModels.Articles
{
    using System;
    using System.Collections.Generic;

    using CrudeNews.Models;
    using CrudeNews.Web.Infrastructure.Mapping;

    public class ArticleViewModel : IMapFrom<Article>
    {
        public int Id { get; set; }

        public User Author { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public string ImagePath { get; set; }

        public CategoryViewModel Category { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
