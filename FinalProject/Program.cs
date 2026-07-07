using System;

namespace CustomerManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager manager = new CustomerManager();
            int choice = 0;

            Console.WriteLine("=== Welcome to the Customer Manager ===");

            while (choice != 5)
            {
                Console.WriteLine("\n--- Menu ---");
                Console.WriteLine("1. Add a new customer");
                Console.WriteLine("2. Display all customers");
                Console.WriteLine("3. Display customer by ID");
                Console.WriteLine("4. Update customer balance");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice (1-5): ");

                choice = Convert.ToInt32(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Write("Enter customer name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter phone number: ");
                    string phone = Console.ReadLine();

                    Console.Write("Enter email address: ");
                    string email = Console.ReadLine();

                    Console.Write("Enter starting balance: ");
                    decimal balance = Convert.ToDecimal(Console.ReadLine());

                    Customer newCustomer = manager.AddCustomer(name, phone, email, balance);

                    Console.WriteLine("\n✅ Customer added successfully!");
                    Console.WriteLine($"   ID: {newCustomer.Id}");
                    Console.WriteLine($"   Name: {newCustomer.Name}");
                    Console.WriteLine($"   Phone: {newCustomer.Phone}");
                    Console.WriteLine($"   Email: {newCustomer.Email}");
                    Console.WriteLine($"   Balance: ${newCustomer.Balance}\n");
                }

                else if (choice == 2)
                {
                    manager.DisplayAllCustomers();  
                }

                else if (choice == 3)
                {
                    Console.Write("Enter the Customer ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    manager.DisplayCustomerById(id);  
                }

                // ---- Option 4: Update balance ----
                else if (choice == 4)
                {
                    Console.Write("Enter the Customer ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter the new balance: ");
                    decimal newBalance = Convert.ToDecimal(Console.ReadLine());

                    manager.UpdateCustomerBalance(id, newBalance);  
                }

                // ---- Option 5: Exit ----
                else if (choice == 5)
                {
                    Console.WriteLine("Goodbye!");
                }

                // ---- Invalid choice ----
                else
                {
                    Console.WriteLine("Invalid choice. Please type 1, 2, 3, 4, or 5.");
                }
            }
        }
    }
}