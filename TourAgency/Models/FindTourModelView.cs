using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class FindTourModelView
    {
        public int Index { get; set; }
        public int TourID { get; set; }
        public string CountryID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Hotel { get; set; }
        [UIHint("Date")]
        public DateTime DateIn { get; set; }
        public int Days { get; set; }
        public string Food { get; set; }
        public int Stars { get; set; }
        public decimal Cost { get; set; }
        public string TourOper { get; set; }
        public int Avaliable { get; set; }
    }
}
