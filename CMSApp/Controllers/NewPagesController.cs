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
    public class NewPagesController : Controller
    {
        private readonly CMSDbContext _context;

        public NewPagesController(CMSDbContext context)
        {
            _context = context;
        }

        // GET: NewPages
        public async Task<IActionResult> Index()
        {
            var cMSDbContext = _context.Pages.Include(n => n.Template);
            return View(await cMSDbContext.ToListAsync());
        }

        // GET: NewPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newPage = await _context.Pages
                .Include(n => n.Template)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newPage == null)
            {
                return NotFound();
            }

            return View(newPage);
        }

        // GET: NewPages/Create
        public IActionResult Create()
        {
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Html");
            return View();
        }

        // POST: NewPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Slug,Content,CreatedAt,UpdatedAt,TemplateId")] NewPage newPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Html", newPage.TemplateId);
            return View(newPage);
        }

        // GET: NewPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newPage = await _context.Pages.FindAsync(id);
            if (newPage == null)
            {
                return NotFound();
            }
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Html", newPage.TemplateId);
            return View(newPage);
        }

        // POST: NewPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Slug,Content,CreatedAt,UpdatedAt,TemplateId")] NewPage newPage)
        {
            if (id != newPage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewPageExists(newPage.Id))
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
            ViewData["TemplateId"] = new SelectList(_context.Templates, "Id", "Html", newPage.TemplateId);
            return View(newPage);
        }

        // GET: NewPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newPage = await _context.Pages
                .Include(n => n.Template)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newPage == null)
            {
                return NotFound();
            }

            return View(newPage);
        }

        // POST: NewPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newPage = await _context.Pages.FindAsync(id);
            _context.Pages.Remove(newPage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewPageExists(int id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
    }
}
