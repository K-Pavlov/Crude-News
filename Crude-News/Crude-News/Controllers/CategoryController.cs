using CrudeNews.Data;
using CrudeNews.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;

namespace CrudeNews.Web.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(ICrudeNewsData data)
            : base (data)
        {
        }


        // GET Category/View/{name}
        public ActionResult All(string name)
        {
            var model = this.data.Categories
                .All()
                .Where(x => x.Name == name)
                .Project()
                .To<CategoryViewModel>();

            return View(model);
        }
    }
}