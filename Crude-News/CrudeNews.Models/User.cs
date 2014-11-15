namespace CrudeNews.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        [NotMapped]
        public readonly Size AVATAR_SIZE = new Size(100, 100);

        public int Age { get; set; }

        public string AvatarPath { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            //// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
           //// Add custom user claims here
            return userIdentity;
        }
    }
}
