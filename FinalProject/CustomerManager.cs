using System;
using System.Collections.Generic;

namespace CustomerManagement
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }

    public class CustomerManager
    {
        private List<Customer> _customers = new List<Customer>();
        private int _nextId = 1;

        // ========== FR1: Add a new customer ==========
        public Customer AddCustomer(string name, string phone, string email, decimal balance)
        {
            Customer newCustomer = new Customer();
            newCustomer.Id = _nextId;
            _nextId = _nextId + 1;
            newCustomer.Name = name;
            newCustomer.Phone = phone;
            newCustomer.Email = email;
            newCustomer.Balance = balance;

            _customers.Add(newCustomer);
            return newCustomer;
        }

        // ========== FR2: Display all customers ==========
        public void DisplayAllCustomers()
        {
            if (_customers.Count == 0)
            {
                Console.WriteLine("\n--- No customers in the system. ---");
                return;
            }

            Console.WriteLine("\n--- All Customers ---");
            foreach (Customer c in _customers)
            {
                Console.WriteLine($"ID: {c.Id} | Name: {c.Name} | Phone: {c.Phone} | Email: {c.Email} | Balance: ${c.Balance}");
            }
            Console.WriteLine("----------------------\n");
        }

        // ========== FR2: Display a customer by ID ==========
        public void DisplayCustomerById(int id)
        {
            Customer foundCustomer = null;
            foreach (Customer c in _customers)
            {
                if (c.Id == id)
                {
                    foundCustomer = c;
                    break;
                }
            }

            if (foundCustomer == null)
            {
                Console.WriteLine($"\n--- Customer with ID {id} not found. ---\n");
                return;
            }

            Console.WriteLine("\n--- Customer Details ---");
            Console.WriteLine($"ID: {foundCustomer.Id}");
            Console.WriteLine($"Name: {foundCustomer.Name}");
            Console.WriteLine($"Phone: {foundCustomer.Phone}");
            Console.WriteLine($"Email: {foundCustomer.Email}");
            Console.WriteLine($"Balance: ${foundCustomer.Balance}");
            Console.WriteLine("------------------------\n");
        }

        // ========== FR3: Update customer balance ==========
        public void UpdateCustomerBalance(int id, decimal newBalance)
        {
            Customer foundCustomer = null;
            foreach (Customer c in _customers)
            {
                if (c.Id == id)
                {
                    foundCustomer = c;
                    break;
                }
            }

            if (foundCustomer == null)
            {
                Console.WriteLine($"\n--- Customer with ID {id} not found. ---\n");
                return;
            }


            // Update the balance
            foundCustomer.Balance = newBalance;
            Console.WriteLine($"\n✅ Balance updated successfully!");
            Console.WriteLine($"   New balance for {foundCustomer.Name}: ${foundCustomer.Balance}\n");
        }
    }
}