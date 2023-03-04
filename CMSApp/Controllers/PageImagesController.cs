using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbModels;
using DbModels.Models;

namespace CMSApp.Controllers
{
    public class PageImagesController : Controller
    {
        private readonly CMSDbContext _context;

        public PageImagesController(CMSDbContext context)
        {
            _context = context;
        }

        // GET: PageImages
        public async Task<IActionResult> Index()
        {
            var cMSDbContext = _context.Images.Include(p => p.Page);
            return View(await cMSDbContext.ToListAsync());
        }

        // GET: PageImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageImage = await _context.Images
                .Include(p => p.Page)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageImage == null)
            {
                return NotFound();
            }

            return View(pageImage);
        }

        // GET: PageImages/Create
        public IActionResult Create()
        {
            ViewData["PageId"] = new SelectList(_context.Pages, "Id", "Content");
            return View();
        }

        // POST: PageImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FileName,ContentType,Data,PageId")] PageImage pageImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PageId"] = new SelectList(_context.Pages, "Id", "Content", pageImage.PageId);
            return View(pageImage);
        }

        // GET: PageImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageImage = await _context.Images.FindAsync(id);
            if (pageImage == null)
            {
                return NotFound();
            }
            ViewData["PageId"] = new SelectList(_context.Pages, "Id", "Content", pageImage.PageId);
            return View(pageImage);
        }

        // POST: PageImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FileName,ContentType,Data,PageId")] PageImage pageImage)
        {
            if (id != pageImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageImageExists(pageImage.Id))
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
            ViewData["PageId"] = new SelectList(_context.Pages, "Id", "Content", pageImage.PageId);
            return View(pageImage);
        }

        // GET: PageImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageImage = await _context.Images
                .Include(p => p.Page)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageImage == null)
            {
                return NotFound();
            }

            return View(pageImage);
        }

        // POST: PageImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pageImage = await _context.Images.FindAsync(id);
            _context.Images.Remove(pageImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageImageExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
