using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using World_Country.Web.DTO;

namespace World_Country.Web.Pages
{
    public class AllStatesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AllStatesModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public List<AllStatesDTO> AllStates { get; set; }

        public async Task OnGet()
        {
            var httpclient = _httpClientFactory.CreateClient("WorldWebAPI");

            AllStates = await httpclient.GetFromJsonAsync<List<AllStatesDTO>>("api/State");
        }
    }
}
