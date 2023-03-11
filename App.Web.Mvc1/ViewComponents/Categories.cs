using App.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Web.Mvc1.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly AppDbContext _context;

        public Categories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _context.Categories.ToListAsync();
            return View(model);
        }
    }
}
