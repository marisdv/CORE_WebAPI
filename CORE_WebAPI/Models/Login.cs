using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Login
    {
        public Login()
        {
            Employee = new HashSet<Employee>();
            Sender = new HashSet<Sender>();
            ShipmentAgent = new HashSet<ShipmentAgent>();
        }

        public int LoginId { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public int UserTypeId { get; set; }

        public UserType UserType { get; set; }
        public ICollection<Employee> Employee { get; set; }
        public ICollection<Sender> Sender { get; set; }
        public ICollection<ShipmentAgent> ShipmentAgent { get; set; }
    }
}
