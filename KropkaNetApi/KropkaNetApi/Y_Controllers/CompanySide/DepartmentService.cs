using AutoMapper;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.ClientSide.Department;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface IDepartmentService
    {
        int Create(CreateDepartmentDto dto);
        IEnumerable<DepartmentListDto> GetList(string filter);
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

        // POST: create product
        public int Create(CreateDepartmentDto dto)
        {
            var department = _mapper.Map<Department>(dto);

            _context.Departments.Add(department);
            _context.SaveChanges();

            return department.Id;
        }

        // GET: get list of products
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
    }
}
