using IPInfoAPI.Data;
using IPInfoAPI.Models;
using IPInfoAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace IPInfoAPI.BackgroundServices
{
    public class IPUpdateService : BackgroundService
    {
        private readonly IServiceProvider _services;

        public IPUpdateService(IServiceProvider services)
        {
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var ip2cService = scope.ServiceProvider.GetRequiredService<IP2CService>();
                    var cacheService = scope.ServiceProvider.GetRequiredService<CacheService>();

                    var ips = await context.IPAddresses
                        .Include(i => i.Country)
                        .OrderBy(i => i.UpdatedAt)
                        .Take(100)
                        .ToListAsync(stoppingToken);

                    foreach (var ip in ips)
                    {
                        var info = await ip2cService.GetIPInfo(ip.IP);
                        if (info.TwoLetterCode != ip.Country.TwoLetterCode)
                        {
                            var country = await context.Countries.FirstOrDefaultAsync(c => c.TwoLetterCode == info.TwoLetterCode, stoppingToken);
                            if (country == null)
                            {
                                country = new Country
                                {
                                    Name = info.CountryName,
                                    TwoLetterCode = info.TwoLetterCode,
                                    ThreeLetterCode = info.ThreeLetterCode
                                };
                                context.Countries.Add(country);
                            }

                            ip.Country = country;
                            ip.UpdatedAt = DateTime.UtcNow;

                            cacheService.Remove(ip.IP);
                        }
                    }

                    await context.SaveChangesAsync(stoppingToken);
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                //await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);

            }
        }
    }
}
