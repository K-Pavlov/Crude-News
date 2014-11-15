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
    using System.Text;

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
                    Content = this.GenerateLoremIpsum(10, 100, 2, 2, 1),
                    DateCreated = DateTime.Now,
                };

                var articleUser = this.GetRandomUser(context);
                var article = new Article
                {
                    Author = articleUser,
                    DateCreated = DateTime.Now,
                    Content = this.GenerateLoremIpsum(100, 1000, 2, 2, 5),
                    Title = string.Format("My title is: {0}", category.Name),
                    Caterogy = category
                };

                article.ImagePath = "~/Content/Images/cat.jpg";

                comment.Article = article;
                article.Comments.Add(comment);
                article.Tags.Add(tag);
                articles.Add(article);

                if (i % 20 == 0)
                {
                    context.Articles.AddOrUpdate(articles.ToArray());
                    articles.Clear();
                }
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

        private string GenerateLoremIpsum(int minWords, int maxWords,
            int minSentences, int maxSentences, int numParagraphs)
        {

            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
        "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
        "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

            var rand = new Random();
            int numSentences = rand.Next(maxSentences - minSentences)
                + minSentences + 1;
            int numWords = rand.Next(maxWords - minWords) + minWords + 1;

            var result = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0)
                        { 
                            result.Append(" ");
                        }
                        result.Append(words[rand.Next(words.Length)]);
                    }
                    result.Append(". ");
                }

                result.Append("\n");
                if (p % 3 == 0)
                {
                    result.Append("\n");
                }
            }

            return result.ToString();
        }
    }
}
