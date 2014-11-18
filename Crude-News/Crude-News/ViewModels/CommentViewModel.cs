using CrudeNews.Models;
using CrudeNews.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudeNews.Web.ViewModels
{
    public class CommentViewModel : IMapFrom<Comment>
    {
        [Required(ErrorMessage="Content is required")]
        public string Content { get; set; }

        public int ArticleId { get; set; }
    }
}