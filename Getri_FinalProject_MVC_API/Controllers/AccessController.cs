using Getri_FinalProject_MVC_API.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Getri_FinalProject_MVC_API.Controllers
{
    public class AccessController : Controller
    {
        HttpClient _Client;
        IConfiguration configuration;

        public AccessController(IConfiguration _configuration)
        {            
            configuration = _configuration;
            string apiAddress = configuration["ApiAddress"];
            Uri baseAddress = new Uri(apiAddress);
            _Client = new HttpClient
            {
                BaseAddress = baseAddress
            };
        }
                
        public IActionResult Login()
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            if(claimsPrincipal.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(VMLoginViewModel modelLogin)
        {
            if (ModelState.IsValid)
            {
                VMLoginViewModel model = new VMLoginViewModel();
             
                string url = "api/UserLogin/GetUserLogin?email=" + modelLogin.Email + "&password=" + modelLogin.Password;
                using (var response = await _Client.GetAsync(url))
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<VMLoginViewModel>(result);
                } 
                
                if(modelLogin.Email == model.Email && modelLogin.Password == model.Password)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, model.Email),
                        new Claim("Other Properties", "Example Role")
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = modelLogin.KeepLoggedIn
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Home");
                }

                ViewData["ValidationMessage"] = "user not found";
                return View();
            }
            else
            {
                return View();
            }
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
