using AutoMapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using KropkaNetApi.Exceptions;
using KropkaNet.Objects.Dtos.ClientSide.Order;
using KropkaNet.Objects.Entities;
using KropkaNet.Objects.Entities.Enum;
using KropkaNet.Objects.Entities.Models.ClientSide;

namespace KropkaNet.Api.Services.ClientSide
{
    public interface IOrderService
    {
        int Create(int? userId, CreateOrderDto dto);
        ReturnResult<OrderListDto> GetListUser(int userId, int page, string filter, string sortBy, SortDirection sortDireciton);
        ReturnResult<OrderListDto> GetList(int page, string filter, string sortBy, SortDirection sortDireciton);
        OrderDetailsDto GetDetails(int id);
        int Update(int id, UpdateOrderDto dto);
        void AddUser(int userId, int orderId);
        void RemoveUser(int userId, int orderId);
        void ChangeState(int id, int state);
        void Delete(int id);
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
            var department = _context.Departments.FirstOrDefault(d => d.Id == dto.DepartmentId);
            if (department == null) throw new NotFoundException("Department not found");

            var order = _mapper.Map<Order>(dto);

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                order.Users = [user];
            }

            order.State = -1;

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

        // GET: get details about company
        public OrderDetailsDto GetDetails(int id)
        {
            var order = _context.Orders
                .Include(c => c.Department)
                .Select(p => new OrderDetailsDto
                {
                    Id = p.Id,
                    DateOfOrderExecution = p.DateOfOrderExecution,
                    State = p.State,
                    DepartmentName = p.Department.DepartmentName,
                    StocktakingId = p.StocktakingId
                })
                .FirstOrDefault(c => c.Id == id);

            if (order == null) throw new NotFoundException("Order not found");

            return order;
        }

        // PUT: update comany
        public int Update(int id, UpdateOrderDto dto)
        {
            var order = _context.Orders
                .FirstOrDefault(c => c.Id == id);

            if (order == null) throw new NotFoundException("Order not found");

            order.DateOfOrderExecution = dto.DateOfOrderExecution;

            _context.SaveChanges();

            return order.Id;
        }

        // PATCH: add user
        public void AddUser(int userId, int orderId)
        {
            var order = _context.Orders
                .Include(c => c.Users)
                .FirstOrDefault(c => c.Id == orderId);
            var user = _context.Users
                .FirstOrDefault(c => c.Id == userId);

            if (order == null) throw new NotFoundException("Order not found");
            if (user == null) throw new NotFoundException("User not found");
            if (order.Users.Any(u => u.Id == user.Id)) throw new BadRequestException("User already in company");

            order.Users.Add(user);
            _context.SaveChanges();
        }

        // PATCH: remove user
        public void RemoveUser(int userId, int orderId)
        {
            var order = _context.Orders
                .Include(c => c.Users)
                .FirstOrDefault(c => c.Id == orderId);
            var user = _context.Users
                .FirstOrDefault(c => c.Id == userId);

            if (order == null) throw new NotFoundException("Order not found");
            if (user == null) throw new NotFoundException("User not found");
            if (!order.Users.Any(u => u.Id == user.Id)) throw new BadRequestException("User is not in order");

            order.Users.Remove(user);
            _context.SaveChanges();
        }

        // PATCH: change state
        public void ChangeState(int id, int state)
        {
            var order = _context.Orders
               .FirstOrDefault(c => c.Id == id);

            if (order == null) throw new NotFoundException("Order not found");

            order.State = state;

            _context.SaveChanges();
        }

        // DELETE : delete order with id
        public void Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(p => p.Id == id);
            if (order == null) throw new NotFoundException("Order not found");

            _context.Remove(order);
            _context.SaveChanges();
        }
    }
}
