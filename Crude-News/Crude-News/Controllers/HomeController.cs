namespace CrudeNews.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using CrudeNews.Data;
    using CrudeNews.Web.Models.Articles;
    using CrudeNews.Web.Models;

    public class HomeController : Controller
    {
        private ICrudeNewsData Data { get; set; }

        public HomeController()
            : this(new CrudeNewsData())
        {

        }

        public HomeController(ICrudeNewsData data)
        {
            this.Data = data;
        }

        // GET Home/Index
        public ActionResult Index()
        {
            var allArticles = this.Data.Articles.All();
            var model = new Dictionary<CategoryViewModel, IList<ArticleSmallViewModel>>();
            var categories = this.Data.Categories
                .All()
                .Project()
                .To<CategoryViewModel>();

            foreach (var category in categories)
            {
                var modelArticlesInCategory = allArticles
                    .Where(x => x.Caterogy.Name == category.Name)
                    .OrderBy(x => x.DateCreated)
                    .Take(3)
                    .Project()
                    .To<ArticleSmallViewModel>();

                model.Add(category, modelArticlesInCategory.ToList());    
            }

            return View(model);
        }

        // GET Home/About
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        // GET Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}