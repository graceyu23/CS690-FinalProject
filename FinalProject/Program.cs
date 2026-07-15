using System;
using ServiceManagement;

namespace CustomerManagement
{
    public class Program
    {
        public static void Main()
        {
            var cm = new CustomerManager();
            var sm = new ServiceManager();
            var am = new AppointmentManager();

            Console.WriteLine("\n=== Welcome ===\n");
            while (true)
            {
                Console.WriteLine("1. Customers\n2. Services\n3. Appointments\n4. Stylists\n5. Exit");
                Console.Write("Choice: ");
                switch (Console.ReadLine())
                {
                    case "1": ManageCustomers(cm); break;
                    case "2": ManageServices(sm); break;
                    case "3": ManageAppointments(am); break;
                    case "4": ManageStylists(am); break;
                    case "5": Console.WriteLine("Goodbye!"); return;
                    default: Console.WriteLine("Invalid."); break;
                }
            }
        }

        static void ManageCustomers(CustomerManager cm)
        {
            while (true)
            {
                Console.WriteLine("=== Manage Customers ===\n");
                Console.WriteLine("\n1.Add Customer\n2.Display all Customers\n3.Find Customer\n4.Update Customer Balance\n5.Update Customer Information\n6.Back");
                Console.Write("Choice: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Name: "); string name = Console.ReadLine();
                        Console.Write("Phone: "); string phone = Console.ReadLine();
                        Console.Write("Email: "); string email = Console.ReadLine();
                        Console.Write("Balance(number only): "); decimal balance = Convert.ToDecimal(Console.ReadLine());
                        cm.AddCustomer(name, phone, email, balance);
                        break;
                    case "2": cm.DisplayAllCustomers(); break;
                    case "3":
                        Console.Write("ID: "); int id = Convert.ToInt32(Console.ReadLine());
                        cm.DisplayCustomerById(id); break;
                    case "4":
                        Console.Write("ID: "); int cid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("New balance: "); decimal nb = Convert.ToDecimal(Console.ReadLine());
                        cm.UpdateCustomerBalance(cid, nb); break;
                    case "5":
                        Console.Write("ID: ");
                        int updateId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("New name (or Enter to keep): ");
                        string newName = Console.ReadLine();
                        Console.Write("New phone (or press Enter to keep): ");
                        string newPhone = Console.ReadLine();
                        Console.Write("New email (or press Enter to keep): ");
                        string newEmail = Console.ReadLine();
                        cm.UpdateCustomerInformation(updateId, newName, newPhone, newEmail);
                        break;
                    case "6": return;
                    default: Console.WriteLine("Invalid."); break;
                }
            }
        }

        static void ManageServices(ServiceManager sm)
        {
            while (true)
            {
                Console.WriteLine("=== Manage Services ===\n");
                Console.WriteLine("1. Add Service\n2. Remove Service\n3. Update Service\n4. Display all Services\n5. Back");
                Console.Write("Choice: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Name: "); string name = Console.ReadLine();
                        Console.Write("Duration (in minutes): "); int dur = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Price (in dollars): "); int price = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Products: "); string prod = Console.ReadLine();
                        sm.AddService(name, dur, price, prod);
                        break;
                    case "2":
                        Console.Write("ID to remove: "); int id = Convert.ToInt32(Console.ReadLine());
                        sm.RemoveService(id); break;
                    case "3":
                        Console.Write("ID: "); int uid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("New name (or Enter): "); string nn = Console.ReadLine();
                        Console.Write("New duration in minutes (or -1 to keep): "); int? nd = int.TryParse(Console.ReadLine(), out int d) ? (d >= 0 ? d : (int?)null) : null;
                        Console.Write("New price in dollars (or -1 to keep): "); int? np = int.TryParse(Console.ReadLine(), out int p) ? (p >= 0 ? p : (int?)null) : null;
                        Console.Write("New products (or Enter): "); string npr = Console.ReadLine();
                        sm.UpdateServiceDetails(uid, nn, nd, np, npr); break;
                    case "4": sm.DisplayAllServices(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid."); break;
                }
            }
        }

        static void ManageAppointments(AppointmentManager am)
        {
            while (true)
            {
                Console.WriteLine("=== Manage Appointments ===\n");
                Console.WriteLine("\n1.Add Appointment\n2.Display all Appointments\n3.Remove Appointment\n4.Back");
                Console.Write("Choice: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Customer ID: "); int cid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Service ID: "); int sid = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Date (yyyy-MM-dd HH:mm): "); DateTime dt = DateTime.Parse(Console.ReadLine());
                        Console.Write("Notes: "); string notes = Console.ReadLine();
                        Console.Write("Stylist ID: "); int stylistId = Convert.ToInt32(Console.ReadLine());
                        am.AddAppointment(cid, sid, stylistId, dt, notes);
                        break;
                    case "2": am.DisplayAllAppointments(); break;
                    case "3":
                        Console.Write("ID to remove: "); int id = Convert.ToInt32(Console.ReadLine());
                        am.RemoveAppointment(id); break;
                    case "4": return;
                    default: Console.WriteLine("Invalid."); break;
                }
            }
        }

        static void ManageStylists(AppointmentManager am)
        {
            while (true)
            {
                Console.WriteLine("\n=== Manage Stylists ===");
                Console.WriteLine("1. Display all stylists");
                Console.WriteLine("2. Find available stylists by skill and day");
                Console.WriteLine("3. Back");
                Console.Write("Choice: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        am.DisplayAllStylists();
                        break;
                    case "2":
                        Console.Write("Skill (e.g., Haircut, Color, Highlights, Shaving): ");
                        string skill = Console.ReadLine();
                        Console.Write("Enter date and time (yyyy-MM-dd HH:mm): ");
                        DateTime dateTime = DateTime.Parse(Console.ReadLine());
                        var available = am.FindAvailableStylists(skill, dateTime);
                        if (available.Count == 0)
                            Console.WriteLine($"\nNo stylists available for '{skill}' at {dateTime:yyyy-MM-dd HH:mm}.\n");
                        else
                        {
                            Console.WriteLine($"\n✅ Available stylists for '{skill}' at {dateTime:yyyy-MM-dd HH:mm}:");
                            foreach (var s in available)
                                Console.WriteLine($"- {s.Name} (ID {s.Id})");
                            Console.WriteLine();
                        }
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid.");
                        break;
                }
            }
        }
    }
}