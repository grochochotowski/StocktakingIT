using KropkaNetApi.X_Models.ClientSide.Company;

namespace KropkaNetApi.Y_Services.ClientSide
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAll();
    }
    public class CompanyService
    {
    }
}
