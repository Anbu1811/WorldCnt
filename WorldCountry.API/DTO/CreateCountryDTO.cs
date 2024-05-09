using System.ComponentModel.DataAnnotations;

namespace WorldCountry.API.DTO
{
    public class CreateCountryDTO
    {


      
        
        public string Name { get; set; }
       
        public string ShortName { get; set; }
       
        public string CountryCode { get; set; }
    }
}
