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
    public class PostCommentsController : Controller
    {
        private readonly AppDbContext _context;

        public PostCommentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PostComments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.PostComments.Include(p => p.Post);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/PostComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PostComments == null)
            {
                return NotFound();
            }

            var postComment = await _context.PostComments
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postComment == null)
            {
                return NotFound();
            }

            return View(postComment);
        }

        // GET: Admin/PostComments/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content");
            return View();
        }

        // POST: Admin/PostComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostId,Comment,IsActive")] PostComment postComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", postComment.PostId);
            return View(postComment);
        }

        // GET: Admin/PostComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PostComments == null)
            {
                return NotFound();
            }

            var postComment = await _context.PostComments.FindAsync(id);
            if (postComment == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", postComment.PostId);
            return View(postComment);
        }

        // POST: Admin/PostComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostId,Comment,IsActive")] PostComment postComment)
        {
            if (id != postComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCommentExists(postComment.Id))
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
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content", postComment.PostId);
            return View(postComment);
        }

        // GET: Admin/PostComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PostComments == null)
            {
                return NotFound();
            }

            var postComment = await _context.PostComments
                .Include(p => p.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postComment == null)
            {
                return NotFound();
            }

            return View(postComment);
        }

        // POST: Admin/PostComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PostComments == null)
            {
                return Problem("Entity set 'AppDbContext.PostComments'  is null.");
            }
            var postComment = await _context.PostComments.FindAsync(id);
            if (postComment != null)
            {
                _context.PostComments.Remove(postComment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostCommentExists(int id)
        {
          return (_context.PostComments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
