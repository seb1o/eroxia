using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia.model
{
    internal class PurchaseProduct
    {

        public Product Product { get; set; }
        //public Purchase Purchase { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;

        public PurchaseProduct(Product product, Purchase purchase, int quantity)
        {

            Product = product;
            //Purchase = purchase;
            Quantity = quantity;

        }
    }
}