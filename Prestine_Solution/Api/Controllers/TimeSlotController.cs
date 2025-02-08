using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class TimeSlotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
