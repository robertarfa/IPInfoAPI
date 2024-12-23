using IPInfoAPI.Controllers;
using IPInfoAPI.Models;
using IPInfoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IPInfoAPI.Tests.Controllers
{
    public class IPInfoControllerTests
    {
        private readonly IPInfoController _controller;
        private readonly Mock<IPInfoService> _serviceMock;

        public IPInfoControllerTests()
        {
            _serviceMock = new Mock<IPInfoService>();
            _controller = new IPInfoController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetIPInfo_ValidIP_ReturnsOkResult()
        {
            // Arrange
            var ip = "192.168.1.1";
            var ipInfo = new Models.IPAddress
            {
                IP = ip,
                Country = new Country { Name = "Brazil" }
            };
            _serviceMock.Setup(x => x.GetIPInfo(ip))
                .ReturnsAsync((ipInfo.Country.Name, "BR", "BRA"));

            // Act
            var result = await _controller.GetIPInfo(ip);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Models.IPAddress>(okResult.Value);
            Assert.Equal(ip, returnValue.IP);
        }
    }

}
