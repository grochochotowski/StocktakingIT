using KropkaNetApi.Exceptions;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using AutoMapper;
using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Models.ClientSide.User;
using System.Linq.Expressions;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface IUserService
    {
        int Create(CreateUserDto dto);
        ReturnResult<UserDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection);
        UserDto GetDetails(int id);
        void Update(int id, UserDto dto);
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

        public int Create(CreateUserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public ReturnResult<UserDto> GetAll(int page, string filter, string sortBy, SortDirection sortDirection)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(u => u.Name.Contains(filter) || u.Email.Contains(filter));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                var param = Expression.Parameter(typeof(User), "u");
                var sortExpression = Expression.Lambda<Func<User, object>>(
                    Expression.Convert(Expression.Property(param, sortBy), typeof(object)), param);

                query = sortDirection == SortDirection.ASC ? query.OrderBy(sortExpression)
                                                            : query.OrderByDescending(sortExpression);
            }

            var list = query.Skip((page - 1) * 10).Take(10)
                            .Select(u => _mapper.Map<UserDto>(u)).ToList();

            var totalCount = query.Count();
            return new ReturnResult<UserDto>(list, totalCount);
        }
    

        public UserDto GetDetails(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new NotFoundException("User not found");

            return _mapper.Map<UserDto>(user);
        }

        public void Update(int id, UserDto dto)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            _mapper.Map(dto, user);  // Mapowanie UserDto na User
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
