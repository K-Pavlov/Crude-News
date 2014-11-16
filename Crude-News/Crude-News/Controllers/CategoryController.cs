using CrudeNews.Data;
using CrudeNews.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;

namespace CrudeNews.Controllers
{
    public class CategoryController : Controller
    {
        private ICrudeNewsData Data { get; set; }

        public CategoryController()
            :this(new CrudeNewsData())
        {

        }

        public CategoryController(ICrudeNewsData data)
        {
            this.Data = data;
        }


        // GET Category/View/{name}
        public ActionResult All(string name)
        {
            var model = this.Data.Categories
                .All()
                .Where(x => x.Name == name)
                .Project()
                .To<CategoryViewModel>();

            return View(model);
        }
    }
}