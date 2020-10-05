using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class Client
    {
        public int ClientID { get; set; }

        [Required(ErrorMessage = "Введите имя клиента")]
        [Display(Name = "Имя клиента")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Введите фамилию клиента")]
        [Display(Name = "Фамилия клиента")]
        public string ClientSurname { get; set; }

        [RegularExpression(@"[0-9]{4}\s[0-9]{6}\s", ErrorMessage = "Некорректный паспорт")]
        [Display(Name = "Российский паспорт")]
        public string RusPassport { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }

        [RegularExpression(@"[0-9]{4}\s[0-9]{6}\s", ErrorMessage = "Некорректный паспорт")]
        [Display(Name = "Загран. паспорт")]
        public string Passport { get; set; }

        [StringLength(50, ErrorMessage ="Слишком много символов")]
        [Display(Name = "Место работы")]
        public string WorkPlace { get; set; }

        [RegularExpression(@"^[A-Z]{2}[A-Za-z]+$", ErrorMessage = "Некорректный логин")]
        [Display(Name = "Логин менеджера")]
        public string ManagerLogin { get; set; }

        [Display(Name = "Дата обновления")]
        [UIHint("Date")]
        public DateTime EditDate { get; set; }
    }
}
