using System;
using System.Collections.Generic;
using System.IO;

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
        private string _filePath = "customers.txt";

        public CustomerManager()
        {
            LoadFromFile();
            if (_customers.Count == 0)
            {
                // Add 3 default customers
                AddCustomer("Jerry Li", "777-1000", "jerry@sample.com", 100);
                AddCustomer("Jessica Smith", "777-2000", "jessika@sample.com", 300);
                AddCustomer("Ben Kim", "777-3000", "ben@sample.com", 500);
                SaveToFile();
            }
        }

        public Customer AddCustomer(string name, string phone, string email, decimal balance)
        {
            var c = new Customer { Id = _nextId++, Name = name, Phone = phone, Email = email, Balance = balance };
            _customers.Add(c);
            SaveToFile();
            Console.WriteLine($"\n✅ Customer added! ID: {c.Id}\n");
            return c;
        }

        public void DisplayAllCustomers()
        {
            if (_customers.Count == 0) { Console.WriteLine("\n--- No customers. ---\n"); return; }
            Console.WriteLine("\n--- All Customers ---");
            foreach (var c in _customers)
                Console.WriteLine($"ID: {c.Id} | {c.Name} | {c.Phone} | {c.Email} | ${c.Balance}");
            Console.WriteLine("----------------------\n");
        }

        public void DisplayCustomerById(int id)
        {
            var c = _customers.Find(x => x.Id == id);
            if (c == null) { Console.WriteLine($"\n--- Customer {id} not found. ---\n"); return; }
            Console.WriteLine($"\nID: {c.Id}\nName: {c.Name}\nPhone: {c.Phone}\nEmail: {c.Email}\nBalance: ${c.Balance}\n");
        }

        public void UpdateCustomerBalance(int id, decimal newBalance)
        {
            var c = _customers.Find(x => x.Id == id);
            if (c == null) { Console.WriteLine($"\n--- Customer {id} not found. ---\n"); return; }
            c.Balance = newBalance;
            SaveToFile();
            Console.WriteLine($"\n✅ Balance updated for {c.Name} to ${c.Balance}\n");
        }

        private void SaveToFile()
        {
            using var writer = new StreamWriter(_filePath);
            foreach (var c in _customers)
                writer.WriteLine($"{c.Id}|{c.Name}|{c.Phone}|{c.Email}|{c.Balance}");
        }

        private void LoadFromFile()
        {
            if (!File.Exists(_filePath)) return;
            _customers.Clear();
            foreach (var line in File.ReadAllLines(_filePath))
            {
                var parts = line.Split('|');
                if (parts.Length == 5)
                {
                    var c = new Customer
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        Phone = parts[2],
                        Email = parts[3],
                        Balance = decimal.Parse(parts[4])
                    };
                    _customers.Add(c);
                    if (c.Id >= _nextId) _nextId = c.Id + 1;
                }
            }
        }
    }
}