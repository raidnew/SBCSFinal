using API.Context;
using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public abstract class Entries<T> : IEntriesStorage<T> where T : class, IDbEntity
    {
        protected DBContext DBContext { get; set; }
        private DbSet<T> _storage;

        public Entries(DBContext dBContext, DbSet<T> storage)
        {
            DBContext = dBContext;
            _storage = storage;
        }

        public void Add(T obj)
        {
            _storage.Add(obj);
            DBContext.SaveChanges();
        }

        public abstract void Edit(T obj);
        public IEnumerable<T> GetAll()
        {
            return _storage;
        }

        public T GetById(int id)
        {
            //return _storage.Where<T>(_ => _.Id == id).FirstOrDefault();
            return _storage.FirstOrDefault(o => o.Id == id);
        }

        public void Remove(int id)
        {
            _storage.Remove(GetById(id));
            DBContext.SaveChanges();
        }


    }
}
