using System.Diagnostics.Metrics;

namespace IPInfoAPI.Models
{
    public class IPAddress
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public required string IP { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public required Country Country { get; set; }
    }
}
