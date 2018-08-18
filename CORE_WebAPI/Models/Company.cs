using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyStreetAddress { get; set; }
        public string CompanySuburb { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyPostalCode { get; set; }
        public string VatNo { get; set; }
        public string RegistrationNo { get; set; }
        public string PayGateId { get; set; }
        public string PayGatePassword { get; set; }
    }
}
