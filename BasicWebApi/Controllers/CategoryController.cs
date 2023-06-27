using Microsoft.AspNetCore.Mvc;

namespace BasicWebApi.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
