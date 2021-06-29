using BenimProjem.BL.Repositories.Abstract;
using BenimProjem.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BenimProjem.BL.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        BenimProjemDbContext _context;

        public Repository()
        {
            _context = ProviderServices.GetContext();
        }

        public T Add(T model)
        {
            GetTable().Add(model);
            Save();
            return model;
        }

        public List<T> Get()
        {
            return GetTable().ToList();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public T GetLast<Key>(Expression<Func<T, Key>> metot)
        {
            return GetTable().OrderByDescending(metot).Take(1).FirstOrDefault();
        }

        public T GetSingle(Expression<Func<T, bool>> metot)
        {
            return GetTable().FirstOrDefault(metot);
        }

        public DbSet<T> GetTable()
        {
            return _context.Set<T>();
        }

        public List<T> GetWhere(Expression<Func<T, bool>> metot)
        {
            return GetTable().Where(metot).ToList();
        }

        public bool Remove(T model)
        {
            try
            {
                GetTable().Remove(model);
                return Save() > 0 ? true : false;
            }
            catch
            {
                return false;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public bool Update(T model)
        {
            _context.Entry<T>(model).State = EntityState.Modified;
            return Save() > 0 ? true : false;
        }
    }
}
