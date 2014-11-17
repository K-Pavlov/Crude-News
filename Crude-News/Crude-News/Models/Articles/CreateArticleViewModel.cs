namespace CrudeNews.Web.Models.Articles
{
    using System.Collections.Generic;
    using System.Web;

    using CrudeNews.Models;
    using CrudeNews.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;


    public class CreateArticleViewModel : IMapFrom<Article>
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public HttpPostedFileBase Image { get; set; }

        [Required(ErrorMessage = "Category is is required")]
        public string Category { get; set; }

        public string Tags { get; set; }

        public IEnumerable<string> CategoryNames { get; set; }
    }
}