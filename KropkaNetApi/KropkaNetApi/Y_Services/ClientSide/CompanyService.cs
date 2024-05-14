using KropkaNetApi.X_Models.ClientSide.Company;
using AutoMapper;
using KropkaNetApi.X_Entities;
using KropkaNetApi.X_Entities.Objects.ClientSide;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface ICompanyService
    {
        int Create(CreateCompanyDto dto);
        IEnumerable<CompanyListDto> GetList(string filter);
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
        public int Create(CreateCompanyDto dto)
        {
            var company = _mapper.Map<Company>(dto);
            var address = $"{company.Address.Country} ,"
                + $"{company.Address.City}, "
                + $"{company.Address.ZipCode}, "
                + $"{company.Address.Street}, "
                + $"{company.Address.Building} "
                + $"{(company.Address.Premises != null ? "/" + company.Address.Premises : "")}\n";

            _context.Companies.Add(company);
            _context.SaveChanges();

            return company.Id;
        }

        // GET: get list of comanies
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

        // DELETE : delete comany with id
        public int Delete(int id)
        {
            var company = _context.Companies.FirstOrDefault(p => p.Id == id);
            var address = _context.Addresses.FirstOrDefault(p => p.Id == id);
            if (company == null) return -1;

            _context.Remove(company);
            _context.Remove(address);

            return 0;
        }
    }
}
