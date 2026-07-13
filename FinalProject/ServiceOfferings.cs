using System;
using System.Collections.Generic;

namespace ServiceManagement
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string ProductsRequired {get; set;}
        public override string ToString()
        {
            return $"ID:{ServiceId, -3} | {ServiceName, -16} | Duration:{Duration, -8} | Price:{Price, -6} | Products: {ProductsRequired}";
        }
    } 

public class ServiceManager
{
    private List<Service> _services = new List<Service>();
    private int _nextId = 1;
    
    // ========== FR1: Add a new service ==========
    public Service AddService(string name, string duration, string price, string products_required)
        {
            Service newService = new Service
            {
                ServiceId = _nextId++,
                ServiceName = name,
                Duration = duration,
                Price = price,
                ProductsRequired = products_required
            };
             

            _services.Add(newService);
            Console.WriteLine($"\n✅ Service ID {id} added successfully.\n");
            return newService;
        }

        // ========== FR2: Remove a service from the menu ==========
        public bool RemoveService(int id)
        {
            Service serviceToRemove = _services.Find(s => s.ServiceId == id);
            if (serviceToRemove == null)
            {
                Console.WriteLine($"\n--- Service with ID {id} not found. ---\n");
                return false;
            }

            _services.Remove(serviceToRemove);
            Console.WriteLine($"\n✅ Service '{serviceToRemove.ServiceName}' (ID {id}) removed successfully.\n");
            return true;
        }

        // ========== FR3: Update service details ==========
        public bool UpdateServiceDetails(int id, string newName, string newDuration, string newPrice, string newProducts)
        {
            Service service = _services.Find(s => s.ServiceId == id);
            if (service == null)
            {
                Console.WriteLine($"\n--- Service with ID{id} not found. ---\n");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(newName))
            {
                service.ServiceName = newName;
            }
            if (!string.IsNullOrWhiteSpace(newDuration))
                service.Duration = newDuration;
            if (!string.IsNullOrWhiteSpace(newPrice))
                service.Price = newPrice;
            if (!string.IsNullOrWhiteSpace(newProducts))
                service.ProductsRequired = newProducts;

            Console.WriteLine($"\n✅ Service ID {id} updated successfully.\n");
            return true;
        }
         

        // ========== FR4: Display all available services ==========
        public void DisplayAllServices()
        {
            if (_services.Count == 0)
            {
                Console.WriteLine("\n--- No services in the system. ---\n");
                return;
            }

            Console.WriteLine("\n--- All Services ---");
            foreach (Service s in _services)
            {
                Console.WriteLine(s.ToString());
            }
            Console.WriteLine("----------------------\n");
        }

    
}