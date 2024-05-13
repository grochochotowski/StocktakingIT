using AutoMapper;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Models.CompanySide.Stocktaking;
using System.Collections.Generic;
using System.Linq;

namespace KropkaNetApi.Y_Services.CompanySide
{
        public interface IStocktakingService
        {
            int Create(CreateStocktakingDto dto);
            IEnumerable<StocktakingListDto> GetAll();
            bool Delete(int id);
        }
    

    public class StocktakingService : IStocktakingService
    {
        private readonly StocktakingContext _context;
        private readonly IMapper _mapper;

        public StocktakingService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(CreateStocktakingDto dto)
        {
            var stocktaking = _mapper.Map<Stocktaking>(dto);
            _context.Stocktakings.Add(stocktaking);
            _context.SaveChanges();
            return stocktaking.Id;
        }

        public IEnumerable<StocktakingListDto> GetAll()
        {
            var stocktakings = _context.Stocktakings.ToList();
            var stocktakingDtos = _mapper.Map<List<StocktakingListDto>>(stocktakings);
            return stocktakingDtos;
        }

        public bool Delete(int id)
        {
            var stocktaking = _context.Stocktakings.Find(id);
            if (stocktaking == null)
                return false;

            _context.Stocktakings.Remove(stocktaking);
            _context.SaveChanges();
            return true;
        }
    }
}
