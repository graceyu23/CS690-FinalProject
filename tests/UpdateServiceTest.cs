using Xunit;
using ServiceManagement;

namespace FinalProject.Tests
{
    public class UpdateServiceTest
    {
        [Fact]
        public void UpdateService()
        {
            var manager = new ServiceManager();
            var original = manager.AddService("Shaving", "40 min", "$50", "Shaving Cream");
            int id = original.ServiceId;

            bool isUpdated = manager.UpdateServiceDetails(
                id,
                "Shaving Deluxe",
                "50 min",
                "$60",
                "Shaving Cream, Aftershave"
            );

            Assert.True(isUpdated, "Update should return true for an existing service.");

        }
    }
}