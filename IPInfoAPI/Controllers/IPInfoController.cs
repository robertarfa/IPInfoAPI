using IPInfoAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IPInfoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IPInfoController : ControllerBase
    {
        private readonly IPInfoService _ipInfoService;

        public IPInfoController(IPInfoService ipInfoService)
        {
            _ipInfoService = ipInfoService;
        }

        [HttpGet("{ip}")]
        public async Task<IActionResult> GetIPInfo(string ip)
        {
            var info = await _ipInfoService.GetIPInfo(ip);
            return Ok(new
            {
                info.CountryName,
                info.TwoLetterCode,
                info.ThreeLetterCode
            });
        }
    }
}
