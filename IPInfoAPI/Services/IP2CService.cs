using System.Net.Http;
using System.Threading.Tasks;
using IPInfoAPI.Data;

namespace IPInfoAPI.Services
{
    public class IP2CService : IIP2CService
    {
        private readonly HttpClient _httpClient;
        private ApplicationDbContext context;

        public IP2CService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IP2CService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<(string CountryName, string TwoLetterCode, string ThreeLetterCode)> GetIPInfo(string ip)
        {
            var response = await _httpClient.GetAsync($"https://ip2c.org/{ip}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var parts = content.Split(';');
            return (parts[3], parts[1], parts[2]);
        }
    }
}
