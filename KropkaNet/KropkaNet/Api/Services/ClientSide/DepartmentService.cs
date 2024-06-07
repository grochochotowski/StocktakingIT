using AutoMapper;
using KropkaNet.Objects.Dtos.ClientSide.Department;
using KropkaNet.Objects.Dtos.ClientSide.User;
using KropkaNet.Objects.Entities;
using KropkaNet.Objects.Entities.Enum;
using KropkaNet.Objects.Entities.Models.ClientSide;
using KropkaNetApi.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KropkaNet.Api.Services.ClientSide
{
    public interface IDepartmentService
    {
        int Create(CreateDepartmentDto dto);
        ReturnResult<DepartmentListDto> GetList(int page, string? filter, string? sortBy, SortDirection sortDireciton);
        List<DepartmentListDto> GetFromCompany(int companyId, string? sortBy, SortDirection sortDireciton);
        List<UserDepartmentsDto> GetUserDepartments(int userId);
        int Update(int id, CreateDepartmentDto dto);
        int Delete(int id);
    }
    public class DepartmentService : IDepartmentService
    {
        private StocktakingContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(StocktakingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST: create department
        public int Create(CreateDepartmentDto dto)
        {
            var company = _context.Companies.FirstOrDefault(c => c.Id == dto.CompanyId);
            if (company == null) throw new NotFoundException("Company not found");

            var department = _mapper.Map<Department>(dto);
           
            _context.Departments.Add(department);
            _context.SaveChanges();

            return department.Id;
        }

        // GET: get list of all departemnts
        public ReturnResult<DepartmentListDto> GetList(int page, string? filter, string ?sortBy, SortDirection sortDireciton)
        {
            var baseQuery = _context.Departments
                .Where(c => (string.IsNullOrEmpty(filter) || (
                       c.DepartmentName.ToLower().Contains(filter.ToLower()) ||
                       c.Id.ToString().Contains(filter))
                       ));

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Department, object>>>
                {
                    { "id", d => d.Id},
                    { "DepartmentName", d => d.DepartmentName}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDireciton == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var items = baseQuery
                .Skip(10 * (page - 1))
                .Take(10)
                .Select(p => new DepartmentListDto
                {
                    Id = p.Id,
                    DepartmentName = p.DepartmentName
                })
                .ToList();

            var totalCount = baseQuery.Count();

            var result = new ReturnResult<DepartmentListDto>(items, totalCount);

            return result;
        }

        //GET : get list of user departments
        public List<UserDepartmentsDto> GetUserDepartments(int userId)
        {
            var departments = _context.Departments
                .Include(d => d.Company)
                .Where(d => d.Company.Users.Any(u => u.Id == userId))
                .Select(d => new UserDepartmentsDto
                {
                    Id = d.Id,
                    CompanyName = d.Company.CompanyName,
                    DepartmentName = d.DepartmentName
                })
                .ToList();

            return departments;
        }

        //GET : get list from companies
        public List<DepartmentListDto> GetFromCompany(int companyId, string? sortBy, SortDirection sortDirection)
        {
            var baseQuery = _context.Departments
                .Where(c => c.CompanyId == companyId);

            if (!string.IsNullOrEmpty(sortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Department, object>>>
                {
                    { "id", c => c.Id},
                    { "departmentName", c => c.DepartmentName}
                };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var departments = baseQuery
                .Select(p => new DepartmentListDto
                {
                    Id = p.Id,
                    DepartmentName = p.DepartmentName
                })
                .ToList();

            var departmentDtos = _mapper.Map<List<DepartmentListDto>>(departments);
            return departmentDtos;
        }

        // PUT: update department
        public int Update(int id, CreateDepartmentDto dto)
        {
            var department = _context.Departments
                .FirstOrDefault(d => d.Id == id);

            if (department == null) throw new NotFoundException("Department not found");

            department.DepartmentName = dto.DepartmentName;

            _context.SaveChanges();

            return department.Id;
        }

        // DELETE : delete department with id
        public int Delete(int id)
        {
            var department = _context.Departments.FirstOrDefault(p => p.Id == id);
            if (department == null) return -1;

            _context.Remove(department);
            _context.SaveChanges();

            return 0;
        }
    }
}
