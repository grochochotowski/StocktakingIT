using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KropkaNet.Models.Objects.CompanySide;
using KropkaNet.Models.system;

namespace KropkaNet.Controllers
{
    public class WarehouseProductsController : Controller
    {
        private readonly StocktakingContext _context;

        public WarehouseProductsController(StocktakingContext context)
        {
            _context = context;
        }

        // GET: WarehouseProducts
        public async Task<IActionResult> Index()
        {
            var stocktakingContext = _context.WarehouseProduct.Include(w => w.Product).Include(w => w.Warehouse);
            return View(await stocktakingContext.ToListAsync());
        }

        // GET: WarehouseProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouseProduct = await _context.WarehouseProduct
                .Include(w => w.Product)
                .Include(w => w.Warehouse)
                .FirstOrDefaultAsync(m => m.WarehouseId == id);
            if (warehouseProduct == null)
            {
                return NotFound();
            }

            return View(warehouseProduct);
        }

        // GET: WarehouseProducts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Category");
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Id");
            return View();
        }

        // POST: WarehouseProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WarehouseId,ProductId,Quantity")] WarehouseProduct warehouseProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouseProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Category", warehouseProduct.ProductId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Id", warehouseProduct.WarehouseId);
            return View(warehouseProduct);
        }

        // GET: WarehouseProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouseProduct = await _context.WarehouseProduct.FindAsync(id);
            if (warehouseProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Category", warehouseProduct.ProductId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Id", warehouseProduct.WarehouseId);
            return View(warehouseProduct);
        }

        // POST: WarehouseProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WarehouseId,ProductId,Quantity")] WarehouseProduct warehouseProduct)
        {
            if (id != warehouseProduct.WarehouseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouseProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseProductExists(warehouseProduct.WarehouseId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Category", warehouseProduct.ProductId);
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Id", warehouseProduct.WarehouseId);
            return View(warehouseProduct);
        }

        // GET: WarehouseProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouseProduct = await _context.WarehouseProduct
                .Include(w => w.Product)
                .Include(w => w.Warehouse)
                .FirstOrDefaultAsync(m => m.WarehouseId == id);
            if (warehouseProduct == null)
            {
                return NotFound();
            }

            return View(warehouseProduct);
        }

        // POST: WarehouseProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warehouseProduct = await _context.WarehouseProduct.FindAsync(id);
            if (warehouseProduct != null)
            {
                _context.WarehouseProduct.Remove(warehouseProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseProductExists(int id)
        {
            return _context.WarehouseProduct.Any(e => e.WarehouseId == id);
        }
    }
}
