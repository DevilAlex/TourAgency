using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TourAgency.Models
{
    public class SearchParamsViewModel
    {
        
        [Display(Name = "C")]
        public DateTime DateFrom { get; set; }
        [Display(Name = "ПО")]
        public DateTime DateTo { get; set; }
        [Display(Name = "C")]
        public int DaysFrom { get; set; }
        [Display(Name = "ПО")]
        public int DaysTo { get; set; }
        [Display(Name = "ОТ")]
        public decimal CostFrom { get; set; }
        [Display(Name = "ДО")]
        public decimal CostTo { get; set; }
        [Display(Name = "Страна отдыха")]
        public string Country { get; set; }
        [Display(Name = "Город")]
        public List<string> City { get; set; }
        [Display(Name = "Тип питания")]
        public List<string> Food { get; set; }
        [Display(Name = "Звездность")]
        public List<int> Star { get; set; }
    }
}
