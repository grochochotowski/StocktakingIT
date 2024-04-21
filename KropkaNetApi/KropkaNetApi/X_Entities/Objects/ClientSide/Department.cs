using System.ComponentModel.DataAnnotations;

namespace KropkaNetApi.X_Entities.Objects.ClientSide;

public class Department
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name of the Department is required")]
    public string DepartmentName { get; set; }



    [Required(ErrorMessage = "Company is required")]
    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }



    public virtual ICollection<Order>? Orders { get; set; }
}