using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KropkaNet.Objects.Entities;
using KropkaNet.Objects.Entities.Models.CompanySide;
using AutoMapper;
using KropkaNet.Objects.Dtos.CompanySide.Stocktaking;

namespace KropkaNet.Controllers
{
    public class StocktakingsController : Controller
    {
        private readonly StocktakingContext _context;
        private readonly IMapper _mapper;

        public StocktakingsController(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Stocktakings
        public async Task<IActionResult> Index()
        {
            var stocktakingContext = _context.Stocktakings.Include(s => s.Order);
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
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stocktaking == null)
            {
                return NotFound();
            }

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
            return View(stocktaking);
        }

        // POST: Stocktakings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpectedTimeHours,Note,WarehouseId,OrderId")] UpdateStocktakingDto updateStocktakingDto)
        {
            var stocktaking = _context.Stocktakings.FirstOrDefault(s => s.Id == id);
            stocktaking.Note = updateStocktakingDto.Note;
            stocktaking.ExpectedTimeHours = updateStocktakingDto.ExpectedTimeHours;

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", stocktaking.OrderId);
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
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stocktaking == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses
                .FirstOrDefaultAsync(w => w.Id == stocktaking.WarehouseId);
            if (warehouse == null)
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
            var warehouse = await _context.Warehouses.FindAsync(stocktaking.WarehouseId);

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == stocktaking.OrderId);

            if (stocktaking != null)
            {
                order.StocktakingId = null;
                _context.Warehouses.Remove(warehouse);
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
