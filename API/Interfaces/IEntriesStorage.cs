using API.Models;
using System.Collections.Generic;

namespace API.Interfaces
{
    public interface IEntriesStorage<T>
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public void EditObj(T obj);
        public void AddOrder(T obj);
        public void Remove(int id);
    }
}
