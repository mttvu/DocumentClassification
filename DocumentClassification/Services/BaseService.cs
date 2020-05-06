using DocumentClassification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClassification.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly DocumentClassificationContext Context;

        public BaseService(DocumentClassificationContext context) 
        {
            Context = context;
        }

        public void Delete(int id)
        {
            var document = Get(id);
            Context.Remove(document);
            Context.SaveChanges();
        }

        public TEntity Get(int? id)
        {
            return Context.Set<TEntity>().Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }
    }
}
