using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.REpository
{
    public abstract class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity:class
    {
        static DbOrganization db;

        public GenericRepository(DbOrganization ctx)
        {
            db = ctx;
        }

        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity=db.Set<TEntity>().Find(id);
            db.Set<TEntity>().Remove(entity);
            db.SaveChanges();
        }

        public TEntity Find(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public List<TEntity> Listele()
        {
           return db.Set<TEntity>().ToList();
        }

        public void Update(TEntity entity)
        {
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
