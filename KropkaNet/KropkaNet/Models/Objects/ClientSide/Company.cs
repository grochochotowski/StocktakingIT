using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models.Objects.ClientSide;

public class Company
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "NIP is required")]
    public int NIP { get; set; }
    [Required(ErrorMessage = "KRS is required")]
    public int KRS { get; set; }
    [Required(ErrorMessage = "Name of the Company is required")]
    public string CompanyName { get; set; }
    public string? Note { get; set; }

    public virtual ICollection<Department>? Departments { get; set; }
    public virtual Address Address { get; set; }

}