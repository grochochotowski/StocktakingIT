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
using KropkaNet.Models.Dtos.ClientSide.Department;
using KropkaNet.Models.Objects.ClientSide;
using KropkaNet.Models.Dtos.CompanySide.Warehouse;
using KropkaNet.Models.Dtos.CompanySide.WarehouseProduct;

namespace KropkaNet.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly StocktakingContext _context;
        private readonly IMapper _mapper;

        public WarehousesController(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Warehouses
        public async Task<IActionResult> Index()
        {
            var warehouses = _context.Warehouses.Include(w => w.Stocktaking).ToList();
            var warehouseDtos = _mapper.Map<List<WarehouseDto>>(warehouses);

            return View(warehouseDtos);
        }

        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses
                .Include(w => w.Stocktaking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            var warehouseDto = _mapper.Map<WarehouseDto>(warehouse);

            return View(warehouseDto);
        }

        // GET: Warehouses/Edit/5
        [HttpGet]
        [Route("Warehouses/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Warehouses/Edit/{id}")]
        public async Task<IActionResult> EditPost(int id, [Bind("Id,Note")] UpdateWarehouseDto updateWarehouseDto)
        {
            if (id != updateWarehouseDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var warehouse = await _context.Warehouses.FindAsync(id);
                    if (warehouse == null)
                    {
                        return NotFound();
                    }

                    var existingStocktakingId = warehouse.StocktakingId;

                    warehouse.Note = updateWarehouseDto.Note;
                    warehouse.StocktakingId = existingStocktakingId;

                    _context.Update(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(id))
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
            return View(updateWarehouseDto);
        }

        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses
                .Include(w => w.Stocktaking)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse != null)
            {
                _context.Warehouses.Remove(warehouse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseExists(int id)
        {
            return _context.Warehouses.Any(e => e.Id == id);
        }
    }
}
