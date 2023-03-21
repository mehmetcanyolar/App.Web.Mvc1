using App.Data;
using App.Web.Mvc1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Mvc1.Controllers
{
	public class CategoryController : Controller
	{
		private readonly AppDbContext _context;

		public CategoryController(AppDbContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index(int id)
		{
			var model = await _context.Posts.Where(p=>p.CategoryId == id).ToListAsync();
			var ad = await _context.Categories.Where(k => k.Id == id).AsNoTracking().FirstOrDefaultAsync();
			var post = new CategoryPageModel()
			{
				Posts = model,
				Category = ad
			};
			return View(post);
		}

	}
}
