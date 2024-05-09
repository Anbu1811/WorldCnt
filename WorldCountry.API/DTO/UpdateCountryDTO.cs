using System.ComponentModel.DataAnnotations;

namespace WorldCountry.API.DTO
{
    public class UpdateCountryDTO
    {

       
        public int Id { get; set; }
       
        public string Name { get; set; }
       
        public string ShortName { get; set; }
       
        public string CountryCode { get; set; }
    }
}
