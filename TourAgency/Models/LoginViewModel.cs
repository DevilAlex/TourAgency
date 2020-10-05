using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Не указан логин")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [UIHint("password")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
