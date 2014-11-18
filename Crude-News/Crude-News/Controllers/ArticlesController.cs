namespace CrudeNews.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using CrudeNews.Data;
    using CrudeNews.Web.ViewModels.Articles;

    public class ArticlesController : BaseController
    {
        public ArticlesController(ICrudeNewsData data)
            : base(data)
        {
        }

        public ActionResult Show(int? id)
        {
            ArticleViewModel model;

            if (id != null)
            {
                model = this.data.Articles
                    .All()
                    .Where(x => x.ID == id.Value)
                    .Project()
                    .To<ArticleViewModel>()
                    .FirstOrDefault();
            }
            else
            {
                model = this.data.Articles
                    .All()
                     .Project()
                    .To<ArticleViewModel>()
                    .FirstOrDefault();
            }

            return View(model);
        }  
    }
}