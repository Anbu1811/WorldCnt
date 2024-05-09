using System.ComponentModel.DataAnnotations;

namespace WorldCountry.API.Model
{
    public class Country
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [MaxLength(5)]
        public string ShortName { get; set; }
        [MaxLength(5)]
        public string CountryCode { get; set; }

    }
}
