using BikeShop.BL.Services;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;
using Moq;

namespace BikeShop.Tests.PartServiceTests
{
    public class PartServiceTests_GetPartById
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly Mock<ILogger<PartService>> _logger;
        private readonly Part _testObject = new Part()
        {
            id = Guid.NewGuid().ToString(),
            partName = "test name 1",
            partSpec = "test spec 1",
        };

        public PartServiceTests_GetPartById()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
            _logger = new Mock<ILogger<PartService>>();
        }

        [Fact]
        public void GetBikeById_Ok()
        {
            var testName = "test name 1";
            var testPart = "test spec 1";

            _partRepositoryMock
                .Setup(b => b.GetPartById(_testObject.id))
                .Returns(_testObject);

            var partService = new PartService(_partRepositoryMock.Object, _logger.Object);

            var result = partService.GetPartById(_testObject.id);

            Assert.NotNull(result);
            Assert.Equal(testName, result.partName);
            Assert.Equal(testPart, result.partSpec);
        }

        [Fact]
        public void GetPartById_InvalidGuid()
        {
            var invalidTestGuid = "1234";
            var testName = "test name 1";
            var testPart = "test spec 1";

            _partRepositoryMock
                .Setup(b => b.GetPartById(invalidTestGuid));

            var partService = new PartService(_partRepositoryMock.Object, _logger.Object);

            var result = partService.GetPartById(_testObject.id);

            Assert.Null(result);
        }
    }
}