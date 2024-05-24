using System.ComponentModel.DataAnnotations;

namespace KropkaNetApi.X_Entities.Objects.ClientSide
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "NIP is required")]
        public string NIP { get; set; }
        [Required(ErrorMessage = "KRS is required")]
        public string KRS { get; set; }
        [Required(ErrorMessage = "Name of the Company is required")]
        public string CompanyName { get; set; }
        public string? Note { get; set; }



        [Required(ErrorMessage = "Address is required")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }



        public virtual ICollection<Department>? Departments { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}