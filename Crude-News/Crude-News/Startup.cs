using Microsoft.Owin;
using Owin;
using CrudeNews.Models;
[assembly: OwinStartupAttribute(typeof(CrudeNews.Startup))]
namespace CrudeNews
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
