using BikeShop.BL.Services;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;
using Moq;

namespace BikeShop.Tests.BikeServiceTests
{
    public class BikeServiceTests_DeleteBikeById
    {
        private readonly Mock<IBikeRepository> _partRepositoryMock;
        private readonly Mock<ILogger<BikeService>> _logger;

        public BikeServiceTests_DeleteBikeById()
        {
            _partRepositoryMock = new Mock<IBikeRepository>();
            _logger = new Mock<ILogger<BikeService>>();
        }

        [Fact]
        public void DeleteBikeById_OK()
        {
            var testObject = new Bike()
            {
                id = Guid.NewGuid().ToString(),
                brand = "test brand 1",
                model = "test model 1",
                price = 2,
                availabilityInStore = 2
            };

            _partRepositoryMock
                .Setup(b => b.DeleteBikeById(testObject.id));

            _partRepositoryMock
                .Setup(b => b.GetBikeById(testObject.id));

            var bikeService = new BikeService(_partRepositoryMock.Object, _logger.Object);

            bikeService.DeleteBikeById(testObject.id);

            var result = bikeService.GetBikeById(testObject.id);

            Assert.Null(result);
        }

        [Fact]
        public void DeleteBikeById_InvalidId()
        {
            var testObject = new Bike()
            {
                id = Guid.NewGuid().ToString(),
                brand = "test brand 1",
                model = "test model 1",
                price = 2,
                availabilityInStore = 2
            };

            _partRepositoryMock
                .Setup(b => b.DeleteBikeById("1234"));

            _partRepositoryMock
                .Setup(b => b.GetBikeById("1234"));

            var bikeService = new BikeService(_partRepositoryMock.Object, _logger.Object);

            bikeService.DeleteBikeById(testObject.id);

            var result = bikeService.GetBikeById(testObject.id);

            Assert.Null(result);
        }
    }
}