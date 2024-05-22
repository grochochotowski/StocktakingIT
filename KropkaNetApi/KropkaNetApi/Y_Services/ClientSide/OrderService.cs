using AutoMapper;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.ClientSide.Order;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface IOrderService
    {
        int Create(CreateOrderDto dto);
        IEnumerable<OrderListDto> GetList(string filter);
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
        public int Create(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);

            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.Id;
        }

        // GET: get list of orders
        public IEnumerable<OrderListDto> GetList(string filter)
        {
            var orderList = _context.Orders
                .Where(
                    p => filter == null || (
                    p.DateOfOrderExecution.ToString().Contains(filter) ||
                    p.Id.ToString().Contains(filter)
                ))
                .OrderBy(p => p.DateOfOrderExecution)
                .Select(p => new OrderListDto
                {
                    Id = p.Id,
                    DateOfOrderExecution = p.DateOfOrderExecution
                })
                .ToList();

            return orderList;
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
