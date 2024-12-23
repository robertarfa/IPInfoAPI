namespace IPInfoAPI.DTOs
{
    public class CountryReportDTO
    {
        public required string CountryName { get; set; }
        public int AddressesCount { get; set; }
        public DateTime LastAddressUpdated { get; set; }
        public string TwoLetterCode { get; set; }
    }
}
