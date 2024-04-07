using System.ComponentModel.DataAnnotations;

namespace HMS.DTOs
{
    public class CustomerDTO
    {
        [Display(Name="customer name")]
        [Required(ErrorMessage = "Enter customer name!")]
        public string CustomerName { get; set; }

        [Display(Name = "email address")]
        [EmailAddress(ErrorMessage = "Enter valid email address!")]
        public string CustomerEmail { get; set; }

        [Display(Name = "phone number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter a valid phone number")]
        public string CustomerPhone { get; set; }

        public string? UserAlreadyExists {  get; set; }

    }
}
