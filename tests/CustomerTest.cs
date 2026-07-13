using Xunit;
using System;
using System.IO;
using CustomerManagement; 

namespace FinalProject.Tests
{
    public class CustomerManagerTests
    {
        [Fact]
        public void AddAndUpdateCustomer_Works()
        {
            var manager = new CustomerManager();
            var c = manager.AddCustomer("Alice", "555-1234", "alice@test.com", 100);
            Assert.Equal(1, c.Id);                    

            manager.UpdateCustomerBalance(c.Id, 200);  

            var output = new StringWriter();
            Console.SetOut(output);
            manager.DisplayCustomerById(c.Id);
            Console.SetOut(Console.Out);

            Assert.Contains("$200", output.ToString());  
        }
    }
}