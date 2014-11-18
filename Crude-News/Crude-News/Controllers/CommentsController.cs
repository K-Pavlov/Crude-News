namespace CrudeNews.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using CrudeNews.Data;
    using CrudeNews.Models;
    using CrudeNews.Web.ViewModels;

    using Microsoft.AspNet.Identity;
    using System.Data.Entity.Validation;
    using System;
    using System.IO;

    public class CommentsController : BaseController
    {
        public CommentsController(ICrudeNewsData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentViewModel model)
        {
            var currentArticle = this.data.Articles.Find(model.ArticleId);

            if (!this.ModelState.IsValid)
            {
                return this.PartialView("_DisplayComments", this.data.Articles.All().FirstOrDefault());
            }

            var currentUserId = this.User.Identity.GetUserId();

            var comment = new Comment
            {
                Content = WebUtility.HtmlEncode(model.Content),
                ArticleID = model.ArticleId,
                DateCreated = DateTime.Now
            };

            currentArticle.Comments.Add(comment);

            this.data.SaveChanges();

            return Json(new 
            {
                toDisplay = this.RenderViewToString(this.ControllerContext, "_DisplayComments", currentArticle.Comments)
            });
        }

        [NonAction]
        public string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}