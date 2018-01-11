using System.Collections.Generic;
using System.Data.Entity;

namespace Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        DbContext db;
        IDbSet<T> dbSet;

        public BaseRepository(DbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        }

        public void Create(T obj)
        {
            dbSet.Add(obj);
            db.SaveChanges();
        }

        public void Delete(T obj)
        {
            dbSet.Remove(obj);
            db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public T GetById(int? id)
        {
            return dbSet.Find(id);
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}