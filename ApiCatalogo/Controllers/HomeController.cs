using Microsoft.AspNetCore.Mvc;

namespace ApiCatalogo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
