using System;
using System.Collections.Generic;
using System.IO;

namespace ServiceManagement
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Duration { get; set; }
        public int Price { get; set; }
        public string ProductsRequired { get; set; }

        public override string ToString()
        {
            return $"ID:{ServiceId, -3} | {ServiceName, -16} | Duration:{Duration, -3} min | Price:${Price, -4} | Products: {ProductsRequired}";
        }
    }

    public class ServiceManager
    {
        private List<Service> _services = new List<Service>();
        private int _nextId = 1;
        private string _filePath = "services.txt";

        public ServiceManager()
        {
            LoadFromFile();
            if (_services.Count == 0)
            {
                AddService("Haircut", 30, 40, "Shampoo, Conditioner");
                AddService("Color Treatment", 90, 120, "Color dye, Developer");
                AddService("Highlights", 60, 90, "Foil, Lightener, Toner");
                SaveToFile();
            }
        }

        public Service AddService(string name, int duration, int price, string products_required)
        {
            var s = new Service
            {
                ServiceId = _nextId++,
                ServiceName = name,
                Duration = duration,
                Price = price,
                ProductsRequired = products_required
            };
            _services.Add(s);
            SaveToFile();

            Console.WriteLine($"\n✅ Service ID {s.ServiceId} added successfully!");
            Console.WriteLine($"ID: {s.ServiceId}");
            Console.WriteLine($"Name: {s.ServiceName}");
            Console.WriteLine($"Duration: {s.Duration} min");
            Console.WriteLine($"Price: ${s.Price}");
            Console.WriteLine($"Products Required: {s.ProductsRequired}\n");
            return s;
        }

        public bool RemoveService(int id)
        {
            var toRemove = _services.Find(s => s.ServiceId == id);
            if (toRemove == null)
            {
                Console.WriteLine($"\n--- Service with ID {id} not found. ---\n");
                return false;
            }
            _services.Remove(toRemove);
            Console.WriteLine($"\n✅ Service '{toRemove.ServiceName}' (ID {id}) removed successfully.\n");
            SaveToFile();
            return true;
        }

        public bool UpdateServiceDetails(int id, string newName, int? newDuration, int? newPrice, string newProducts)
        {
            var service = _services.Find(s => s.ServiceId == id);
            if (service == null)
            {
                Console.WriteLine($"\n--- Service with ID {id} not found. ---\n");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(newName))
                service.ServiceName = newName;
            if (newDuration.HasValue && newDuration.Value >= 0)
                service.Duration = newDuration.Value;
            if (newPrice.HasValue && newPrice.Value >= 0)
                service.Price = newPrice.Value;
            if (!string.IsNullOrWhiteSpace(newProducts))
                service.ProductsRequired = newProducts;

            Console.WriteLine($"\n✅ Service ID {id} updated successfully.\n");
            SaveToFile();
            return true;
        }

        public void DisplayAllServices()
        {
            if (_services.Count == 0)
            {
                Console.WriteLine("\n--- No services in the system. ---\n");
                return;
            }
            Console.WriteLine("\n--- All Services ---");
            foreach (var s in _services)
                Console.WriteLine(s.ToString());
            Console.WriteLine("----------------------\n");
        }

        private void SaveToFile()
        {
            using var writer = new StreamWriter(_filePath);
            foreach (var s in _services)
                writer.WriteLine($"{s.ServiceId}|{s.ServiceName}|{s.Duration}|{s.Price}|{s.ProductsRequired}");
        }

        private void LoadFromFile()
        {
            if (!File.Exists(_filePath)) return;
            _services.Clear();
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length == 5)
                {
                    var s = new Service
                    {
                        ServiceId = int.Parse(parts[0]),
                        ServiceName = parts[1],
                        Duration = int.Parse(parts[2]),
                        Price = int.Parse(parts[3]),
                        ProductsRequired = parts[4]
                    };
                    _services.Add(s);
                    if (s.ServiceId >= _nextId) _nextId = s.ServiceId + 1;
                }
            }
        }
    }
}