using CrudeNews.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace CrudeNews.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ICrudeNewsData data;

        public BaseController(ICrudeNewsData data)
        {
            this.data = data;
        }
    }
}
