using Shop.BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BusinessLayer.BusinessModel
{
    public class EmptyProduct
    {
        public ProductDTO CreateEmpty()
        {
            return new ProductDTO()
            {
                Name = string.Empty,
                Text = string.Empty,
                Price = int.MaxValue,
                CategoryId = null,
                ManufacturerId = null
            };
        }
    }
}
