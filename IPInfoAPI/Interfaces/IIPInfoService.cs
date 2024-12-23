   namespace IPInfoAPI.Services
   {
       public interface IIPInfoService
       {
           Task<(string CountryName, string TwoLetterCode, string ThreeLetterCode)> GetIPInfo(string ip);
       }
   }
   