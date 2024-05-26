using AutoMapper;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.CompanySide.Position;

namespace KropkaNetApi.Y_Services.CompanySide
{
    public interface IPositionService
    {
        IEnumerable<PositionDto> GetAll();
    }

    public class PositionService : IPositionService
    {
        private readonly StocktakingContext _context;
        private readonly IMapper _mapper;

        public PositionService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        // GET: Get all postions
        public IEnumerable<PositionDto> GetAll()
        {
            var positions = _context.Positions.ToList();
            var positionDtos = _mapper.Map<List<PositionDto>>(positions);

            return positionDtos;
        }

    }
}
