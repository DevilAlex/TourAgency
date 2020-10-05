using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TourAgency.Models
{
    public class Tour
    {
        public int TourID { get; set; }
        public DateTime StartData { get; set; }
        public int DaysOfRest { get; set; }
        public decimal TourCost { get; set; }
        public int HotelID { get; set; }
        public string TourOperatorID { get; set; }
        public string TransportID { get; set; }
        public int Avaliable { get; set; }
    }
}
