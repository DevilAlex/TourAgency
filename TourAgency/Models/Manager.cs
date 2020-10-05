using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class Manager
    {
        public int ManagerID { get; set; }

        [Required(ErrorMessage = "Введите имя менеджера")]
        [Display(Name = "Имя менеджера")]
        public string ManagerName { get; set; }

        [Required(ErrorMessage = "Введите фамилию менеджера")]
        [Display(Name = "Фамилия менеджера")]
        public string ManagerSurname { get; set; }

        [Display(Name = "Размер премии")]
        public int? Prime{ get; set; }

        [RegularExpression(@"^[A-Z]{2}[A-Za-z]+$", ErrorMessage ="Некорректный логин")]
        [Display(Name = "Логин менеджера")]
        public string ManagerLogin { get; set; }

        [DataType(DataType.Password)]
        public string ManagerPassword { get; set; }

        [Display(Name = "Страна")]
        public string CountryID { get; set; }
        public string ManagerRole { get; set; }

    }
}
