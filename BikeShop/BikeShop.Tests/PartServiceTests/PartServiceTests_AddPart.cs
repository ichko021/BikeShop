using BikeShop.BL.Services;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Moq;

namespace BikeShop.Tests.PartServiceTests
{
    public class PartServiceTests_AddPart
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;

        public PartServiceTests_AddPart()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
        }

        [Fact]
        public void AddPart_OK()
        {

            var testObject = new Part()
            {
                id = Guid.NewGuid().ToString(),
                partName = "test name 1",
                partSpec = "test spec 1",
            };

            _partRepositoryMock
                .Setup(b => b.AddPart(It.IsAny<Part>()))
                .Callback<Part>(b => b.id = testObject.id)
                .Returns(testObject);

            var partService = new PartService(_partRepositoryMock.Object);

            var response = partService.AddPart(testObject);

            Assert.NotNull(response);
        }

        [Fact]
        public void AddPart_NullObject()
        {
            var testObject = new Part();

            _partRepositoryMock
                .Setup(b => b.AddPart(It.IsAny<Part>()));

            var partService = new PartService(_partRepositoryMock.Object);

            var response = partService.AddPart(testObject);

            Assert.Null(response);
        }
    }
}
