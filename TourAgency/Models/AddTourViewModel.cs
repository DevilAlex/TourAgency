using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class AddTourViewModel
    {
        [Display(Name = "Дата отправления")]
        [UIHint("Date")]
        public DateTime Data { get; set; }
        [Display(Name = "Количество дней")]
        public int Days { get; set; }
        [Display(Name = "Страна")]
        public int Country { get; set; }
        [Display(Name = "Название отеля")]
        public string Hotel { get; set; }
        [Display(Name = "Туроператор")]
        public string TourOperator { get; set; }
        [Display(Name = "Тип транспорта")]
        public string Transport { get; set; }
        [Display(Name = "Количество туров")]
        public int Avaliable { get; set; }
    }
}
