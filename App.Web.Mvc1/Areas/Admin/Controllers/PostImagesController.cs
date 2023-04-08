using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Data.Entity;

namespace App.Web.Mvc1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostImagesController : Controller
    {
        private readonly AppDbContext _context;

        public PostImagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PostImages
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PostImages.Include(p => p.Post);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/PostImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PostImages == null)
            {
                return NotFound();
            }

            var postImage = await _context.PostImages
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postImage == null)
            {
                return NotFound();
            }

            return View(postImage);
        }

        // GET: Admin/PostImages/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content");
            return View();
        }

        // POST: Admin/PostImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostId,ImagePath")] PostImage postImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", postImage.PostId);
            return View(postImage);
        }

        // GET: Admin/PostImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PostImages == null)
            {
                return NotFound();
            }

            var postImage = await _context.PostImages.FindAsync(id);
            if (postImage == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", postImage.PostId);
            return View(postImage);
        }

        // POST: Admin/PostImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostId,ImagePath")] PostImage postImage)
        {
            if (id != postImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostImageExists(postImage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", postImage.PostId);
            return View(postImage);
        }

        // GET: Admin/PostImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PostImages == null)
            {
                return NotFound();
            }

            var postImage = await _context.PostImages
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postImage == null)
            {
                return NotFound();
            }

            return View(postImage);
        }

        // POST: Admin/PostImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PostImages == null)
            {
                return Problem("Entity set 'AppDbContext.PostImages'  is null.");
            }
            var postImage = await _context.PostImages.FindAsync(id);
            if (postImage != null)
            {
                _context.PostImages.Remove(postImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostImageExists(int id)
        {
          return (_context.PostImages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
