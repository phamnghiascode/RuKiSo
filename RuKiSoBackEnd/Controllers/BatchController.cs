using Microsoft.AspNetCore.Mvc;

namespace RuKiSoBackEnd.Controllers
{
    public class BatchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
