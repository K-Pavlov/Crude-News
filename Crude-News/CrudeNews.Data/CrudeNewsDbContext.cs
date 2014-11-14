﻿namespace CrudeNews.Data
{
    using System.Data.Entity;
    using System.Linq;
    using CrudeNews.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using CrudeNews.Data.Migrations;
    using System.Data.Entity.Validation;

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

        public IDbSet<ArticleLike> Likes { get; set; }

        public static CrudeNewsDbContext Create()
        {
            return new CrudeNewsDbContext();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}
