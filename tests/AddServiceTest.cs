using Xunit;
using ServiceManagement;

namespace FinalProject.Tests
{
    public class ShavingServiceTests
    {
        [Fact]
        public void ShavingService_ReturnsCorrectData()
        {
            var expectedName = "Shaving";
            var expectedDuration = "40 min";
            var expectedPrice = "$50";
            var expectedProducts = "Shaving Cream";

            var service = new Service
            {
                ServiceName = expectedName,
                Duration = expectedDuration,
                Price = expectedPrice,
                ProductsRequired = expectedProducts
            };

            Assert.Equal(expectedName, service.ServiceName);
            Assert.Equal(expectedDuration, service.Duration);
            Assert.Equal(expectedPrice, service.Price);
            Assert.Equal(expectedProducts, service.ProductsRequired);
        }
    }
}