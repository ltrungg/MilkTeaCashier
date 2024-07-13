using Microsoft.EntityFrameworkCore;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceBase<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly MilkTeaCashierContext _context;

        public ServiceBase()
        {
            _context = new MilkTeaCashierContext();
            _dbSet = _context.Set<T>();
        }
        public List<T> GetAll() => _dbSet.ToList();
        public T? Get(int id) => _dbSet.Find(id);
        
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
        
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public T? GetByName(string name)
        {
            return _dbSet.Find(name);
        }

        /// <summary>
        /// Searches for entities in the DbSet that match the given predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A list of entities that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        public List<T> Search(Func<T, bool> predicate) => _dbSet.Where(predicate).ToList();
     }
}
