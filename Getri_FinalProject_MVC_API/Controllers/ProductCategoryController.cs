using Getri_FinalProject_MVC_API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Services.Repository;

namespace Getri_FinalProject_MVC_API.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            		List<CategoryWithIdViewModel> categoryLst = new List<CategoryWithIdViewModel>();
            HttpResponseMessage response = await _client.GetAsync("api/Category/GetCategoryList");
            if (response.IsSuccessStatusCode)
            {
				var result = response.Content.ReadAsStringAsync().Result;
				categoryLst = JsonConvert.DeserializeObject<List<CategoryWithIdViewModel>>(result);
			}

            ViewBag.CategoryList = new SelectList(categoryLst, "CategoryId", "CategoryName");

			ProductsViewModel objProductsViewModel = new ProductsViewModel();
			
			objProductsViewModel.ProductCreateViewModel = new ProductCreateViewModel(); ;
			objProductsViewModel.LstProductCreateViewModel = new List<ProductCreateViewModel>();

			return View(objProductsViewModel);
        }

        public async Task<IActionResult> AddProducts(ProductsViewModel model)
        {
			List<CategoryWithIdViewModel> categoryLst = new List<CategoryWithIdViewModel>();
			HttpResponseMessage response = await _client.GetAsync("api/Category/GetCategoryList");
			if (response.IsSuccessStatusCode)
            {
				var result = response.Content.ReadAsStringAsync().Result;
				categoryLst = JsonConvert.DeserializeObject<List<CategoryWithIdViewModel>>(result);
			}

            ViewBag.CategoryList = new SelectList(categoryLst, "CategoryId", "CategoryName");

            ProductsViewModel objProductsViewModel = new ProductsViewModel();
            ProductCreateViewModel objProductCreateViewModel = new ProductCreateViewModel();
            objProductCreateViewModel.CategoryId = model.ProductCreateViewModel.CategoryId;
			objProductCreateViewModel.ProductId = model.ProductCreateViewModel.ProductId;
            objProductCreateViewModel.ProductName = model.ProductCreateViewModel.ProductName;
            objProductCreateViewModel.ProductDescription = model.ProductCreateViewModel.ProductDescription;
            objProductCreateViewModel.ProductPrice = model.ProductCreateViewModel.ProductPrice;
            objProductCreateViewModel.CategoryName = categoryLst.FirstOrDefault(x => x.CategoryId == model.ProductCreateViewModel.CategoryId).CategoryName;

            List<ProductCreateViewModel> lstCreateViewModel = new List<ProductCreateViewModel>();
            if(HttpContext.Session.GetString("lstData") != null)
            {
				var lstPrevious = JsonConvert.DeserializeObject<List<ProductCreateViewModel>>(HttpContext.Session.GetString("lstData"));

                if(lstPrevious != null)
                {
                    lstCreateViewModel = lstPrevious;
                }
			}

            lstCreateViewModel.Add(objProductCreateViewModel);
            HttpContext.Session.SetString("lstData", JsonConvert.SerializeObject(lstCreateViewModel));

            objProductsViewModel.ProductCreateViewModel = new ProductCreateViewModel();
            objProductsViewModel.LstProductCreateViewModel = lstCreateViewModel;

            return View("CreateProduct", objProductsViewModel);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitProducts()
        {
            if(HttpContext.Session.GetString("lstData") != null)
            {
                var lstPrevious = JsonConvert.DeserializeObject<List<ProductCreateViewModel>>(HttpContext.Session.GetString("lstData"));

                if (lstPrevious != null)
                {
                    foreach (var product in lstPrevious)
                    {
                        ProductInsertViewModel objProductViewModel = new ProductInsertViewModel();
                        objProductViewModel.ProductName = product.ProductName;
                        objProductViewModel.ProductDescription = product.ProductDescription;
                        objProductViewModel.ProductPrice = product.ProductPrice;
                        objProductViewModel.CategoryId = product.CategoryId;

                        var response = _client.PostAsJsonAsync("api/Product/InsertProduct", objProductViewModel).Result;

                        if (response.IsSuccessStatusCode)
                        {
                           // return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Error while inserting product");
                        }
                    }
                }
            }

            HttpContext.Session.Remove("lstData");

            ProductsViewModel objProductsViewModel = new ProductsViewModel();
            objProductsViewModel.ProductCreateViewModel = new ProductCreateViewModel(); ;
            objProductsViewModel.LstProductCreateViewModel = new List<ProductCreateViewModel>();

            return RedirectToAction("ProductList");
        }

        public async Task<IActionResult> ProductsList()
        {
            List<ProductCreateViewModel> productList = new List<ProductCreateViewModel>();
            HttpResponseMessage response = await _client.GetAsync("api/Product/GetProductList");
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<ProductCreateViewModel>>(result);
            }

            List<CategoryWithIdViewModel> categoryList = new List<CategoryWithIdViewModel>();
            HttpResponseMessage responseCategory = await _client.GetAsync("api/Category/GetCategoryList");
            if (responseCategory.IsSuccessStatusCode)
            {
                var result = responseCategory.Content.ReadAsStringAsync().Result;
                categoryList = JsonConvert.DeserializeObject<List<CategoryWithIdViewModel>>(result);
            }

            foreach (var product in productList)
            {
                product.CategoryName = categoryList.FirstOrDefault(x => x.CategoryId == product.CategoryId).CategoryName;
            }

            return View(productList);
        }
    }
}
