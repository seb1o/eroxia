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
        private IStorage _storage { get; set; }
        private List<Employee>? Employees { get; set; }
        private List<Product>? Products { get; set; }
        private List<Client>? Clients { get; set; }

        public BusinessLogic(IStorage storage)
        {
            _storage = storage;
        }

        public List<Employee> GetEmployees()
        {
            if (Employees == null)
            {
                try
                {
                    Employees = _storage.GetEmployeesFromDB().Result;
                }
                catch (Exception ex)
                {
                    Employees = new List<Employee>();
                }
            }
            return Employees;
        }

        public List<Product> GetProducts()
        {
            if (Products == null)
            {
                try
                {
                    Products = _storage.GetProductsFromDB().Result;
                    // potrei usare async e await, ma meglio di no senno dovrei usarlo fino al main di program, Result "ferma tutto"
                }
                catch (Exception ex)
                {
                    Products = new List<Product>();
                }
            }
            return Products;

        }

        public bool DeleteProduct(int idProduct)
        {
            var result = _storage.DeleteProductFromDB(idProduct).Result;
            if (result)
            {
                Products?.RemoveAll(p => p.IdProduct == idProduct);
            }
            return result;
        }

        public bool InsertProduct(Product product)
        {
            var resultId = _storage.InsertProductToDB(product).Result;
            if (resultId > 0)
            {
                product.IdProduct = resultId;
                Products?.Add(product);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Client> GetClients()
        {
            if (Clients == null)
            {
                try
                {
                    Clients = _storage.GetClientsFromDB().Result;
                    var employees = GetEmployees();
                    foreach (var client in Clients)
                    {
                        client.Employee = employees.FirstOrDefault(e => e.FiscalCode == client.FiscalCodeEmployee);
                    }
                }
                catch (Exception ex)
                {
                    Clients = new List<Client>();
                }
            }
            return Clients;
        }
    }
}