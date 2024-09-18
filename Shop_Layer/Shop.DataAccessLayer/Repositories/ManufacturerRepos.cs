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
    public class ManufacturerRepos : IRepository<Manufacturer>
    {
        private readonly DbProductContext _context;

        public ManufacturerRepos(DbProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Manufacturer>> GetAll()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer?> Get(Guid id)
        {
            return await _context.Manufacturers.FindAsync(id);
        }

        public async Task<IEnumerable<Manufacturer>> Find(Func<Manufacturer, bool> predicate)
        {
            return _context.Manufacturers.Where(predicate).ToList();
        }

        public async Task Create(Manufacturer item)
        {
            await _context.Manufacturers.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Manufacturer item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer is null)
            {
                return;
            }
            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();
        }
    }
}

