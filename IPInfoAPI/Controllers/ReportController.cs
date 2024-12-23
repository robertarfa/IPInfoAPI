using IPInfoAPI.Data;
using IPInfoAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPInfoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport([FromQuery] string[] countryCodes = null)
        {
            IQueryable<CountryReportDTO> query = _context.Countries
                .GroupJoin(_context.IPAddresses,
                    c => c.Id,
                    i => i.CountryId,
                    (c, ips) => new CountryReportDTO
                    {
                        CountryName = c.Name,
                        AddressesCount = ips.Count(),
                        LastAddressUpdated = ips.Max(i => i.UpdatedAt),
                        TwoLetterCode = c.TwoLetterCode
                    });

            if (countryCodes != null && countryCodes.Length > 0)
            {
                query = query.Where(c => countryCodes.Contains(c.TwoLetterCode));
            }

            var result = await query.Select(c => new
            {
                c.CountryName,
                c.AddressesCount,
                c.LastAddressUpdated,
            }).ToListAsync();

            return Ok(result);
        }
    }
}
