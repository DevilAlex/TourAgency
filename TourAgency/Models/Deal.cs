using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAgency.Models
{
    public class Deal
    {
        public int DealID { get; set; }
        public int TourID { get; set; }
        public int ClientID { get; set; }
        public int ManagerID { get; set; }
        public DateTime AgreementDate { get; set; }
        public string AgreementNumber { get; set; }
    }
}
