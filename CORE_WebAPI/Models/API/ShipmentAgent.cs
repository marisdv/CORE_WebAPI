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
            if (agent.DateEmployed != null)
            {
                this.DateEmployed = agent.DateEmployed;
            }
            if (agent.AgentCompany != null)
            {
                this.AgentCompany = agent.AgentCompany;
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
            if (agent.ApplicationAccepted != 0)
            {
                this.ApplicationAccepted = agent.ApplicationAccepted;
            }
            if (agent.AgentAvailability != 0)
            {
                this.AgentAvailability = agent.AgentAvailability;
            }
            if (agent.AgentActive != 0)
            {
                this.AgentActive = agent.AgentActive;
            }
            if (agent.Insurance != 0)
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
            if (agent.LicenceImageId != 0)
            {
                this.LicenceImageId = agent.LicenceImageId;
            }
            if (agent.AgentImageId != 0)
            {
                this.AgentImageId = agent.AgentImageId;
            }
            if (agent.CurrentLocId != 0)
            {
                this.CurrentLocId = agent.CurrentLocId;
            }
        }
    }
}
