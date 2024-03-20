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
    public class PositionsController : Controller
    {
        private readonly StocktakingContext _context;

        public PositionsController(StocktakingContext context)
        {
            _context = context;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Positions.ToListAsync());
        }
    }
}
