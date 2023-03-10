using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc1.Controllers
{
	public class PageController : Controller
	{
		public IActionResult Detail()
		{
			return View();
		}
	}
}
