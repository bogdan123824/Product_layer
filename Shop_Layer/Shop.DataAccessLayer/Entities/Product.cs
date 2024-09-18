using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccessLayer.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Price { get; set; } = int.MaxValue;
        public Category? Category { get; set; }
        public Manufacturer? Manufacturer { get; set; }
    }
}
