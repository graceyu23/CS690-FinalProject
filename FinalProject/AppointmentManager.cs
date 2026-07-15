using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CustomerManagement
{
    public class Appointment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int StylistId { get; set; }
        public DateTime DateTime { get; set; }
        public string Notes { get; set; }
    }

    public class Stylist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
        public Dictionary<string, bool> Availability { get; set; } = new Dictionary<string, bool>(); // derived from WorkingHours on load
        public Dictionary<string, (TimeSpan Start, TimeSpan End)> WorkingHours { get; set; } = new Dictionary<string, (TimeSpan Start, TimeSpan End)>();
    }

    public class AppointmentManager
    {
        private List<Appointment> _appointments = new List<Appointment>();
        private int _nextAppointmentId = 1;
        private string _appointmentFile = "appointments.txt";

        private List<Stylist> _stylists = new List<Stylist>();
        private int _nextStylistId = 1;
        private string _stylistFile = "stylists.txt";

        public AppointmentManager()
        {
            LoadAppointments();
            LoadStylists();
            if (_stylists.Count == 0) AddDefaultStylists();
        }

        // ===== Appointments =====

        public Appointment AddAppointment(int customerId, int serviceId, int stylistId, DateTime dateTime, string notes)
        {
            if (IsStylistBusy(stylistId, dateTime))
            {
                Console.WriteLine($"\n❌ Stylist {stylistId} is already booked at {dateTime:yyyy-MM-dd HH:mm}.\n");
                return null;
            }

            var a = new Appointment
            {
                Id = _nextAppointmentId++,
                CustomerId = customerId,
                ServiceId = serviceId,
                StylistId = stylistId,
                DateTime = dateTime,
                Notes = notes
            };
            _appointments.Add(a);
            SaveAppointments();
            Console.WriteLine($"\n✅ Appointment added! ID: {a.Id}\n");
            return a;
        }

        public void DisplayAllAppointments()
        {
            if (_appointments.Count == 0) { Console.WriteLine("\n--- No appointments. ---\n"); return; }
            Console.WriteLine("\n--- All Appointments ---");
            foreach (var a in _appointments)
                Console.WriteLine($"ID: {a.Id} | Customer: {a.CustomerId} | Service: {a.ServiceId} | Stylist: {a.StylistId} | {a.DateTime:yyyy-MM-dd HH:mm} | Notes: {a.Notes}");
            Console.WriteLine("------------------------\n");
        }

        public void RemoveAppointment(int id)
        {
            var a = _appointments.Find(x => x.Id == id);
            if (a == null) { Console.WriteLine($"\n--- Appointment {id} not found. ---\n"); return; }
            _appointments.Remove(a);
            SaveAppointments();
            Console.WriteLine($"\n✅ Appointment {id} removed.\n");
        }

        private bool IsStylistBusy(int stylistId, DateTime dateTime)
        {
            return _appointments.Any(a => a.StylistId == stylistId && a.DateTime == dateTime);
        }

        // ===== Stylist management =====

        private void AddDefaultStylists()
        {
            // Sally
            AddStylist("Sally", new List<string> { "Haircut", "Color" },
                new Dictionary<string, bool>
                {
                    { "Monday", true }, { "Tuesday", true }, { "Wednesday", false },
                    { "Thursday", true }, { "Friday", true }, { "Saturday", false }, { "Sunday", true }
                },
                new Dictionary<string, (TimeSpan Start, TimeSpan End)>
                {
                    { "Monday", (TimeSpan.FromHours(10), TimeSpan.FromHours(18)) },
                    { "Tuesday", (TimeSpan.FromHours(13), TimeSpan.FromHours(21)) },
                    { "Thursday", (TimeSpan.FromHours(10), TimeSpan.FromHours(18)) },
                    { "Friday", (TimeSpan.FromHours(13), TimeSpan.FromHours(21)) },
                    { "Sunday", (TimeSpan.FromHours(10), TimeSpan.FromHours(18)) }
                });

            // Mike
            AddStylist("Mike", new List<string> { "Haircut", "Color", "Shaving" },
                new Dictionary<string, bool>
                {
                    { "Monday", false }, { "Tuesday", true }, { "Wednesday", true },
                    { "Thursday", true }, { "Friday", true }, { "Saturday", true }, { "Sunday", false }
                },
                new Dictionary<string, (TimeSpan Start, TimeSpan End)>
                {
                    { "Tuesday", (TimeSpan.FromHours(10), TimeSpan.FromHours(18)) },
                    { "Wednesday", (TimeSpan.FromHours(13), TimeSpan.FromHours(21)) },
                    { "Thursday", (TimeSpan.FromHours(13), TimeSpan.FromHours(21)) },
                    { "Friday", (TimeSpan.FromHours(10), TimeSpan.FromHours(18)) },
                    { "Saturday", (TimeSpan.FromHours(10), TimeSpan.FromHours(18)) }
                });

            // Emma
            AddStylist("Emma", new List<string> { "Haircut", "Color", "Highlight" },
                new Dictionary<string, bool>
                {
                    { "Monday", false }, { "Tuesday", false }, { "Wednesday", true },
                    { "Thursday", true }, { "Friday", true }, { "Saturday", true }, { "Sunday", true }
                },
                new Dictionary<string, (TimeSpan Start, TimeSpan End)>
                {
                    { "Wednesday", (TimeSpan.FromHours(13), TimeSpan.FromHours(21)) },
                    { "Thursday", (TimeSpan.FromHours(10), TimeSpan.FromHours(18)) },
                    { "Friday", (TimeSpan.FromHours(13), TimeSpan.FromHours(21)) },
                    { "Saturday", (TimeSpan.FromHours(10), TimeSpan.FromHours(18)) },
                    { "Sunday", (TimeSpan.FromHours(10), TimeSpan.FromHours(18)) }
                });
        }

        public Stylist AddStylist(string name, List<string> skills, Dictionary<string, bool> availability, Dictionary<string, (TimeSpan Start, TimeSpan End)> workingHours)
        {
            var s = new Stylist
            {
                Id = _nextStylistId++,
                Name = name,
                Skills = skills,
                Availability = availability,
                WorkingHours = workingHours
            };
            _stylists.Add(s);
            SaveStylists();
            Console.WriteLine($"\n✅ Stylist {s.Name} added (ID {s.Id})\n");
            return s;
        }

        public List<Stylist> FindAvailableStylists(string skill, DateTime dateTime)
        {
            var dayName = dateTime.DayOfWeek.ToString();
            var timeOfDay = dateTime.TimeOfDay;

            var candidates = _stylists.Where(s =>
                s.Skills.Any(k => k.Equals(skill, StringComparison.OrdinalIgnoreCase)) &&
                s.Availability.ContainsKey(dayName) && s.Availability[dayName] &&
                s.WorkingHours.ContainsKey(dayName) &&
                timeOfDay >= s.WorkingHours[dayName].Start &&
                timeOfDay <= s.WorkingHours[dayName].End
            ).ToList();

            var available = candidates.Where(s => !IsStylistBusy(s.Id, dateTime)).ToList();
            return available;
        }

        public void DisplayAllStylists()
        {
            if (_stylists.Count == 0) { Console.WriteLine("\n--- No stylists. ---\n"); return; }
            Console.WriteLine("\n--- All Stylists ---");
            foreach (var s in _stylists)
            {
                string days = string.Join(", ", s.Availability.Where(kv => kv.Value).Select(kv => kv.Key));
                string hours = string.Join(", ", s.WorkingHours.Select(kv => $"{kv.Key}: {kv.Value.Start:hh\\:mm} - {kv.Value.End:hh\\:mm}"));
                Console.WriteLine($"ID: {s.Id} | {s.Name} | Skills: {string.Join(", ", s.Skills)} | Days: {days} | Hours: {hours}");
            }
            Console.WriteLine("----------------------\n");
        }

        // ===== File Managemet =====

        private void SaveAppointments()
        {
            using var w = new StreamWriter(_appointmentFile);
            w.WriteLine("Id|CustomerId|ServiceId|StylistId|DateTime|Notes");
            foreach (var a in _appointments)
                w.WriteLine($"{a.Id}|{a.CustomerId}|{a.ServiceId}|{a.StylistId}|{a.DateTime:yyyy-MM-dd HH:mm}|{a.Notes}");
        }

        private void LoadAppointments()
        {
            if (!File.Exists(_appointmentFile)) return;
            _appointments.Clear();
            foreach (var line in File.ReadAllLines(_appointmentFile))
            {
                var p = line.Split('|');
                if (p.Length == 6)
                {
                    var a = new Appointment
                    {
                        Id = int.Parse(p[0]),
                        CustomerId = int.Parse(p[1]),
                        ServiceId = int.Parse(p[2]),
                        StylistId = int.Parse(p[3]),
                        DateTime = DateTime.Parse(p[4]),
                        Notes = p[5]
                    };
                    _appointments.Add(a);
                    if (a.Id >= _nextAppointmentId) _nextAppointmentId = a.Id + 1;
                }
            }
        }

        private void SaveStylists()
        {
            using var w = new StreamWriter(_stylistFile);
            foreach (var s in _stylists)
            {
                string skills = string.Join(";", s.Skills);
                string workingHours = string.Join(";", s.WorkingHours.Select(kv =>
                    $"{kv.Key}:{kv.Value.Start.Hours}:{kv.Value.Start.Minutes}:{kv.Value.End.Hours}:{kv.Value.End.Minutes}"));
                // Availability is NOT saved – it will be rebuilt from WorkingHours on load.
                w.WriteLine($"{s.Id}|{s.Name}|{skills}|{workingHours}");
            }
        }

        private void LoadStylists()
        {
            if (!File.Exists(_stylistFile)) return;
            _stylists.Clear();
            foreach (var line in File.ReadAllLines(_stylistFile))
            {
                var p = line.Split('|');
                if (p.Length == 4)  // Id, Name, Skills, WorkingHours
                {
                    var skills = new List<string>(p[2].Split(';', StringSplitOptions.RemoveEmptyEntries));
                    var workingHours = new Dictionary<string, (TimeSpan Start, TimeSpan End)>();
                    foreach (var entry in p[3].Split(';', StringSplitOptions.RemoveEmptyEntries))
                    {
                        var parts = entry.Split(':');
                        if (parts.Length == 5)
                        {
                            var day = parts[0];
                            var start = new TimeSpan(int.Parse(parts[1]), int.Parse(parts[2]), 0);
                            var end = new TimeSpan(int.Parse(parts[3]), int.Parse(parts[4]), 0);
                            workingHours[day] = (start, end);
                        }
                    }

                    // Rebuild Availability from WorkingHours (available = has working hours)
                    var availability = new Dictionary<string, bool>();
                    foreach (var day in workingHours.Keys)
                        availability[day] = true;

                    var s = new Stylist
                    {
                        Id = int.Parse(p[0]),
                        Name = p[1],
                        Skills = skills,
                        Availability = availability,
                        WorkingHours = workingHours
                    };
                    _stylists.Add(s);
                    if (s.Id >= _nextStylistId) _nextStylistId = s.Id + 1;
                }
            }
        }
    }
}