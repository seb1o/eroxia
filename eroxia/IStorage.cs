using eroxia.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia
{
    internal interface IStorage
    {
        public Task<List<Product>> GetProductsFromDB();
    }
}
