using API.Models;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IEntriesStorage<T>
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public void Edit(T obj);
        public void Add(T obj);
        public void Remove(int id);
    }
}
