﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Models.DBObjects;

namespace ShopOnline.Controllers
{
    public class TVAController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TVAController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TVA
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Tvas.Include(t => t.IdCategoryNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TVA/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Tvas == null)
            {
                return NotFound();
            }

            var tva = await _context.Tvas
                .Include(t => t.IdCategoryNavigation)
                .FirstOrDefaultAsync(m => m.IdTva == id);
            if (tva == null)
            {
                return NotFound();
            }

            return View(tva);
        }

        // GET: TVA/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory");
            return View();
        }

        // POST: TVA/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTva,Tva1,IdCategory")] Tva tva)
        {
            if (ModelState.IsValid)
            {
                tva.IdTva = Guid.NewGuid();
                _context.Add(tva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", tva.IdCategory);
            return View(tva);
        }

        // GET: TVA/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Tvas == null)
            {
                return NotFound();
            }

            var tva = await _context.Tvas.FindAsync(id);
            if (tva == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", tva.IdCategory);
            return View(tva);
        }

        // POST: TVA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IdTva,Tva1,IdCategory")] Tva tva)
        {
            if (id != tva.IdTva)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TvaExists(tva.IdTva))
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
            ViewData["IdCategory"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", tva.IdCategory);
            return View(tva);
        }

        // GET: TVA/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Tvas == null)
            {
                return NotFound();
            }

            var tva = await _context.Tvas
                .Include(t => t.IdCategoryNavigation)
                .FirstOrDefaultAsync(m => m.IdTva == id);
            if (tva == null)
            {
                return NotFound();
            }

            return View(tva);
        }

        // POST: TVA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Tvas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tvas'  is null.");
            }
            var tva = await _context.Tvas.FindAsync(id);
            if (tva != null)
            {
                _context.Tvas.Remove(tva);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TvaExists(Guid id)
        {
          return (_context.Tvas?.Any(e => e.IdTva == id)).GetValueOrDefault();
        }
    }
}
