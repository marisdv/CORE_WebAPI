using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE_WebAPI.Models;
using CORE_WebAPI.Models.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CORE_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {

        private readonly ProjectCALServerContext _context;

        public ReportsController(ProjectCALServerContext context)
        {
            _context = context;
        }

        #region REPORTS
        public struct ReportModel
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int EmployeeId { get; set; }
        }
        public struct EmpModel
        {
            public int EmployeeId { get; set; }
        }
        // date range restricts missing everywhere
        #region 1DownloadLoc - working (except date range)
        // POST: api/reports/downloadLoc
        [HttpPost("downloadloc")]
        public DownloadLocReport GetDownloadLocReport([FromBody] ReportModel repModel)
        {
            try
            {
                //System.Diagnostics.Debugger.Break();
                DownloadLocReport report = new DownloadLocReport();

                //TODO: restrict report to dates provided
                report.StartDate = repModel.StartDate;
                report.EndDate = repModel.EndDate;
                report.EmpFullName = _context.Employee
                                                .FirstOrDefault(e => e.EmployeeId == repModel.EmployeeId)
                                                .getFullName();

                List<DownloadReportLine> lines = new List<DownloadReportLine>();
                //var cities = _context.City.Include(c => c.DownloadLocation).Include(c => c.Province);
                foreach (City city in _context.City.Include(c => c.DownloadLocation).Include(c => c.Province))
                {
                    DownloadReportLine line = new DownloadReportLine();
                    line.totalDownloads = city.DownloadLocation.Count();
                    if(line.totalDownloads > 0)
                    {
                        line.province = city.Province.ProvinceName;
                        line.city = city.CityName;
                        lines.Add(line);
                        report.TotalDownloads += line.totalDownloads;
                    }
                }


                //System.Diagnostics.Debugger.Break();
                lines.Sort((x, y) => x.province.CompareTo(y.province));
                report.Lines = lines;
                //System.Diagnostics.Debugger.Break();
                return report;
            }
            catch (Exception)
            {
                System.Diagnostics.Debugger.Break();
                return new DownloadLocReport();
            }
        }
        #endregion

        #region 3PackageType - report & maybe graph
        // POST: api/reports/packagetype
        [HttpPost("packagetype")]
        public PackageTypeReport GetPackageTypeReport([FromBody] EmpModel empModel)
        {
            try
            {
                //System.Diagnostics.Debugger.Break();
                PackageTypeReport report = new PackageTypeReport();

                report.EmpFullName = _context.Employee
                                                .FirstOrDefault(e => e.EmployeeId == empModel.EmployeeId)
                                                .getFullName();

                List<PackageReportLine> lines = new List<PackageReportLine>();

                //for each package in Package table, check the packageTypeId and add to the total sent for that package type
                foreach (PackageType packageType in _context.PackageType.Include(p => p.Package))
                {
                    PackageReportLine line = new PackageReportLine();
                    line.packType = packageType.PackageTypeDescr;
                    line.totalSent = packageType.Package.Count();
                    lines.Add(line);
                    report.TotalSent += line.totalSent;
                }
                lines.Sort((y, x) => x.totalSent.CompareTo(y.totalSent));
                report.Lines = lines;
                //System.Diagnostics.Debugger.Break();
                return report;
            }
            catch (Exception)
            {
                System.Diagnostics.Debugger.Break();
                return new PackageTypeReport();
            }
        }
        #endregion

        #region 4ShipmentDuration
        // POST: api/reports/shipmentduration
        [HttpPost("shipmentduration")]
        public ShipmentDurationReport GetShipmentDurationReport([FromBody] EmpModel empModel)
        {
            try
            {
                //System.Diagnostics.Debugger.Break();
                ShipmentDurationReport report = new ShipmentDurationReport();

                report.EmpFullName = _context.Employee
                                                .FirstOrDefault(e => e.EmployeeId == empModel.EmployeeId)
                                                .getFullName();

                List<DurationReportLine> lines = new List<DurationReportLine>();
                foreach (ShipmentAgent agent in _context.ShipmentAgent.Include(s => s.Shipment))
                {
                    DurationReportLine line = new DurationReportLine();
                    line.agentName = _context.ShipmentAgent
                                                        .FirstOrDefault(a => a.AgentId == agent.AgentId)
                                                        .getFullName();
                    line.noOfShipments = agent.Shipment.Count;
                    if (line.noOfShipments > 0)
                    {
                        //System.Diagnostics.Debugger.Break();
                        line.totalDuration = new TimeSpan();
                        foreach (var shipment in agent.Shipment)
                        {
                            //System.Diagnostics.Debugger.Break();
                            var duration = shipment.DeliveryTime - shipment.CollectionTime;
                            line.totalDuration += duration;
                        }

                        line.avgDuration = (line.totalDuration / line.noOfShipments).ToString();
                        lines.Add(line);
                    }
                }

                report.Lines = lines;
                //System.Diagnostics.Debugger.Break();
                return report;
            }
            catch (Exception)
            {
                //System.Diagnostics.Debugger.Break();
                return new ShipmentDurationReport();
            }
        }
        #endregion
        
        #region 5DailyShipments - graph (only this graph is necessary) //not working yet
        /*
        // POST: api/reports/dailyshipments
        [HttpPost("dailyshipments")]
        public DailyShipmentsReport GetDailyShipmentsReport([FromBody] ReportModel repModel)
        {
            try
            {
                System.Diagnostics.Debugger.Break();
                DailyShipmentsReport report = new DailyShipmentsReport();

                //TODO: restrict report to dates provided   
                report.StartDate = repModel.StartDate;
                report.EndDate = repModel.EndDate;
                report.EmpFullName = _context.Employee
                                                .FirstOrDefault(e => e.EmployeeId == repModel.EmployeeId)
                                                .getFullName();

                List<DailyShipmentsReportLine> lines = new List<DailyShipmentsReportLine>();
                
                //only check the days in the range given
                //for each day in the date range given, count the number of shipments completed
                foreach (Shipment shipment in _context.Shipment)
                {
                    DailyShipmentsReportLine line = new DailyShipmentsReportLine();
                    
                    lines.Add(line);
                    
                }
                report.Lines = lines;
                System.Diagnostics.Debugger.Break();
                return report;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                return new DailyShipmentsReport();
            }
        }
        */
        #endregion 

        #region 6AgentSalary
        //similar to average shipment duration report?
        // POST: api/reports/salary
        [HttpPost("salary")]
        public AgentSalaryReport GetAgentSalaryReport([FromBody] ReportModel repModel)
        {
            try
            {
                //System.Diagnostics.Debugger.Break();
                AgentSalaryReport report = new AgentSalaryReport();

                //TODO: restrict report to dates provided
                report.StartDate = repModel.StartDate;
                report.EndDate = repModel.EndDate;
                report.EmpFullName = _context.Employee
                                                .FirstOrDefault(e => e.EmployeeId == repModel.EmployeeId)
                                                .getFullName();


                //for each agent in the Agent table, calculate their salary
                //find shipments with the "selected" agent ID and add the totalCost of all the agent's shipments
                //multiply the total cost the agent's shipments with the fixed agent salary % rate
            
                List<SalaryReportLine> lines = new List<SalaryReportLine>();
                foreach (ShipmentAgent agent in _context.ShipmentAgent.Include(s=>s.Shipment))
                {
                    SalaryReportLine line = new SalaryReportLine();
                    line.agentName = _context.ShipmentAgent
                                                          .FirstOrDefault(a => a.AgentId == agent.AgentId)
                                                          .getFullName();
                    line.bankDetails = _context.ShipmentAgent
                                                          .FirstOrDefault(a => a.AgentId == agent.AgentId)
                                                          .getBankDetails();

                    line.noOfShipments = agent.Shipment.Count();
                    if (line.noOfShipments > 0)
                    {
                        foreach (var shipment in agent.Shipment)
                        {
                            decimal totalCost = 0;
                            if (shipment.Paid == 0)
                            {
                                 totalCost += shipment.TotalCost;
                            }
                            line.agentSalary = Decimal.Multiply(totalCost, _context.FixedPrice.FirstOrDefault(f => f.FixedPriceId == 6).FixedPrice1);
                        }
                    report.TotalSalary += line.agentSalary;
                    lines.Add(line);
                    }
                }
                report.Lines = lines;
                //System.Diagnostics.Debugger.Break();
                return report;
            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debugger.Break();
                return new AgentSalaryReport();
            }
        }
        
        #endregion
   
        #region 7Income
            
        // POST: api/reports/income
        [HttpPost("income")]
        public IncomeReport GetIncomeReport([FromBody] ReportModel repModel)
        {
            try
            {
                //System.Diagnostics.Debugger.Break();
                IncomeReport report = new IncomeReport();

                //TODO: restrict report to dates provided
                report.StartDate = repModel.StartDate;
                report.EndDate = repModel.EndDate;
                report.EmpFullName = _context.Employee
                                                .FirstOrDefault(e => e.EmployeeId == repModel.EmployeeId)
                                                .getFullName();

                
                //total income from shipments = add all totalCost for the selected period (where Paid = 1)
                foreach (Shipment shipment in _context.Shipment)
                {
                    if(shipment.Paid == 1)
                    {
                        report.ShipmentIncome += shipment.TotalCost;
                    }
                }
                
                //total income from penalties = add all penaltyAmount for the selected period (where DatePaid != null)
                foreach (Penalty penalty in _context.Penalty)
                {
                    if (penalty.DatePaid != null)
                    {
                        report.PenaltyIncome += penalty.PenaltyAmount;
                    }
                }
                
                //System.Diagnostics.Debugger.Break();

                //total income received = shipments + penalties
                report.TotalIncome = report.ShipmentIncome + report.PenaltyIncome;

                //payments to agents = total income * agent income % (fixed price)
                //Curt needs to read this into brackets on the HTML
                
                report.AgentPayments = Decimal.Multiply(report.ShipmentIncome, _context.FixedPrice.FirstOrDefault(f => f.FixedPriceId == 6).FixedPrice1);
                
                //profit/loss
                report.ProfitLoss = report.TotalIncome - report.AgentPayments;

                if (report.ProfitLoss > 0)
                {
                    report.isProfit = true;
                }
                else
                    report.isProfit = false;

                //System.Diagnostics.Debugger.Break();

                return report;
            }
            catch (Exception)
            {
                System.Diagnostics.Debugger.Break();
                return new IncomeReport();
            }
        }
        
        #endregion
    
        #region 2PopularArea - not gonna happen
        #endregion
        #endregion
    }
}
