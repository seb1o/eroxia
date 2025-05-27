using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia.model
{
    internal class Product
    {

        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string? Material { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public string? Color { get; set; }

        public Product(int idProduct, string name, string manufacturer, decimal price)
        {
            IdProduct = idProduct;
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
        }
    }
}
