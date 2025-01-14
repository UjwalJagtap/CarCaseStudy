using Microsoft.AspNetCore.Mvc;

namespace MVCCoreApp.Controllers
{
   
    public class JourneyController : Controller
    {
       
        public IActionResult Welcome()
        {
            return View();
        }
    }
}