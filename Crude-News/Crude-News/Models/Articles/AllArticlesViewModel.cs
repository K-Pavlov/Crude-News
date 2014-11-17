using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudeNews.Web.Models.Articles
{
    public class AllArticlesViewModel
    {
        public IPagedList<ArticleSmallViewModel> Articles { get; set; }

        public int Page { get; set; }
    }
}