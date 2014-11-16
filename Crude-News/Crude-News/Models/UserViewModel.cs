namespace CrudeNews.Web.Models
{
    using CrudeNews.Models;
    using CrudeNews.Web.Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<User>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public string AvatarPath { get; set; }
    }
}