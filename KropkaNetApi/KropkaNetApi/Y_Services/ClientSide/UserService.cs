using KropkaNetApi.Exceptions;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using AutoMapper;
using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Models.ClientSide.User;
using System.Linq.Expressions;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Models.CompanySide.Employee;
using Microsoft.EntityFrameworkCore;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface IUserService
    {
        ReturnResult<UserListDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection);
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
                    { "Name", e => e.Name},
                    { "Surname", e => e.Surname}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .OrderBy(p => p.Surname)
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
            user.PersonalNumber = dto.PersonalNumber;
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
