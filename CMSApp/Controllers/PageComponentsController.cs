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
    public class PageComponentsController : Controller
    {
        private readonly CMSDbContext _context;

        public PageComponentsController(CMSDbContext context)
        {
            _context = context;
        }

        // GET: PageComponents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Components.ToListAsync());
        }

        // GET: PageComponents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageComponent = await _context.Components
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageComponent == null)
            {
                return NotFound();
            }

            return View(pageComponent);
        }

        // GET: PageComponents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PageComponents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Html,PageId")] PageComponent pageComponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageComponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pageComponent);
        }

        // GET: PageComponents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageComponent = await _context.Components.FindAsync(id);
            if (pageComponent == null)
            {
                return NotFound();
            }
            return View(pageComponent);
        }

        // POST: PageComponents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Html,PageId")] PageComponent pageComponent)
        {
            if (id != pageComponent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageComponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageComponentExists(pageComponent.Id))
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
            return View(pageComponent);
        }

        // GET: PageComponents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageComponent = await _context.Components
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageComponent == null)
            {
                return NotFound();
            }

            return View(pageComponent);
        }

        // POST: PageComponents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pageComponent = await _context.Components.FindAsync(id);
            _context.Components.Remove(pageComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageComponentExists(int id)
        {
            return _context.Components.Any(e => e.Id == id);
        }
    }
}
