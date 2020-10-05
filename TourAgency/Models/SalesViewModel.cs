using System;
using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class SalesViewModel
    {
        public int Index { get; set; }
        [Display(Name = "ФИО клиента")]
        public string ClientName { get; set; }
        [Display(Name = "ФИО менеджера")]
        public string ManagerName { get; set; }
        [Display(Name = "Дни")]
        public int Days { get; set; }
        [Display(Name = "Страна")]
        public string Country { get; set; }
        [Display(Name = "Туроператор")]
        public string TourOper { get; set; }
        [Display(Name = "Дата договора")]
        [UIHint("Date")]
        public DateTime DateIn { get; set; }
        [Display(Name = "Стоимость")]
        public decimal Cost { get; set; }
        [Display(Name = "Номер договора")]
        public string AgrNumber { get; set; }
        [Display(Name = "Город")]
        public string City { get; set; }
        [Display(Name = "Отель")]
        public string Hotel { get; set; }
        [Display(Name = "Дата отправления")]
        [UIHint("Date")]
        public DateTime DateOut { get; set; }
    }
}
