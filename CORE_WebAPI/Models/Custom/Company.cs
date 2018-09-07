using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Company
    {
        public void UpdateChangedFields(Company company)
        {
            if (company.CompanyName != null)
            {
                this.CompanyName = company.CompanyName;
            }

            if (company.CompanyPhone != null)
            {
                this.CompanyPhone = company.CompanyPhone;
            }

            if (company.CompanyEmail != null)
            {
                this.CompanyEmail = company.CompanyEmail;
            }

            if (company.CompanyStreetAddress != null)
            {
                this.CompanyStreetAddress = company.CompanyStreetAddress;
            }
          
            if (company.CompanySuburb != null)
            {
                this.CompanySuburb = company.CompanySuburb;
            }

            if (company.CompanyCity != null)
            {
                this.CompanyCity = company.CompanyCity;
            }

            if (company.CompanyPostalCode != null)
            {
                this.CompanyPostalCode = company.CompanyPostalCode;
            }

            if (company.VatNo != null)
            {
                this.VatNo = company.VatNo;
            }

            if (company.RegistrationNo != null)
            {
                this.RegistrationNo = company.RegistrationNo;
            }

            if (company.PayGateId != null)
            {
                this.PayGateId = company.PayGateId;
            }

            if (company.PayGatePassword != null)
            {
                this.PayGatePassword = company.PayGatePassword;
            }

            if (company.CompanyCity != null)
            {
                this.CompanyCity = company.CompanyCity;
            }
        }
    }
}
