using Getri_FinalProject_MVC_API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Repository;

namespace Getri_FinalProject_MVC_API.Controllers
{
    public class ProductCategoryController : Controller
    {
        HttpClient _client;
        IConfiguration configuration;
        private readonly ICategoryRepository categoryRepository;

        public ProductCategoryController(IConfiguration _configuration, ICategoryRepository _categoryRepository)
        {
            configuration = _configuration;
            categoryRepository = _categoryRepository;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            _client = new HttpClient { BaseAddress = baseAddress };
        }

        public async Task<IActionResult> Index()
        {
            List<CategoryWithIdViewModel> model = new List<CategoryWithIdViewModel>();
            HttpResponseMessage response = await _client.GetAsync("api/Category/GetCategoryList");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<CategoryWithIdViewModel>>(result);
            }
            return View(model);
        }
    }
}
