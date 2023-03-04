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
    public class PageTemplatesController : Controller
    {
        private readonly CMSDbContext _context;

        public PageTemplatesController(CMSDbContext context)
        {
            _context = context;
        }

        // GET: PageTemplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Templates.ToListAsync());
        }

        // GET: PageTemplates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageTemplate = await _context.Templates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageTemplate == null)
            {
                return NotFound();
            }

            return View(pageTemplate);
        }

        // GET: PageTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PageTemplates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Html")] PageTemplate pageTemplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pageTemplate);
        }

        // GET: PageTemplates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageTemplate = await _context.Templates.FindAsync(id);
            if (pageTemplate == null)
            {
                return NotFound();
            }
            return View(pageTemplate);
        }

        // POST: PageTemplates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Html")] PageTemplate pageTemplate)
        {
            if (id != pageTemplate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageTemplateExists(pageTemplate.Id))
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
            return View(pageTemplate);
        }

        // GET: PageTemplates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageTemplate = await _context.Templates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageTemplate == null)
            {
                return NotFound();
            }

            return View(pageTemplate);
        }

        // POST: PageTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pageTemplate = await _context.Templates.FindAsync(id);
            _context.Templates.Remove(pageTemplate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageTemplateExists(int id)
        {
            return _context.Templates.Any(e => e.Id == id);
        }
    }
}
