using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class BuyTourViewModel
    {
        public int TourID { get; set; }
        public string CountryID { get; set; }
        [Display(Name="Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Дата рождения")]
        [UIHint("Date")]
        public DateTime Birthday { get; set; }
    }
}
