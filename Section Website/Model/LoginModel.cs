using System.ComponentModel.DataAnnotations;

namespace Section_Website.Model
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
