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

        public ProductCategoryController(IConfiguration _configuration)
        {
            configuration = _configuration;
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

        public async Task<IActionResult> CategoryDetails(int id)
        {
			CategoryWithIdViewModel model = new CategoryWithIdViewModel();
			HttpResponseMessage response = await _client.GetAsync("api/Category/GetCategoryById?id=" + id);
			if (response.IsSuccessStatusCode)
            {
				var result = response.Content.ReadAsStringAsync().Result;
				model = JsonConvert.DeserializeObject<CategoryWithIdViewModel>(result);
			}
			return View(model);
		}

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {             
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/Category/InsertCategory", model);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error while creating record");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            CategoryWithIdViewModel model = new CategoryWithIdViewModel();
            HttpResponseMessage response = await _client.GetAsync("api/Category/GetCategoryById?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<CategoryWithIdViewModel>(result);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(CategoryWithIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _client.PutAsJsonAsync("api/Category/EditCategory", model);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error while updating record");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            CategoryWithIdViewModel model = new CategoryWithIdViewModel();
            HttpResponseMessage response = await _client.GetAsync("api/Category/GetCategoryById?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<CategoryWithIdViewModel>(result);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(CategoryWithIdViewModel model)
        {
            HttpResponseMessage response = await _client.DeleteAsync("api/Category/RemoveCategory?id=" + model.CategoryId);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error while deleting record");
            }

            return View(model);
        }
    }
}
