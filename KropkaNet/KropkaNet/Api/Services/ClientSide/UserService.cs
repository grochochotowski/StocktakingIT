using KropkaNetApi.Exceptions;
using AutoMapper;
using System.Linq.Expressions;
using KropkaNet.Objects.Dtos.ClientSide.User;
using KropkaNet.Objects.Entities;
using KropkaNet.Objects.Entities.Enum;
using KropkaNet.Objects.Entities.Models.ClientSide;
using Microsoft.EntityFrameworkCore;

namespace KropkaNet.Api.Services.ClientSide
{
    public interface IUserService
    {
        ReturnResult<UserListDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection);
        List<UserListDto> NotInCompany(int companyId);
        List<UserListDto> NotInOrder(int orderId);
        List<UserListDto> GetFromOrder(int orderId, string sortBy, SortDirection sortDirection);
        List<UserListDto> GetFromCompany(int companyId, string sortBy, SortDirection sortDirection);
        UserDto GetDetails(int id);
        void Update(int id, UpdateUserDto dto);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly StocktakingContext _context;
        private readonly IMapper _mapper;

        public UserService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public ReturnResult<UserListDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Users
               .Where(e => (string.IsNullOrEmpty(filter) || (
                      e.Name.ToLower().Contains(filter.ToLower()) ||
                      e.Surname.ToLower().Contains(filter.ToLower()) ||
                      e.Email.ToLower().Contains(filter.ToLower()) ||
                      e.Id.ToString().Contains(filter))
                      ));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>
                {
                    { "id", e => e.Id},
                    { "name", e => e.Name},
                    { "surname", e => e.Surname}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .Select(p => new UserListDto
                {
                    Id = p.Id,
                    Surname = p.Surname,
                    Name = p.Name
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<UserListDto>(items, totalCount);

            return result;
        }

        public List<UserListDto> NotInCompany(int companyId)
        {
            var users = _context.Users
               .Where(u => !u.Companies.Any(c => c.Id == companyId))
               .ToList();

            var userDtos = _mapper.Map<List<UserListDto>>(users);

            return userDtos;
        }
        public List<UserListDto> NotInOrder(int orderId)
        {
            var users = _context.Users
               .Where(u => !u.Orderds.Any(c => c.Id == orderId))
               .ToList();

            var userDtos = _mapper.Map<List<UserListDto>>(users);

            return userDtos;
        }

        public List<UserListDto> GetFromOrder(int orderId, string? sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Users
                .Include(u => u.Orderds)
                .Where(u => u.Orderds.Any(o => o.Id == orderId));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>
                {
                    { "id", u => u.Id},
                    { "name", u => u.Name},
                    { "surname", u => u.Surname}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var users = baseQuery
                .ToList();

            var usersDto = _mapper.Map<List<UserListDto>>(users);
            return usersDto;
        }
        public List<UserListDto> GetFromCompany(int companyId, string? sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Users
                .Where(u => u.Companies.Any(c => c.Id == companyId));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<User, object>>>
                {
                    { "id", u => u.Id},
                    { "name", u => u.Name},
                    { "surname", u => u.Surname}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var users = baseQuery
                .ToList();

            var usersDto = _mapper.Map<List<UserListDto>>(users);
            return usersDto;
        }

        public UserDto GetDetails(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new NotFoundException("User not found");

            return _mapper.Map<UserDto>(user);
        }

        public void Update(int id, UpdateUserDto dto)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Note = dto.Note;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new NotFoundException("User not found");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

}
