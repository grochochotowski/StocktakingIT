using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models;

public class Address
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public int ZipCode { get; set; }
    [Required]
    public string Street { get; set; }
    [Required]
    public string Building { get; set; }
    [Required]
    public string Local { get; set; }
}