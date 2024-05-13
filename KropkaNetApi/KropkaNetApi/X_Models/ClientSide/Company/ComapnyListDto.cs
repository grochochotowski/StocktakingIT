using System.ComponentModel.DataAnnotations;

namespace KropkaNetApi.X_Models.ClientSide.Company
{
    public class ComapnyListDto
    {
        public int Id { get; set; }
        public int NIP { get; set; }
        public int KRS { get; set; }
        public string CompanyName { get; set; }
    }
}
