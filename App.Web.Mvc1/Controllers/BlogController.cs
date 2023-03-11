using App.Data;
using App.Web.Mvc1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Mvc1.Controllers
{
	public class BlogController : Controller
	{
		private readonly AppDbContext _context;

		public BlogController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> SearchAsync(string q)
		{
			var model = await _context.Categories.Where(k => k.Name.Contains(q)).ToListAsync();
			var post = new SearchModel()
			{
				search = q,
				Categorys = model,
			};
			return View(post);
		}


		public IActionResult Detail(int id)

		{
			var model = _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            return View(model);
		}
	}
}
