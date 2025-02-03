using BikeShop.BL.Services;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;
using Moq;
namespace BikeShop.Tests.BikeServiceTests
{
    public class BikeServiceTests_UpdateBikeById
    {

        private readonly Mock<IBikeRepository> _bikeRepositoryMock;
        private readonly Mock<ILogger<BikeService>> _logger;

        public BikeServiceTests_UpdateBikeById()
        {
            _bikeRepositoryMock = new Mock<IBikeRepository>();
            _logger = new Mock<ILogger<BikeService>>();
        }

        [Fact]
        public void UpdateBikeById_Ok()
        {
            var testObject = new Bike()
            {
                id = Guid.NewGuid().ToString(),
                brand = "test brand 1",
                model = "test model 1",
                price = 2,
                availabilityInStore = 2
            };

            var updatedTestObject = new Bike()
            {
                id = testObject.id,
                brand = "test brand update",
                model = "test model update",
                price = 2,
                availabilityInStore = 2
            };

            _bikeRepositoryMock
                .Setup(b => b.GetBikeById(testObject.id))
                .Returns(testObject);

            _bikeRepositoryMock
                .Setup(b => b.UpdateBikeById(testObject.id, It.IsAny<Bike>()))
                .Returns(testObject);

            var bikeService = new BikeService(_bikeRepositoryMock.Object, _logger.Object);

            var response = bikeService.UpdateBikeById(testObject.id, updatedTestObject);

            Assert.NotNull(response);
            Assert.Equal(response.brand, updatedTestObject.brand);
            Assert.Equal(response.model, updatedTestObject.model);
            Assert.Equal(response.price, updatedTestObject.price);
            Assert.Equal(response.availabilityInStore, updatedTestObject.availabilityInStore);
        }

        [Fact]
        public void UpdateBikeById_InvalidId()
        {
            var testObject = new Bike();

            var updatedTestObject = new Bike()
            {
                id = testObject.id,
                brand = "test brand update",
                model = "test model update",
                price = 2,
                availabilityInStore = 2
            };

            _bikeRepositoryMock
                .Setup(b => b.GetBikeById("1234"))
                .Returns(testObject);

            _bikeRepositoryMock
                .Setup(b => b.UpdateBikeById("1234", It.IsAny<Bike>()))
                .Returns(testObject);

            var bikeService = new BikeService(_bikeRepositoryMock.Object, _logger.Object);

            var response = bikeService.UpdateBikeById(testObject.id, updatedTestObject);

            Assert.Null(response);
        }
    }
}

