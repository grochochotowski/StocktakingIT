using KropkaNetApi.X_Models.ClientSide.Company;
using AutoMapper;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using Microsoft.EntityFrameworkCore;
using KropkaNetApi.X_Entities.Enum;
using System.Linq.Expressions;
using KropkaNetApi.Exceptions;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface ICompanyService
    {
        int Create(int userId, CreateCompanyDto dto);
        ReturnResult<CompanyListDto> GetListUser(int userId, int page, string filter, string sortBy, SortDirection sortDireciton);
        ReturnResult<CompanyListDto> GetList(int page, string filter, string sortBy, SortDirection sortDireciton);
        int Delete(int id);
    }
    public class CompanyService : ICompanyService
    {
        private StocktakingContext _context;
        private readonly IMapper _mapper;

        public CompanyService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        // POST: create comany
        public int Create(int userId, CreateCompanyDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                throw new BadRequestException("User not found"+ userId);
            }

            var company = _mapper.Map<Company>(dto);
            company.Users = [user];

            var address = new Address()
            {
                Country = dto.Country,
                City = dto.City,
                ZipCode = dto.ZipCode,
                Street = dto.Street,
                Building = dto.Building,
                Premises = dto.Premises
            };
            _context.Addresses.Add(address);
            _context.SaveChanges();

            company.AddressId = address.Id;

            _context.Companies.Add(company);
            _context.SaveChanges();

            return company.Id;
        }

        // GET: get list of comanies of user
        public ReturnResult<CompanyListDto> GetListUser(int userId, int page, string filter, string sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.Companies
                .Include(c => c.Address)
                .Include(c => c.Users)
                .Where(c => (string.IsNullOrEmpty(filter) || (
                       c.CompanyName.ToLower().Contains(filter.ToLower()) ||
                       c.NIP.Contains(filter) ||
                       c.KRS.Contains(filter) ||
                       c.Id.ToString().Contains(filter))) &&
                       c.Users.Any(u => u.Id == userId));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Company, object>>>
                {
                    { "id", c => c.Id},
                    { "CompanyName", c => c.CompanyName}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDireciton == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .OrderBy(p => p.CompanyName)
                .Select(p => new CompanyListDto
                {
                    Id = p.Id,
                    CompanyName = p.CompanyName
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<CompanyListDto>(items, totalCount);

            return result;
        }

        // GET: get list of all users
        public ReturnResult<CompanyListDto> GetList(int page, string filter, string sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.Companies
                .Include(c => c.Address)
                .Include(c => c.Users)
                .Where(c => (string.IsNullOrEmpty(filter) || (
                       c.CompanyName.ToLower().Contains(filter.ToLower()) ||
                       c.NIP.Contains(filter) ||
                       c.KRS.Contains(filter) ||
                       c.Id.ToString().Contains(filter))
                       ));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Company, object>>>
                {
                    { "id", c => c.Id},
                    { "CompanyName", c => c.CompanyName}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDireciton == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .OrderBy(p => p.CompanyName)
                .Select(p => new CompanyListDto
                {
                    Id = p.Id,
                    CompanyName = p.CompanyName
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<CompanyListDto>(items, totalCount);

            return result;
        }

        // DELETE : delete comany with id
        public int Delete(int id)
        {
            var company = _context.Companies.FirstOrDefault(p => p.Id == id);
            if (company == null) return -1;

            _context.Remove(company);

            return 0;
        }
    }
}
