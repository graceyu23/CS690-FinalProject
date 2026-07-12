using Xunit;
using ServiceManagement;

namespace FinalProject.Tests
{
    public class RemoveServiceTest
    {
        [Fact]
        public void RemoveService_ById()
        {
            var manager = new ServiceManager();
            var service = manager.AddService("Shaving", "40 min", "$50", "Shaving Cream");
            int id = service.ServiceId;

            bool isRemoved = manager.RemoveService(id);

            Assert.True(isRemoved);
        }
    }
}