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
    public class UnitOfWorkEF : IUnitOfWork
    {
        private bool disposed = false;

        private readonly DbProductContext _context;
        private readonly ProductRepos _productRepos;
        private readonly ManufacturerRepos _manufacturerRepos;
        private readonly CategoryRepos _categoryRepos;

        public IRepository<Product> Products => _productRepos;
        public IRepository<Manufacturer> Manufacturers => _manufacturerRepos;
        public IRepository<Category> Categories => _categoryRepos;

        public UnitOfWorkEF(DbProductContext context)
        {
            _context = context;
            _productRepos = new ProductRepos(context);
            _manufacturerRepos = new ManufacturerRepos(context);
            _categoryRepos = new CategoryRepos(context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                _context.Dispose();
                disposed = true;
            }
        }
         public async Task SaveChanges() => await _context.SaveChangesAsync();
    }

}
