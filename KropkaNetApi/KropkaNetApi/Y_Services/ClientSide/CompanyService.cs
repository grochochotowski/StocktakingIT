using KropkaNetApi.X_Models.ClientSide.Company;
using AutoMapper;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Entities.Objects.ClientSide;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface ICompanyService
    {
        int Create(CreateCompanyDto dto);
        IEnumerable<CompanyListDto> GetList(string filter);
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

        // POST: create product
        public int Create(CreateCompanyDto dto)
        {
            var company = _mapper.Map<Company>(dto);

            _context.Companies.Add(company);
            _context.SaveChanges();

            return company.Id;
        }

        // GET: get list of products
        public IEnumerable<CompanyListDto> GetList(string filter)
        {
            var companyList = _context.Companies
                .Where(
                    p => filter == null || (
                    p.CompanyName.ToLower().Contains(filter) ||
                    p.NIP.ToString().Contains(filter) ||
                    p.KRS.ToString().Contains(filter) ||
                    p.Id.ToString().Contains(filter)
                ))
                .OrderBy(p => p.CompanyName)
                .Select(p => new CompanyListDto
                {
                    Id = p.Id,
                    CompanyName = p.CompanyName
                })
                .ToList();

            return companyList;
        }

    }
}
