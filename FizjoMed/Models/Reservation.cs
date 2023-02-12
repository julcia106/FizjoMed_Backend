using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizjoMed.Models
{
    public class Reservation
    {
        public int reservation_id { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int treatment_id { get; set; }
        public string client_Name { get; set; }
        public string client_Surname { get; set; }
        public string client_Phone { get; set; }
        public string client_Email { get; set; }
        public int worker_id { get; set; }
    }
}
