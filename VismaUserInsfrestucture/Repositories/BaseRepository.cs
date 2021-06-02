using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VismaUserCore.Entities;
using VismaUserCore.Interfaces;
using VismaUserInsfrestucture.Data;

namespace VismaUserInsfrestucture.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly VismaChallengeContext _context;
        protected DbSet<T> _entities;
        public BaseRepository(VismaChallengeContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity); 
        }

        public async Task<T> Add(T entity)
        {
            _entities.Add(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            T current = await GetById(id);
            _entities.Remove(current);
        }

    }
}
