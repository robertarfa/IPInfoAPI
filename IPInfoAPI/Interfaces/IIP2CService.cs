   namespace IPInfoAPI.Services
   {
       public interface IIP2CService
       {
           Task<(string CountryName, string TwoLetterCode, string ThreeLetterCode)> GetIPInfo(string ip);
       }
   }
   