namespace CrudeNews.Web.ViewModels
{
    using CrudeNews.Models;
    using CrudeNews.Web.Infrastructure.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Name { get; set; }

        public bool IsMainNews { get; set; }
    }
}