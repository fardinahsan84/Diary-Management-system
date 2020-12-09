using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DiaryManagement.Models;
using System.Data.Entity;
using System.Web.ModelBinding;

namespace DigitalDiary.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        DigitalDiaryDataContext context;
        public Repository()
        {
            context = new DigitalDiaryDataContext();
        }
        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Set<TEntity>().Remove(this.Get(id));
            context.SaveChanges();
        }

    }
}