using IPInfoAPI.Data;
using IPInfoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

namespace IPInfoAPI.Services
{
    public class IPInfoService : IIPInfoService
    {
        private ApplicationDbContext _context;
        private ApplicationDbContext object1;
        private ICacheService object2;
        private IIP2CService object3;
        private readonly CacheService _cacheService;
        private readonly IP2CService _ip2cService;
        //private ApplicationDbContext context;

        //public IPInfoService(ApplicationDbContext context)
        //{
        //    this.context = context;
        //}

        public IPInfoService(ApplicationDbContext context, CacheService cacheService, IP2CService ip2cService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _ip2cService = ip2cService ?? throw new ArgumentNullException(nameof(ip2cService));
        }

        public IPInfoService(ApplicationDbContext object1, ICacheService object2, IIP2CService object3)
        {
            this.object1 = object1;
            this.object2 = object2;
            this.object3 = object3;
        }

        public async Task<(string CountryName, string TwoLetterCode, string ThreeLetterCode)> GetIPInfo(string ip)
        {
            // Tentar obter do cache
            var cachedInfo = _cacheService.Get<(string, string, string)>(ip);
            if (cachedInfo != default)
                return cachedInfo;

            // Tentar obter do banco de dados
            var dbInfo = await _context.IPAddresses
                .Include(i => i.Country)
                .FirstOrDefaultAsync(i => i.IP == ip);

            if (dbInfo != null)
            {
                var result = (dbInfo.Country.Name, dbInfo.Country.TwoLetterCode, dbInfo.Country.ThreeLetterCode);
                _cacheService.Set(ip, result, TimeSpan.FromHours(1));
                return result;
            }

            // Obter da API IP2C
            var apiInfo = await _ip2cService.GetIPInfo(ip);

            // Salvar no banco de dados
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.TwoLetterCode == apiInfo.TwoLetterCode);
            if (country == null)
            {
                country = new Country
                {
                    Name = apiInfo.CountryName,
                    TwoLetterCode = apiInfo.TwoLetterCode,
                    ThreeLetterCode = apiInfo.ThreeLetterCode
                };
                _context.Countries.Add(country);
            }

            var ipAddress = new IPAddress
            {
                IP = ip,
                Country = country
            };
            _context.IPAddresses.Add(ipAddress);
            await _context.SaveChangesAsync();

            // Salvar no cache
            _cacheService.Set(ip, apiInfo, TimeSpan.FromHours(1));

            return apiInfo;
        }
    }
}
