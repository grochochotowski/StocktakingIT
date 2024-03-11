using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models.Objects;

public class Department
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name of the Department is required")]
    public string DepartmentName { get; set; }
}