using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using World_Country.Web.DTO;

namespace World_Country.Web.Pages
{
    public class AllCountriesModel : PageModel
    {
        private readonly  IHttpClientFactory _httpClientFactory;

        public AllCountriesModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public List<AllCountryDTO> AllCountry { get; set; }

        
        


        public async Task OnGet()
        {
            var httpClient = _httpClientFactory.CreateClient("WorldWebAPI");
         

            AllCountry = await httpClient.GetFromJsonAsync<List<AllCountryDTO>>("api/Country");
            
		}
    }
}
