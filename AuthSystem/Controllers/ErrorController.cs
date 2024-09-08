using Microsoft.AspNetCore.Mvc;

namespace AuthSystem.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        public IActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }
    }

}
