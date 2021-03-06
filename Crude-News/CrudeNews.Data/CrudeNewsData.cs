﻿namespace CrudeNews.Data
{
    using System;
    using System.Collections.Generic;

    using CrudeNews.Data.Repositories;
    using CrudeNews.Models;
    using System.Data.Entity.Validation;
    using System.Data.Entity.Core;

    public class CrudeNewsData : ICrudeNewsData
    {
        private CrudeNewsDbContext context;
        private IDictionary<Type, object> repositories;

        public CrudeNewsData()
            : this(new CrudeNewsDbContext())
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
            get { return this.GetRepository<Tag>(); }
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
                var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), this.context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
