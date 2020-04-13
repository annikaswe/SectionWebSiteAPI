using System.ComponentModel.DataAnnotations;

namespace Section_Website.Model
{
    public class RegistrationModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
