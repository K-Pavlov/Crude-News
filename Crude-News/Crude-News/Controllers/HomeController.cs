using CrudeNews.Data;
using CrudeNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using CrudeNews.Models.Articles;

namespace CrudeNews.Controllers
{
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

        public ActionResult Index()
        {
            var allArticles = this.Data.Articles.All();

            var model = new Dictionary<String, IQueryable<ArticleSmallViewModel>>();
            var categories = this.Data.Categories.All();

            foreach (var category in categories)
            {
                var modelArticlesInCategory = allArticles
                    .Where(x => x.Caterogy.ID == category.ID)
                    .OrderBy(x => x.DateCreated)
                    .Take(3)
                    .Project()
                    .To<ArticleSmallViewModel>();

                model.Add(category.Name, modelArticlesInCategory);    
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}