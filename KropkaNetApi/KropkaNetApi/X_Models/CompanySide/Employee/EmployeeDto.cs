using KropkaNetApi.Dtos.CompanySide.Position;
using KropkaNetApi.Dtos.CompanySide.Stocktaking;
namespace KropkaNetApi.Dtos.CompanySide.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }


        public int PositionId { get; set; }
        public virtual PositionDto? Position { get; set; }


        public virtual ICollection<StocktakingDto>? Stocktakings { get; set; }
    }
}
