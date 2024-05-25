using AutoMapper;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.ClientSide.Order;
using KropkaNetApi.X_Entities.Enum;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface IOrderService
    {
        int Create(int? userId, CreateOrderDto dto);
        ReturnResult<OrderListDto> GetListUser(int userId, int page, string filter, string sortBy, SortDirection sortDireciton);
        ReturnResult<OrderListDto> GetList(int page, string filter, string sortBy, SortDirection sortDireciton);
        int Delete(int id);
    }
    public class OrderService : IOrderService
    {
        private StocktakingContext _context;
        private readonly IMapper _mapper;

        public OrderService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST: create order
        public int Create(int? userId, CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                order.Users = [user];
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        

        // DELETE : delete order with id
        public int Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(p => p.Id == id);
            if (order == null) return -1;

            _context.Remove(order);

            return 0;
        }
    }
}
