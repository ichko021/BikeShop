using BikeShop.BL.Services;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;
using Moq;

namespace BikeShop.Tests.BikeServiceTests
{
    public class BikeServiceTests_AddBike
    {
        private readonly Mock<IBikeRepository> _bikeRepositoryMock;
        private readonly Mock<ILogger<BikeService>> _logger;

        public BikeServiceTests_AddBike()
        {
            _bikeRepositoryMock = new Mock<IBikeRepository>();
            _logger = new Mock<ILogger<BikeService>>();
        }

        [Fact]
        public void AddBike_OK()
        {

            var testObject = new Bike()
            {
                id = Guid.NewGuid().ToString(),
                brand = "test brand 1",
                model = "test model 1",
                price = 2,
                availabilityInStore = 2
            };

            _bikeRepositoryMock
                .Setup(b => b.AddBike(It.IsAny<Bike>()))
                .Callback<Bike>(b => b.id = testObject.id)
                .Returns(testObject);

            var bikeService = new BikeService(_bikeRepositoryMock.Object, _logger.Object);

            var response = bikeService.AddBike(testObject);

            Assert.NotNull(response);
        }

        [Fact]
        public void AddBike_NullObject()
        {
            var testObject = new Bike();

            _bikeRepositoryMock
                .Setup(b => b.AddBike(It.IsAny<Bike>()));

            var bikeService = new BikeService(_bikeRepositoryMock.Object, _logger.Object);

            var response = bikeService.AddBike(testObject);

            Assert.Null(response);
        }
    }
}
