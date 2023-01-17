using System.ComponentModel.DataAnnotations;

namespace DaveStore.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(200, MinimumLength = 2)]
        public string Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        [StringLength(200, MinimumLength = 2)]
        public string Password { get; set; } 
        public bool RememberMe { get; set; }
    }
}
