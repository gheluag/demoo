using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllForLife.Entity
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Article { get; set; }
        public decimal Price { get; set; }
        public int CurrentSale { get; set; }
        public int MaxSale { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public string? ImageURL { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public string Brand { get; set; }
    }
}
