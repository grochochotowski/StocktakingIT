using System.ComponentModel.DataAnnotations;

namespace KropkaNet.Models.Objects;

public class Address
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Country is required")]
    public string Country { get; set; }
    [Required(ErrorMessage = "City is required")]
    public string City { get; set; }
    [Required(ErrorMessage = "Zip code is required")]
    public string ZipCode { get; set; }
    [Required(ErrorMessage = "Street is required")]
    public string Street { get; set; }
    [Required(ErrorMessage = "Building is required")]
    public string Building { get; set; }
    public string? Premises { get; set; }
}