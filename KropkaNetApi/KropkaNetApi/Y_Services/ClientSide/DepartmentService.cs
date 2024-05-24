using AutoMapper;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.ClientSide.Department;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using KropkaNetApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface IDepartmentService
    {
        int Create(int? comapnyId, CreateDepartmentDto dto);
        IEnumerable<DepartmentListDto> GetList(string filter);
        void RemoveOrder(int comapnyId, int departmentId);
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
        public int Create(int? comapnyId, CreateDepartmentDto dto)
        {
            var department = _mapper.Map<Department>(dto);

            var company = _context.Companies.FirstOrDefault(c => c.Id == comapnyId);
           
            _context.Departments.Add(department);
            _context.SaveChanges();

            return department.Id;
        }

        // GET: get list of departments
        public IEnumerable<DepartmentListDto> GetList(string filter)
        {
            var departmentList = _context.Departments
                .Where(
                    p => filter == null || (
                    p.DepartmentName.ToLower().Contains(filter) ||
                    p.Id.ToString().Contains(filter)
                ))
                .OrderBy(p => p.DepartmentName)
                .Select(p => new DepartmentListDto
                {
                    Id = p.Id,
                    DepartmentName = p.DepartmentName
                })
                .ToList();

            return departmentList;
        }

        // PATCH: remove user
        public void RemoveOrder(int comapnyId, int departmentId)
        {
            var department = _context.Departments
                .Include(c => c.Companies)
                .FirstOrDefault(c => c.Id == departmentId);
            var company = _context.Companies
                .FirstOrDefault(c => c.Id == comapnyId);

            if (department == null) throw new NotFoundException("Company not found");
            if (company == null) throw new NotFoundException("User not found");
            if (!department.Companies.Any(u => u.Id == company.Id)) throw new BadRequestException("User is not in company");

            department.Companies.Remove(company);
            _context.SaveChanges();
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
