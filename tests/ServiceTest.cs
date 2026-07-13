using System;
using System.IO;
using Xunit;
using ServiceManagement;

namespace FinalProject.Tests
{
    public class ServiceManagerTests
    {
        [Fact]
        public void AddAndUpdateService_Works()
        {
            var manager = new ServiceManager();
            var s = manager.AddService("Haircut", "30 min", "$40", "Shampoo");

            manager.UpdateServiceDetails(s.ServiceId, "Deluxe Haircut", "45 min", "$55", "Shampoo, Gel");

            var output = new StringWriter();
            Console.SetOut(output);
            manager.DisplayAllServices();
            Console.SetOut(Console.Out);

            string result = output.ToString();
            Assert.Contains("Deluxe Haircut", result);
            Assert.Contains("45 min", result);
            Assert.Contains("$55", result);
        }
    }
}