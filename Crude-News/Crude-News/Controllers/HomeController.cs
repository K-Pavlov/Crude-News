using CrudeNews.Data;
using CrudeNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
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