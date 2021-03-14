using Microsoft.AspNetCore.Mvc;

namespace CalculationEngine.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        [Route("Home/Index/{id?}")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
