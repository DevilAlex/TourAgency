using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAgency.Models
{
    public class Hotel
    {
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string CityID { get; set; }
        public int Stars { get; set; }
        public int BuildYear { get; set; }
        public double HotelSquare { get; set; }
        public decimal RoomCost { get; set; }
        public string FoodTypeID { get; set; }
        public string RestTypeID { get; set; }
        public string Description { get; set; }
    }
}
