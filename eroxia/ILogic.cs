using eroxia.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia
{
    internal interface ILogic
    {
        public bool DeleteProduct(int idProduct);
        public bool InsertProduct(Product product);
        public List<Employee> GetEmployees();
        public List<Product> GetProducts();
        public List<Client> GetClients();
    }
}