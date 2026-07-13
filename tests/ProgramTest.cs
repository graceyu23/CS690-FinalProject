using System;
using System.IO;
using Xunit;
using ServiceManagement;    
using CustomerManagement;

namespace FinalProject.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_AddCustomer_ShowsSuccessMessage()
        {
            var input = "1\nJohn Doe\n555-1234\njohn@test.com\n100\n9\n";
            var output = new StringWriter();
            Console.SetIn(new StringReader(input));
            Console.SetOut(output);
            Program.Main(new string[0]);
            Assert.Contains("Customer added successfully", output.ToString());
        }
    }
}