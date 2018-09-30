using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_WebAPI.Models.Reports
{
    public class IncomeReport
    {
        public DateTime StartDate;
        public DateTime EndDate;
        public double TotalIncome;
        public double ShipmentIncome;
        public double PenaltyIncome;
        public double AgentPayments;
        public double ProfitLoss;
        public bool isProfit;
        public string EmpFullName;

        public void GenerateIncomeReport()
        {

        }
    }
}
