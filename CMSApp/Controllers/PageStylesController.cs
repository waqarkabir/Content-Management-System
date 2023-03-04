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
    public class PageStylesController : Controller
    {
        private readonly CMSDbContext _context;

        public PageStylesController(CMSDbContext context)
        {
            _context = context;
        }

        // GET: PageStyles
        public async Task<IActionResult> Index()
        {
            var cMSDbContext = _context.Styles.Include(p => p.Page);
            return View(await cMSDbContext.ToListAsync());
        }

        // GET: PageStyles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageStyle = await _context.Styles
                .Include(p => p.Page)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageStyle == null)
            {
                return NotFound();
            }

            return View(pageStyle);
        }

        // GET: PageStyles/Create
        public IActionResult Create()
        {
            ViewData["PageId"] = new SelectList(_context.Pages, "Id", "Content");
            return View();
        }

        // POST: PageStyles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Selector,Css,PageId")] PageStyle pageStyle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pageStyle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PageId"] = new SelectList(_context.Pages, "Id", "Content", pageStyle.PageId);
            return View(pageStyle);
        }

        // GET: PageStyles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageStyle = await _context.Styles.FindAsync(id);
            if (pageStyle == null)
            {
                return NotFound();
            }
            ViewData["PageId"] = new SelectList(_context.Pages, "Id", "Content", pageStyle.PageId);
            return View(pageStyle);
        }

        // POST: PageStyles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Selector,Css,PageId")] PageStyle pageStyle)
        {
            if (id != pageStyle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pageStyle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageStyleExists(pageStyle.Id))
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
            ViewData["PageId"] = new SelectList(_context.Pages, "Id", "Content", pageStyle.PageId);
            return View(pageStyle);
        }

        // GET: PageStyles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pageStyle = await _context.Styles
                .Include(p => p.Page)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pageStyle == null)
            {
                return NotFound();
            }

            return View(pageStyle);
        }

        // POST: PageStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pageStyle = await _context.Styles.FindAsync(id);
            _context.Styles.Remove(pageStyle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PageStyleExists(int id)
        {
            return _context.Styles.Any(e => e.Id == id);
        }
    }
}
