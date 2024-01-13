using System.ComponentModel.DataAnnotations;

namespace Shiva_Enterprise_APIs.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool RememberMe { get; internal set; }
        public string? ReturnUrl { get; set; }
    }
}
