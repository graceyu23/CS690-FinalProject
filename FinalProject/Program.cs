using System;
using ServiceManagement;    

namespace CustomerManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            ServiceManager serviceManager = new ServiceManager();

            
            serviceManager.AddService("Haircut", "30 min", "$40", "Shampoo, Conditioner");
            serviceManager.AddService("Color Treatment", "90 min", "$120", "Color dye, Developer");
            serviceManager.AddService("Highlights", "60 min", "$90", "Foil, Lightener, Toner");

            int choice = 0;

            Console.WriteLine("=== Welcome to the Customer & Service Manager ===");

            while (choice != 9)    
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1.  Add a new customer");
                Console.WriteLine("2.  Display all customers");
                Console.WriteLine("3.  Display customer by ID");
                Console.WriteLine("4.  Update customer balance");
                Console.WriteLine("5.  Add a new service");
                Console.WriteLine("6.  Remove a service");
                Console.WriteLine("7.  Update service details");
                Console.WriteLine("8.  Display all services");
                Console.WriteLine("9.  Exit");
                Console.Write("Enter your choice (1-9): ");

                choice = Convert.ToInt32(Console.ReadLine());

                // ---- Customer Operations ----
                if (choice == 1)   // Add customer
                {
                    Console.Write("Enter customer name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter phone number: ");
                    string phone = Console.ReadLine();
                    Console.Write("Enter email address: ");
                    string email = Console.ReadLine();
                    Console.Write("Enter starting balance: ");
                    decimal balance = Convert.ToDecimal(Console.ReadLine());

                    Customer newCustomer = customerManager.AddCustomer(name, phone, email, balance);

                    Console.WriteLine("\n✅ Customer added successfully!");
                    Console.WriteLine($"   ID: {newCustomer.Id}");
                    Console.WriteLine($"   Name: {newCustomer.Name}");
                    Console.WriteLine($"   Phone: {newCustomer.Phone}");
                    Console.WriteLine($"   Email: {newCustomer.Email}");
                    Console.WriteLine($"   Balance: ${newCustomer.Balance}\n");
                }
                else if (choice == 2)   // Display all customers
                {
                    customerManager.DisplayAllCustomers();
                }
                else if (choice == 3)   // Display customer by ID
                {
                    Console.Write("Enter the Customer ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    customerManager.DisplayCustomerById(id);
                }
                else if (choice == 4)   // Update customer balance
                {
                    Console.Write("Enter the Customer ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter the new balance: ");
                    decimal newBalance = Convert.ToDecimal(Console.ReadLine());
                    customerManager.UpdateCustomerBalance(id, newBalance);
                }

                // ---- Service Operations ----
                else if (choice == 5)   // Add service
                {
                    Console.Write("Enter service name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter duration (e.g., '45 min'): ");
                    string duration = Console.ReadLine();
                    Console.Write("Enter price (e.g., '$50'): ");
                    string price = Console.ReadLine();
                    Console.Write("Enter products required: ");
                    string products = Console.ReadLine();

                    var s = serviceManager.AddService(name, duration, price, products);
                    Console.WriteLine($"\n✅ Service added with ID {s.ServiceId}\n");
                }
                else if (choice == 6)   // Remove service
                {
                    Console.Write("Enter service ID to remove: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    serviceManager.RemoveService(id);
                }
                else if (choice == 7)   // Update service
                {
                    Console.Write("Enter service ID to update: ");
                    int uid = Convert.ToInt32(Console.ReadLine());
                    Console.Write("New name (or Enter to keep): ");
                    string nn = Console.ReadLine();
                    Console.Write("New duration (or Enter): ");
                    string nd = Console.ReadLine();
                    Console.Write("New price (or Enter): ");
                    string np = Console.ReadLine();
                    Console.Write("New products (or Enter): ");
                    string npr = Console.ReadLine();
                    serviceManager.UpdateServiceDetails(uid, nn, nd, np, npr);
                }
                else if (choice == 8)   // Display all services
                {
                    serviceManager.DisplayAllServices();
                }
                else if (choice == 9)   // Exit
                {
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number from 1 to 9.");
                }
            }
        }
    }
}