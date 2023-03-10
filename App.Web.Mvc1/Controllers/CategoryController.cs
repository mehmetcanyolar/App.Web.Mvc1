using Microsoft.AspNetCore.Mvc;

namespace App.Web.Mvc1.Controllers
{
	public class CategoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
