namespace CrudeNews.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using CrudeNews.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<CrudeNewsDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CrudeNewsDbContext context)
        {
            var manager = new UserManager<User>(
               new UserStore<User>(
                   new CrudeNewsDbContext()));

            if (context.Categories.Any())
            {
                return;
            }

            var articles = new List<Article>();

            var categories = new List<Category>() 
            {
                new Category 
                {
                    Name = "Daily news",
                },
                new Category 
                {
                    Name = "News in the country",
                },
                new Category
                {
                    Name = "World wide news"
                },
                new Category 
                {
                    Name = "Software"
                },
                new Category 
                {
                    Name = "Hardware"
                }
            };

            context.Categories.AddOrUpdate(categories.ToArray());

            var tags = new List<Tag>()
            {
                new Tag 
                {
                    Name = "Sports"
                },
                new Tag 
                {
                    Name = "Emergency"
                },
                new Tag 
                {
                    Name = "Entertainment"
                },
                new Tag 
                {
                    Name = "Outdoors"
                },
                new Tag 
                {
                    Name = "Beauty"
                }
            };

            context.Tags.AddOrUpdate(tags.ToArray());

            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                var user = new User()
                {
                    UserName = string.Format("User{0}", i.ToString())
                };
                manager.Create(user, string.Format("Password{0}", i.ToString()));
            }

            for (int i = 0; i < 100; i++)
            {
                var category = this.GetRandomCategory(context);
                var tag = this.GetRandomTag(context);

                var commentUser = this.GetRandomUser(context);
                var comment = new Comment
                {
                    Author = commentUser,
                    Content = string.Format("{0} {1} {2}", category.Name, commentUser.UserName, i.ToString()),
                    DateCreated = DateTime.Now,
                };

                var articleUser = this.GetRandomUser(context);
                var article = new Article
                {
                    Author = articleUser,
                    DateCreated = DateTime.Now,
                    Content = string.Format("{2} {1} {0}", category.Name, articleUser.UserName, i.ToString()),
                    Title = string.Format("My title is: {0}", category.Name),
                    Caterogy = category
                };

                if (i % 2 == 0)
                {
                    article.ImagePath = "~/Content/Images/cat.jpg";
                }

                comment.Article = article;
                article.Comments.Add(comment);
                article.Tags.Add(tag);
                articles.Add(article);
            }

            context.Articles.AddOrUpdate(articles.ToArray());
        }

        private Category GetRandomCategory(CrudeNewsDbContext context)
        {
            var categories = context.Categories.ToList();
            int categoryIndex = new Random().Next(0, categories.Count - 1);

            return categories[categoryIndex];
        }

        private Tag GetRandomTag(CrudeNewsDbContext context)
        {
            var tags = context.Tags.ToList();
            int tagIndex = new Random().Next(0, tags.Count - 1);

            return tags[tagIndex];
        }

        private User GetRandomUser(CrudeNewsDbContext context)
        {
            var users = context.Users.ToList();
            int userIndex = new Random().Next(0, users.Count - 1);

            return users[userIndex];
        }
    }
}
