using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BusinessLayer.DTO
{
    public class ManufacturerDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
    }
}
