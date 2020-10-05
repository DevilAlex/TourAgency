using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class HotelViewModel
    {
        [Display(Name = "Название")]
        public string HotelName { get; set; }
        [Display(Name = "Страна")]
        public string CountryName { get; set; }
        [Display(Name = "Город")]
        public string CityName { get; set; }
        [Display(Name = "Звезды")]
        public int Stars { get; set; }
        [Display(Name = "Год постройки")]
        public int BuildYear { get; set; }
        [Display(Name = "Площадь территории")]
        [DisplayFormat(DataFormatString = "{0:F1}")]
        public decimal HotelSquare { get; set; }
        [Display(Name = "Стоимость проживания")]
        public decimal RoomCost { get; set; }
        [Display(Name = "Тип питания")]
        public string FoodType { get; set; }
        [Display(Name = "Тип отдыха")]
        public string RestType { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
