using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class ManagerViewModel
    {
        public int ManagerID { get; set; }
        [Display(Name = "Имя менеджера")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия менеджера")]
        public string LastName { get; set; }
        [Display(Name = "Размер премии")]
        public int? Prime { get; set; }
        [Display(Name = "Страна")]
        public string CountryName { get; set; }
        [Display(Name = "Количество сделок")]
        public int CountTours { get; set; }
        [Display(Name = "Сумма сделок")]
        public decimal SumCost { get; set; }
    }
}
