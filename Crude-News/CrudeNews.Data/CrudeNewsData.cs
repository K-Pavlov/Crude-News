namespace CrudeNews.Data
{
    using CrudeNews.Data.Repositories;
    using CrudeNews.Models;
    using System;
    using System.Collections.Generic;

    public class CrudeNewsData : ICrudeNewsData
    {
        private CrudeNewsDbContext context;
        private IDictionary<Type, object> repositories;

        public CrudeNewsData()
            :this(new CrudeNewsDbContext())
        {
        }

        public CrudeNewsData(CrudeNewsDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Article> Articles
        {
            get { return this.GetRepository<Article>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public IRepository<Tag> Tags
        {
            get { return GetRepository<Tag>(); }
        }

        public IRepository<Like> Likes
        {
            get { return this.GetRepository<Like>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
