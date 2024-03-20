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
    public class StocktakingsController : Controller
    {
        private readonly StocktakingContext _context;

        public StocktakingsController(StocktakingContext context)
        {
            _context = context;
        }

        // GET: Stocktakings
        public async Task<IActionResult> Index()
        {
            var stocktakingContext = _context.Stocktakings.Include(s => s.Warehouse);
            return View(await stocktakingContext.ToListAsync());
        }

        // GET: Stocktakings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocktaking = await _context.Stocktakings
                .Include(s => s.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stocktaking == null)
            {
                return NotFound();
            }

            return View(stocktaking);
        }

        // GET: Stocktakings/Create
        public IActionResult Create()
        {
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Id");
            return View();
        }

        // POST: Stocktakings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpectedTimeHours,Note,WarehouseId,OrderId")] Stocktaking stocktaking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stocktaking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Id", stocktaking.WarehouseId);
            return View(stocktaking);
        }

        // GET: Stocktakings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocktaking = await _context.Stocktakings.FindAsync(id);
            if (stocktaking == null)
            {
                return NotFound();
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Id", stocktaking.WarehouseId);
            return View(stocktaking);
        }

        // POST: Stocktakings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpectedTimeHours,Note,WarehouseId,OrderId")] Stocktaking stocktaking)
        {
            if (id != stocktaking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stocktaking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StocktakingExists(stocktaking.Id))
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
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Id", stocktaking.WarehouseId);
            return View(stocktaking);
        }

        // GET: Stocktakings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocktaking = await _context.Stocktakings
                .Include(s => s.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stocktaking == null)
            {
                return NotFound();
            }

            return View(stocktaking);
        }

        // POST: Stocktakings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stocktaking = await _context.Stocktakings.FindAsync(id);
            if (stocktaking != null)
            {
                _context.Stocktakings.Remove(stocktaking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StocktakingExists(int id)
        {
            return _context.Stocktakings.Any(e => e.Id == id);
        }
    }
}
