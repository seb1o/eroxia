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
                Console.WriteLine("3. Exit");
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
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private void ShowEmployees()
        {
            throw new NotImplementedException();
        }

        private void ShowProducts()
        {
            throw new NotImplementedException();
        }
    }
}
