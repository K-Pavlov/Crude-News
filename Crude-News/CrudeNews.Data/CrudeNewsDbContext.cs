namespace CrudeNews.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;

    using CrudeNews.Data.Migrations;
    using CrudeNews.Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class CrudeNewsDbContext : IdentityDbContext<User>
    {
        public CrudeNewsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CrudeNewsDbContext, Configuration>());
        }

        public IDbSet<Article> Articles { get; set; }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Tag> Tags { get; set; }

        public static CrudeNewsDbContext Create()
        {
            return new CrudeNewsDbContext();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
