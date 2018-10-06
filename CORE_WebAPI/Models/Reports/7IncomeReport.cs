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
        public decimal TotalIncome { get; set; }
        public decimal ShipmentIncome { get; set; }
        public decimal PenaltyIncome { get; set; }
        public decimal AgentPayments { get; set; }
        public decimal ProfitLoss { get; set; }
        public bool isProfit { get; set; }
        public string EmpFullName { get; set; }
    }
}
