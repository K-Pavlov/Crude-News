namespace CrudeNews.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using CrudeNews.Data;
    using CrudeNews.Web.ViewModels.Articles;
    using CrudeNews.Web.ViewModels;

    public class HomeController : BaseController
    {
        public HomeController(ICrudeNewsData data)
            : base(data)
        {
        }

        // GET Home/Index
        public ActionResult Index()
        {
            var model = new Dictionary<CategoryViewModel, IList<ArticleSmallViewModel>>();
            var categories = this.data.Categories
                .All()
                .Project()
                .To<CategoryViewModel>();

            foreach (var category in categories)
            {
                var modelArticlesInCategory = this.data.Articles
                    .All()
                    .ToList()
                    .Where(x => x.Caterogy.Name == category.Name)
                    .OrderByDescending(x => x.DateCreated)
                    .ThenByDescending(x => x.Title)
                    .Take(3)
                    .AsQueryable()
                    .Project()
                    .To<ArticleSmallViewModel>();

                model.Add(category, modelArticlesInCategory.ToList());    
            }

            return View(model);
        }
    }
}