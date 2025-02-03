using BikeShop.BL.Services;
using BikeShop.DL.Interfaces;
using BikeShop.DTO.DTO;
using Microsoft.Extensions.Logging;
using Moq;

namespace BikeShop.Tests.PartServiceTests
{
    public class PartServiceTests_GetAllParts
    {
        private readonly Mock<IPartRepository> _partRepositoryMock;
        private readonly Mock<ILogger<PartService>> _logger;

        private readonly List<Part> _bikeList = new List<Part>()
        {
            new Part()
            {
                id = Guid.NewGuid().ToString(),
                partName = "test name 1",
                partSpec = "test spec 1"
            },
            new Part()
            {
                id = Guid.NewGuid().ToString(),
                partName = "test name 2",
                partSpec = "test spec 2"
            },
            new Part()
            {
                id = Guid.NewGuid().ToString(),
                partName = "test name 3",
                partSpec = "test spec 3"
            }
        };

        public PartServiceTests_GetAllParts()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
            _logger = new Mock<ILogger<PartService>>();
        }

        [Fact]
        public void GetAllParts_OK()
        {
            var count = 3;
            var firstTestName = "test name 1";
            var secondTestName = "test name 2";
            var thirdTestName = "test name 3";

            _partRepositoryMock
                .Setup(p => p.GetAllParts())
                .Returns(_bikeList);

            var partService = new PartService(_partRepositoryMock.Object, _logger.Object);

            var result = partService.GetAllParts();

            Assert.NotNull(result);
            Assert.Equal(count, result.Count);
            Assert.Equal(firstTestName, result[0].partName);
            Assert.Equal(secondTestName, result[1].partName);
            Assert.Equal(thirdTestName, result[2].partName);
        }

        [Fact]
        public void GetAllParts_Empty()
        {
            _partRepositoryMock
                .Setup(b => b.GetAllParts())
                .Returns(new List<Part>());

            var partService = new PartService(_partRepositoryMock.Object, _logger.Object);

            var result = partService.GetAllParts();

            Assert.Empty(result);
        }
    }
}
