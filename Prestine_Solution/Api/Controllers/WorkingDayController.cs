using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class WorkingDayController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
