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

        // GET: get list of orders of user
        public ReturnResult<OrderListDto> GetListUser(int userId, int page, string filter, string sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.Orders
                .Include(c => c.Department)
                .Include(c => c.Users)
                .Where(c => (string.IsNullOrEmpty(filter) || (
                       c.DateOfOrderExecution.ToString().Contains(filter) ||
                       c.State.ToString().Contains(filter) ||
                       c.Department.DepartmentName.Contains(filter) ||
                       c.Id.ToString().Contains(filter))) &&
                       c.Users.Any(u => u.Id == userId));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Order, object>>>
                {
                    { "id", c => c.Id},
                    { "DateOfOrderExecution", c => c.DateOfOrderExecution},
                    { "State", c => c.State},
                    { "Name", c => c.Department.DepartmentName}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDireciton == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .Select(p => new OrderListDto
                {
                    Id = p.Id,
                    DateOfOrderExecution = p.DateOfOrderExecution,
                    State = p.State,
                    DepartmentName = p.Department.DepartmentName
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<OrderListDto>(items, totalCount);

            return result;
        }

        // GET: get list of all orders
        public ReturnResult<OrderListDto> GetList(int page, string filter, string sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.Orders
                .Include(c => c.Department)
                .Include(c => c.Users)
                .Where(c => (string.IsNullOrEmpty(filter) || (
                       c.DateOfOrderExecution.ToString().Contains(filter) ||
                       c.State.ToString().Contains(filter) ||
                       c.Department.DepartmentName.Contains(filter) ||
                       c.Id.ToString().Contains(filter))
                ));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Order, object>>>
                {
                    { "id", c => c.Id},
                    { "DateOfOrderExecution", c => c.DateOfOrderExecution},
                    { "State", c => c.State},
                    { "Name", c => c.Department.DepartmentName}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDireciton == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .Select(p => new OrderListDto
                {
                    Id = p.Id,
                    DateOfOrderExecution = p.DateOfOrderExecution,
                    State = p.State,
                    DepartmentName = p.Department.DepartmentName
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<OrderListDto>(items, totalCount);

            return result;
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
