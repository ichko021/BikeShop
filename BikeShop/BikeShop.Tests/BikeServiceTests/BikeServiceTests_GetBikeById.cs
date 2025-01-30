using BikeShop.BL.Services;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Moq;

namespace BikeShop.Tests.BikeServiceTests
{
    public class BikeServiceTests_GetBikeById
    {
        private readonly Mock<IBikeRepository> _bikeServiceMock;
        private readonly Bike _testObject = new Bike()
        {
            id = Guid.NewGuid().ToString(),
            brand = "test brand 1",
            model = "test model 1",
            price = 2,
            availabilityInStore = 2
        };

        public BikeServiceTests_GetBikeById()
        {
            _bikeServiceMock = new Mock<IBikeRepository>();
        }

        [Fact]
        public void GetBikeById_Ok()
        {
            var testBrand = "test brand 1";
            var testModel = "test model 1";
            var testPrice = 2;
            var testAvailabilityInStore = 2;

            _bikeServiceMock
                .Setup(b => b.GetBikeById(_testObject.id))
                .Returns(_testObject);

            var bikeService = new BikeService(_bikeServiceMock.Object);

            var result = bikeService.GetBikeById(_testObject.id);

            Assert.NotNull(result);
            Assert.Equal(testBrand, result.brand);
            Assert.Equal(testModel, result.model);
            Assert.Equal(testPrice, result.price);
            Assert.Equal(testAvailabilityInStore, result.availabilityInStore);
        }

        [Fact]
        public void GetBikeById_InvalidGuid()
        {
            var invalidTestGuid = "1234";
            var testBrand = "test brand 1";
            var testModel = "test model 1";
            var testPrice = 2;
            var testAvailabilityInStore = 2;

            _bikeServiceMock
                .Setup(b => b.GetBikeById(invalidTestGuid))
                .Returns((Bike)null);

            var bikeService = new BikeService(_bikeServiceMock.Object);

            var result = bikeService.GetBikeById(_testObject.id);

            Assert.Null(result);
        }
    }
}