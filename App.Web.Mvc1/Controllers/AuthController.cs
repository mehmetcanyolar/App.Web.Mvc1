using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc1.Controllers
{
	public class AuthController : Controller
	{
	
        public IActionResult Girisyap()
        {
            return View();
        }

        public IActionResult Kayitol()
        {
            return View();
        }

    }
}
