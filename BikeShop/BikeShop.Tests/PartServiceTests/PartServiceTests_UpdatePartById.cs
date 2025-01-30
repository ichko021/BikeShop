using BikeShop.BL.Services;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Moq;

namespace BikeShop.Tests.PartServiceTests
{
    public class PartServiceTests_UpdatePartById
    {

        private readonly Mock<IPartRepository> _partRepositoryMock;

        public PartServiceTests_UpdatePartById()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
        }

        [Fact]
        public void UpdateBikeById_Ok()
        {
            var testObject = new Part()
            {
                id = Guid.NewGuid().ToString(),
                partName = "test name 1",
                partSpec = "test spec 1",
            };

            var updatedTestObject = new Part()
            {
                id = Guid.NewGuid().ToString(),
                partName = "test name update",
                partSpec = "test spec update",
            };

            _partRepositoryMock
                .Setup(b => b.GetPartById(testObject.id))
                .Returns(testObject);

            _partRepositoryMock
                .Setup(b => b.UpdatePartById(testObject.id, It.IsAny<Part>()))
                .Returns(testObject);

            var partService = new PartService(_partRepositoryMock.Object);

            var response = partService.UpdatePartById(testObject.id, updatedTestObject);

            Assert.NotNull(response);
            Assert.Equal(response.partName, updatedTestObject.partName);
            Assert.Equal(response.partSpec, updatedTestObject.partSpec);
        }

        [Fact]
        public void UpdatePartById_InvalidId()
        {
            var testObject = new Part();

            var updatedTestObject = new Part()
            {
                id = Guid.NewGuid().ToString(),
                partName = "test name update",
                partSpec = "test spec update",
            };

            _partRepositoryMock
                .Setup(b => b.GetPartById("1234"))
                .Returns(testObject);

            _partRepositoryMock
                .Setup(b => b.UpdatePartById("1234", It.IsAny<Part>()))
                .Returns(testObject);

            var partService = new PartService(_partRepositoryMock.Object);

            var response = partService.UpdatePartById(testObject.id, updatedTestObject);

            Assert.Null(response);
        }
    }
}

