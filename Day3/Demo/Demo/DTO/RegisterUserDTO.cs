using System.ComponentModel.DataAnnotations;

namespace Demo.DTO
{
    public class RegisterUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string CinfirmPassword { get; set; }
        public string Email { get; set; }
    }
}
