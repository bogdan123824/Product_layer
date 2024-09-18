using Microsoft.EntityFrameworkCore;
using Shop.DataAccessLayer.EF;
using Shop.DataAccessLayer.Entities;
using Shop.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccessLayer.Repositories
{
    public class CategoryRepos : IRepository<Category>
    {
        private readonly DbProductContext _context;

        public CategoryRepos(DbProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> Get(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> Find(Func<Category, bool> predicate)
        {
            return _context.Categories.Where(predicate).ToList();
        }

        public async Task Create(Category item)
        {
            await _context.Categories.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Category item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null)
            {
                return;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}

