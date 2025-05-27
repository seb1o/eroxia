using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia.model
{
    internal class Purchase
    {

        public int IdPurchase { get; set; } 
        public Client Client { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ExpeditionDate { get; set; }
        public List<PurchaseProduct> PurchaseProducts { get; set; } = new List<PurchaseProduct>();
        public decimal TotalPrice => PurchaseProducts.Sum(pp => pp.TotalPrice);

        public Purchase(int idPurchase, Client client, List<PurchaseProduct> purchaseProducts)
        {
            IdPurchase = idPurchase;
            Client = client;
            CreationDate = DateTime.Now;
        }
    }
}
