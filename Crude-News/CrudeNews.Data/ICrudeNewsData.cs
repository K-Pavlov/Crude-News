namespace CrudeNews.Data
{
    using CrudeNews.Data.Repositories;
    using CrudeNews.Models;

    public interface ICrudeNewsData
    {
        IRepository<User> Users { get; }

        IRepository<Article> Articles { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Category> Categories { get; }

        IRepository<Tag> Tags { get; }

        int SaveChanges();
    }
}
