namespace CrudeNews.Models
{
    using CrudeNews.Infrastructure.Mapping;

    public class UserViewModel : IMapFrom<User>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }
    }
}