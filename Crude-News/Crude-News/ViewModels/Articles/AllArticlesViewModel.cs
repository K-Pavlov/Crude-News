using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudeNews.Web.ViewModels.Articles
{
    public class AllArticlesViewModel
    {
        public ICollection<ArticleViewModel> Articles { get; set; }

        public int Page { get; set; }
    }
}