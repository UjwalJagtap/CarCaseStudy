using System.ComponentModel.DataAnnotations;

namespace MVCCoreApp.Models
{
    public class Login
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string UserType { get; set; }  // Example: Admin, User, etc.
    }
}
