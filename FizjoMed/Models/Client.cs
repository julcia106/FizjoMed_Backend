using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizjoMed.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string PhoneNumber { get; set; }
        public string PersonalPassword { get; set; }
        public string LoginName { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}
