using IPInfoAPI.Data;
using IPInfoAPI.Models;
using IPInfoAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace IPInfoAPI.Tests.Services
{
    public class IPInfoServiceTests
    {
        private readonly Mock<ApplicationDbContext> _dbContextMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly Mock<IP2CService> _ip2cServiceMock;
        private readonly IPInfoService _ipInfoService;

        public IPInfoServiceTests()
        {
            _dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _cacheServiceMock = new Mock<ICacheService>();
            _ip2cServiceMock = new Mock<IP2CService>();
            _ipInfoService = new IPInfoService(_dbContextMock.Object, _cacheServiceMock.Object, _ip2cServiceMock.Object);
        }

        [Fact]
        public async Task GetIPInfo_ValidIP_ReturnsCountryInfo()
        {
            // Arrange
            var ip = "192.168.1.1";
            var countryName = "Brazil";
            var twoLetterCode = "BR";
            var threeLetterCode = "BRA";

            _ip2cServiceMock.Setup(x => x.GetIPInfo(ip))
                .ReturnsAsync((countryName, twoLetterCode, threeLetterCode));

            // Act
            var result = await _ipInfoService.GetIPInfo(ip);

            // Assert
            Assert.Equal(countryName, result.CountryName);
            Assert.Equal(twoLetterCode, result.TwoLetterCode);
            Assert.Equal(threeLetterCode, result.ThreeLetterCode);
        }

        [Fact]
        public async Task GetIPInfo_InvalidIP_ReturnsNull()
        {
            // Arrange
            var ip = "invalid_ip";

            _ip2cServiceMock.Setup(x => x.GetIPInfo(ip))
                .ReturnsAsync((null, null, null));

            // Act
            var result = await _ipInfoService.GetIPInfo(ip);

            // Assert
            Assert.Null(result.CountryName);
            Assert.Null(result.TwoLetterCode);
            Assert.Null(result.ThreeLetterCode);
        }
    }
}
