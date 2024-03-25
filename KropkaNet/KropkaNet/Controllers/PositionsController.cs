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
using KropkaNet.Models.Dtos.CompanySide.Position;

namespace KropkaNet.Controllers
{
    public class PositionsController : Controller
    {
        private readonly StocktakingContext _context;
        private readonly IMapper _mapper;

        public PositionsController(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Positions
        public async Task<IActionResult> Index()
        {
            var positions = _context.Positions.ToList();
            var postionsDtos = _mapper.Map<List<PositionDto>>(positions);
            return View(postionsDtos);
        }
    }
}
