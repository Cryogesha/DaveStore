using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DaveStore.Data;
using DaveStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace DaveStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductSpecsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductSpecsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductSpecs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductSpecs.Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/ProductSpecs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductSpecs == null)
            {
                return NotFound();
            }

            var productSpec = await _context.ProductSpecs
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductSpecId == id);
            if (productSpec == null)
            {
                return NotFound();
            }

            return View(productSpec);
        }

        // GET: Admin/ProductSpecs/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            return View();
        }

        // POST: Admin/ProductSpecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductSpecId,Name,Value,ProductId")] ProductSpec productSpec)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productSpec);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productSpec.ProductId);
            return View(productSpec);
        }

        // GET: Admin/ProductSpecs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductSpecs == null)
            {
                return NotFound();
            }

            var productSpec = await _context.ProductSpecs.FindAsync(id);
            if (productSpec == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productSpec.ProductId);
            return View(productSpec);
        }

        // POST: Admin/ProductSpecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductSpecId,Name,Value,ProductId")] ProductSpec productSpec)
        {
            if (id != productSpec.ProductSpecId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSpec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSpecExists(productSpec.ProductSpecId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productSpec.ProductId);
            return View(productSpec);
        }

        // GET: Admin/ProductSpecs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductSpecs == null)
            {
                return NotFound();
            }

            var productSpec = await _context.ProductSpecs
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductSpecId == id);
            if (productSpec == null)
            {
                return NotFound();
            }

            return View(productSpec);
        }

        // POST: Admin/ProductSpecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductSpecs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductSpecs'  is null.");
            }
            var productSpec = await _context.ProductSpecs.FindAsync(id);
            if (productSpec != null)
            {
                _context.ProductSpecs.Remove(productSpec);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSpecExists(int id)
        {
          return _context.ProductSpecs.Any(e => e.ProductSpecId == id);
        }
    }
}
