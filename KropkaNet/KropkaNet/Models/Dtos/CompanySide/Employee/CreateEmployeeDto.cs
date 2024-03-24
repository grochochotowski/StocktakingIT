using KropkaNet.Models.Dtos.CompanySide.Position;
using KropkaNet.Models.Dtos.CompanySide.Stocktaking;

namespace KropkaNet.Models.Dtos.CompanySide.Employee
{
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }
        public int PositionId { get; set; }
    }
}
