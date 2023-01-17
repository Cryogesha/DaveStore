using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DaveStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(200)]
        public string FirstName { get; set; }

        [MaxLength(200)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string Country { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }

        [MaxLength(50)]
        public string PostCode { get; set; }
    }
}
