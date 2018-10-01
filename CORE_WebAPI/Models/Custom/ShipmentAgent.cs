using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class ShipmentAgent
    {
        public void UpdateChangedFields(ShipmentAgent agent)
        {
            if (agent.AgentName != null)
            {
                this.AgentName = agent.AgentName;
            }
            if (agent.AgentSurname != null)
            {
                this.AgentSurname = agent.AgentSurname;
            }
            if (agent.AgentEmail != null)
            {
                this.AgentEmail = agent.AgentEmail;
            }
            if (agent.AgentNationalId != null)
            {
                this.AgentNationalId = agent.AgentNationalId;
            }
            if (agent.AgentPassportNo != null)
            {
                this.AgentPassportNo = agent.AgentPassportNo;
            }
            if (agent.DateEmployed != null && agent.DateEmployed != new DateTime())
            {
                this.DateEmployed = agent.DateEmployed;
            }
            if (agent.BankAccNo != null)
            {
                this.BankAccNo = agent.BankAccNo;
            }
            if (agent.BankName != null)
            {
                this.BankName = agent.BankName;
            }
            if (agent.BankAccType != null)
            {
                this.BankAccType = agent.BankAccType;
            }
            if (agent.BankBranchCode != null)
            {
                this.BankBranchCode = agent.BankBranchCode;
            }
            if (agent.ApplicationAccepted != null)
            {
                this.ApplicationAccepted = agent.ApplicationAccepted;
            }
            if (agent.AgentAvailability != null)
            {
                this.AgentAvailability = agent.AgentAvailability;
            }
            if (agent.AgentActive != null)
            {
                this.AgentActive = agent.AgentActive;
            }
            if (agent.Insurance != null)
            {
                this.Insurance = agent.Insurance;
            }
            if (agent.CityId != 0)
            {
                this.CityId = agent.CityId;
            }
            if (agent.LoginId != 0)
            {
                this.LoginId = agent.LoginId;
            }
            if (agent.LicenceImage != null)
            {
                this.LicenceImage = agent.LicenceImage;
            }
            if (agent.AgentImage != null)
            {
                this.AgentImage = agent.AgentImage;
            }
        }
    }
}
