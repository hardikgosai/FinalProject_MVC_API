using Microsoft.AspNetCore.Mvc;

namespace Getri_FinalProject_MVC_API.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
