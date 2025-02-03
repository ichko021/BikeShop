using BikeShop.BL.Services;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;
using Moq;

namespace BikeShop.Tests.BikeServiceTests
{
    public class BikeServiceTests_GetAllBike
    {
        private readonly Mock<IBikeRepository> _bikeServiceMock;
        private readonly Mock<ILogger<BikeService>> _logger;

        private readonly List<Bike> _bikeList = new List<Bike>()
        {
            new Bike()
            {
                id = Guid.NewGuid().ToString(),
                brand = "test brand 1",
                model = "test model 1",
                price = 2,
                availabilityInStore = 2
            },
            new Bike()
            {
                id = Guid.NewGuid().ToString(),
                brand = "test brand 2",
                model = "test model 2",
                price = 2,
                availabilityInStore = 2
            },
            new Bike()
            {
                id = Guid.NewGuid().ToString(),
                brand = "test brand 3",
                model = "test model 3",
                price = 2,
                availabilityInStore = 2
            }
        };

        public BikeServiceTests_GetAllBike()
        {
            _bikeServiceMock = new Mock<IBikeRepository>();
            _logger = new Mock<ILogger<BikeService>>(); 
        }

        [Fact]
        public void GetAllBikes_Ok()
        {
            var count = 3;
            var firstTestBrand = "test brand 1";
            var secondTestBrand = "test brand 2";
            var thirdTestBrand = "test brand 3";

            _bikeServiceMock
                .Setup(b => b.GetAllBikes())
                .Returns(_bikeList);

            var bikeService = new BikeService(_bikeServiceMock.Object, _logger.Object);

            var result = bikeService.GetAllBikes();

            Assert.NotNull(result);
            Assert.Equal(count, result.Count);
            Assert.Equal(firstTestBrand, result[0].brand);
            Assert.Equal(secondTestBrand, result[1].brand);
            Assert.Equal(thirdTestBrand, result[2].brand);
        }

        [Fact]
        public void GetAllBikes_Empty()
        {
            _bikeServiceMock
                .Setup(b => b.GetAllBikes())
                .Returns(new List<Bike>());

            var bikeService = new BikeService(_bikeServiceMock.Object, _logger.Object);

            var result = bikeService.GetAllBikes();

            Assert.Empty(result);
        }
    }
}
