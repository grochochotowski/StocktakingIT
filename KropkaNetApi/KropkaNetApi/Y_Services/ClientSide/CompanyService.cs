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
        int Create(int? userId, CreateCompanyDto dto);
        ReturnResult<CompanyListDto> GetListUser(int userId, int page, string filter, string sortBy, SortDirection sortDireciton);
        ReturnResult<CompanyListDto> GetList(int page, string filter, string sortBy, SortDirection sortDireciton);
        CompanyDto GetDetails(int id);
        int Update(int id, CreateCompanyDto dto);
        void AddUser(int userId, int companyId);
        void RemoveUser(int userId, int companyId);
        void Delete(int id);
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
        public int Create(int? userId, CreateCompanyDto dto)
        {
            var company = _mapper.Map<Company>(dto);

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                company.Users = [user];
            }

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

        // GET: get list of all companies
        public ReturnResult<CompanyListDto> GetList(int page, string filter, string sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.Companies
                .Include(c => c.Address)
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

        // GET: get details about company
        public CompanyDto GetDetails(int id)
        {
            var company = _context.Companies
                .Include(c => c.Address)
                .FirstOrDefault(c => c.Id == id);

            if (company == null) throw new NotFoundException("Company not found");

            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }

        // PUT: update comany
        public int Update(int id, CreateCompanyDto dto)
        {
            var company = _context.Companies
                .Include(c => c.Address)
                .FirstOrDefault(c => c.Id == id);

            if (company == null) throw new NotFoundException("Company not found");

            company.NIP = dto.NIP;
            company.KRS = dto.KRS;
            company.CompanyName = dto.CompanyName;
            company.Note = dto.Note;

            company.Address.Country = dto.Country;
            company.Address.City = dto.City;
            company.Address.ZipCode = dto.ZipCode;
            company.Address.Street = dto.Street;
            company.Address.Building = dto.Building;
            company.Address.Premises = dto.Premises;

            _context.SaveChanges();

            return company.Id;
        }

        // PATCH: add user
        public void AddUser(int userId, int companyId)
        {
            var company = _context.Companies
                .Include(c => c.Users)
                .FirstOrDefault(c => c.Id == companyId);
            var user = _context.Users
                .FirstOrDefault(c => c.Id == userId);

            if (company == null) throw new NotFoundException("Company not found");
            if (user == null) throw new NotFoundException("User not found");
            if (company.Users.Any(u => u.Id == user.Id)) throw new BadRequestException("User already in company");

            company.Users.Add(user);
            _context.SaveChanges();
        }

        // PATCH: remove user
        public void RemoveUser(int userId, int companyId)
        {
            var company = _context.Companies
                .Include(c => c.Users)
                .FirstOrDefault(c => c.Id == companyId);
            var user = _context.Users
                .FirstOrDefault(c => c.Id == userId);

            if (company == null) throw new NotFoundException("Company not found");
            if (user == null) throw new NotFoundException("User not found");
            if (!company.Users.Any(u => u.Id == user.Id)) throw new BadRequestException("User is not in company");

            company.Users.Remove(user);
            _context.SaveChanges();
        }

        // DELETE : delete comany with id
        public void Delete(int id)
        {
            var company = _context.Companies.FirstOrDefault(p => p.Id == id);
            if (company == null) throw new NotFoundException("Company not found");

            _context.Remove(company);
            _context.SaveChanges();
        }
    }
}
