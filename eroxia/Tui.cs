using eroxia.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia
{
    internal class Tui
    {
        private ILogic Logic { get; set; }
        public Tui(ILogic logic)
        {
            Logic = logic;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Eroxia!");
            while (true)
            {
                Console.WriteLine("1. Show Products");
                Console.WriteLine("2. Show Employees");
                Console.WriteLine("3. insert product");
                Console.WriteLine("4. delete product");
                Console.WriteLine("5. Show Clients ");
                Console.WriteLine("6. Exit");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowProducts();
                        break;
                    case "2":
                        ShowEmployees();
                        break;
                    case "3":
                        insertProduct();
                        break;
                    case "4":
                        deleteProduct();
                        break;
                    case "5":
                        ShowClients();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private void deleteProduct()
        {
            Console.WriteLine("Enter the ID of the product to delete:");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int idProduct))
            {
                if (Logic.DeleteProduct(idProduct))
                {
                    Console.WriteLine($"Product with ID {idProduct} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Product with ID {idProduct} not found or could not be deleted.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format. Please enter a valid integer.");
            }
        }

        private void insertProduct()
        {
            Console.WriteLine("Enter product name:");
            var name = Console.ReadLine();
            Console.WriteLine("Enter product manufacturer:");
            var manufacturer = Console.ReadLine();
            Console.WriteLine("Enter product price:");
            var price = Console.ReadLine();
            Console.WriteLine("Enter product material (optional):");
            var material = Console.ReadLine();
            Console.WriteLine("Enter product color (optional):");
            var color = Console.ReadLine();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(manufacturer) || string.IsNullOrEmpty(price))
            {
                Console.WriteLine("Name, manufacturer, and price are required fields. Please try again.");
                return;
            }

            var product = new Product(name, manufacturer, decimal.Parse(price))
            {
                Material = string.IsNullOrEmpty(material) ? null : material,
                Color = string.IsNullOrEmpty(color) ? null : color
            };

            if (Logic.InsertProduct(product))
            {
                Console.WriteLine("Product inserted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to insert product. Please try again.");
            }
        }

        private void ShowEmployees()
        {
            var employees = Logic.GetEmployees();
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }
        }

        private void ShowProducts()
        {
            var products = Logic.GetProducts();
            foreach (var product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }

        private void ShowClients()
        {
            var clients = Logic.GetClients();
            foreach (var client in clients)
            {
                Console.WriteLine(client.ToString());
            }
        }
    }
}