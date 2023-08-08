﻿using System.Collections.Generic;

namespace Accounting.Server.Storage
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Remove(T entity);
        IEnumerable<T> GetAll();
    }
}
