namespace CrudeNews.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using CrudeNews.Data;
    using CrudeNews.Web.ViewModels.Articles;
    using System.Collections.Generic;

    public class PageableArticleListController : BaseController
    {
        public const int PageSize = 10;

        public PageableArticleListController(ICrudeNewsData data) 
            : base(data)
        {
        }

        public ActionResult Articles()
        {
            var model = GetModel(0);
            this.SetTempData(1);

            return View(model);
        }

        public ActionResult GetPage(int? page)
        {
            int currentPage = page == null ? 1 : page.Value;

            int numberOfPages = this.GetPageCount();

            if (currentPage <= 0)
            {
                currentPage = 1;
            }
            else if (currentPage >= numberOfPages)
            {
                currentPage = numberOfPages;
            }

            int skip = (currentPage - 1) * PageSize;

            var model = GetModel(skip);

            this.SetTempData(currentPage);
            return this.PartialView("_Pages", model);
        }

        [NonAction]
        private List<ArticleViewModel> GetModel(int skip)
        {

            var articles = this.data.Articles.All();

            var model = articles.OrderByDescending(x => x.DateCreated)
                .Skip(skip)
                .Take(PageSize)
                .Project()
                .To<ArticleViewModel>()
                .ToList();

            return model;
        }

        [NonAction]
        private int GetPageCount()
        {
            int count = this.data.Articles.All().Count();

            double pageCount = double.Parse(count.ToString()) /
                                double.Parse(PageSize.ToString());

            int numberOfPages = (int)Math.Ceiling(pageCount);

            return numberOfPages;
        }

        [NonAction]
        private void SetTempData(int currentPage)
        {
            int numberOfPages = this.GetPageCount();

            int nextPage = numberOfPages;
            int previousPage = 1;

            if (currentPage - 1 > 0)
            {
                previousPage = currentPage - 1;
            }

            if (currentPage + 1 <= numberOfPages)
            {
                nextPage = currentPage + 1;
            }

            this.TempData["NumberOfPages"] = numberOfPages;
            this.TempData["NextPage"] = nextPage;
            this.TempData["PreviousPage"] = previousPage;
        }
    }
}