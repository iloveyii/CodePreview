using System.Collections.Generic;

namespace Accounting.Server.Storage
{
    public class MemoryRepository<T> : IRepository<T> 
    {
        private readonly IList<T> entities;

        public MemoryRepository()
        {
            this.entities = new List<T>();
        }
        public void Add(T entity)
        {
            entities.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return entities;
        }

        public void Remove(T entity)
        {
            entities.Remove(entity);
        }
    }
}
