using BikeShop.BL.Services;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;
using Moq;

namespace BikeShop.Tests.PartServiceTests
{
    public class PartServiceTests_DeletePartById
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly Mock<ILogger<PartService>> _logger;

        public PartServiceTests_DeletePartById()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
            _logger = new Mock<ILogger<PartService>>();
        }

        [Fact]
        public void DeletePartById_OK()
        {
            var testObject = new Part()
            {
                id = Guid.NewGuid().ToString(),
                partName = "test name 1",
                partSpec = "test spec 1",
            };

            _partRepositoryMock
                .Setup(p => p.DeletePartById(testObject.id));

            _partRepositoryMock
                .Setup(p => p.GetPartById(testObject.id));

            var partService = new PartService(_partRepositoryMock.Object, _logger.Object);

            partService.DeletePartById(testObject.id);

            var result = partService.GetPartById(testObject.id);

            Assert.Null(result);
        }

        [Fact]
        public void DeletePartById_InvalidId()
        {
            var testObject = new Part()
            {
                id = Guid.NewGuid().ToString(),
                partName = "test name 1",
                partSpec = "test spec 1",
            };

            _partRepositoryMock
                .Setup(p => p.DeletePartById("1234"));

            _partRepositoryMock
                .Setup(p => p.GetPartById("1234"));

            var partService = new PartService(_partRepositoryMock.Object, _logger.Object);

            partService.DeletePartById(testObject.id);

            var result = partService.GetPartById(testObject.id);

            Assert.Null(result);
        }
    }
}