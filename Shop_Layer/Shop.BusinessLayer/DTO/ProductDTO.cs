using Shop.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BusinessLayer.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Price { get; set; } = int.MaxValue;

        public Guid? CategoryId { get; set; }
        public CategoryDTO? Category { get; set; }

        public Guid? ManufacturerId { get; set; }
        public ManufacturerDTO? Manufacturer { get; set; }
    }
}
