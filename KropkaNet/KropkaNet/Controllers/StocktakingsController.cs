using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KropkaNet.Models.Objects.CompanySide;
using KropkaNet.Models.system;
using AutoMapper;
using KropkaNet.Models.Dtos.ClientSide.User;
using KropkaNet.Models.Dtos.CompanySide.Stocktaking;
using KropkaNet.Models.Dtos.CompanySide.Warehouse;

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
            var stocktakings = _context.Stocktakings.Include(s => s.Warehouse);
            var stocktakingDtos = _mapper.Map<List<StocktakingDto>>(stocktakings);

            return View(stocktakingDtos);
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

            var stocktakingDto = _mapper.Map<StocktakingDto>(stocktaking);

            return View(stocktakingDto);
        }

        // GET: Stocktakings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stocktakings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpectedTimeHours,Note,WarehouseId,OrderId")] CreateStocktakingDto createStocktakingDto)
        {
            if (ModelState.IsValid)
            {
                var warehouse = new Warehouse();
                _context.Add(warehouse);
                await _context.SaveChangesAsync();

                var stocktaking = _mapper.Map<Stocktaking>(createStocktakingDto);
                stocktaking.WarehouseId = warehouse.Id;
                _context.Add(stocktaking);
                await _context.SaveChangesAsync();

                var order = await _context.Orders.FindAsync(createStocktakingDto.OrderId);
                if (order != null)
                {
                    order.StocktakingId = stocktaking.Id;
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }

                warehouse = await _context.Warehouses.FindAsync(warehouse.Id);
                if (warehouse != null)
                {
                    warehouse.StocktakingId = stocktaking.Id;
                    _context.Update(warehouse);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "Id", "Id", createStocktakingDto.WarehouseId);
            return View(createStocktakingDto);
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
