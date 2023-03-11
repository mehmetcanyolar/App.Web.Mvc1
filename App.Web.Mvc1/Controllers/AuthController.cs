using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc1.Controllers
{
	public class AuthController : Controller
	{
		public IActionResult Kayitol()
		{
			return View();
		}

        public IActionResult Girisyap()
        {
            return View();
        }
    }
}
