using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models;

public class Company
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int NIP { get; set; }
    [Required]
    public int KRS { get; set; }
    [Required]
    public string NazwaFirmy { get; set; }
    
    //public virtual ICollection<> { get; set; }
    //public virtual ICollection<> { get; set; }

}