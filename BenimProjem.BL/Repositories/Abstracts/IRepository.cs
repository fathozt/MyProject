using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BenimProjem.BL.Repositories.Abstract
{
   public interface IRepository<T> where T : class
    {
        List<T> Get();
        List<T> GetWhere(Expression<Func<T, bool>> metot);
        T GetSingle(Expression<Func<T, bool>> metot);
        T GetById(int id);
        T Add(T model);
        T GetLast<Key>(Expression<Func<T, Key>> metot);
        bool Remove(T model);
        bool Update(T model);
        int Save();
        DbSet<T> GetTable();
    }
}
