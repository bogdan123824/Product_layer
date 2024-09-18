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
    public class ProductRepos : IRepository<Product>
    {
        private readonly DbProductContext _context;

        public ProductRepos(DbProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> Get(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> Find(Func<Product, bool> predicate)
        {
            return _context.Products.Where(predicate).ToList();
        }

        public async Task Create(Product item)
        {
            await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                return;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}

