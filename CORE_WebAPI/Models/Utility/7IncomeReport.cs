using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_WebAPI.Models.Reports
{
    public class IncomeReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalIncome { get; set; }
        public double ShipmentIncome { get; set; }
        public double PenaltyIncome { get; set; }
        public double AgentPayments { get; set; }
        public double ProfitLoss { get; set; }      
        public bool isProfit { get; set; }
        public string EmpFullName { get; set; }
    }
}
