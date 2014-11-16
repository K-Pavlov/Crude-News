



namespace CrudeNews.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using CrudeNews.Data;
    using CrudeNews.Web.Models.Articles;

    public class ArticlesController : Controller
    {
        public ICrudeNewsData Data { get; set; }

        public ArticlesController(ICrudeNewsData data)
        {
            this.Data = data;
        }

        public ArticlesController()
            : this(new CrudeNewsData())
        {

        }

        public ActionResult Show(int id)
        {
            var model = this.Data.Articles
                .All()
                .Where(x => x.ID == id)
                .Project()
                .To<ShowArticleViewModel>()
                .FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShowArticleViewModel model)
        {
            return View();
        }
    }
}