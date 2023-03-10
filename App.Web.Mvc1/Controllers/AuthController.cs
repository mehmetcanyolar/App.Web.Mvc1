using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc1.Controllers
{
	public class AuthController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
