using eroxia.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia
{
    internal class BusinessLogic : ILogic
    {
        private IStorage Storage { get; set; }
        private List<Employee> Employees { get; set; } = new List<Employee>();
        private List<Product> Products { get; set; } = new List<Product>();
        public BusinessLogic(IStorage storage)
        {
            Storage = storage;
        }
        public async Task<List<Employee>> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public async  Task<List<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
