using Microsoft.Owin;
using Owin;
using CrudeNews.Models;
using CrudeNews.Data;
using System;

[assembly: OwinStartupAttribute(typeof(CrudeNews.Web.Startup))]
namespace CrudeNews.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
