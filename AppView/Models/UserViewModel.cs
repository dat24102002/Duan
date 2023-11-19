
using System.ComponentModel.DataAnnotations;

namespace AppView.Models
{
    public class UserViewModel
    {
        [EmailAddress(ErrorMessage ="Email phải đúng định dạng")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage ="Password phải tối thiểu 6 ký tự")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
