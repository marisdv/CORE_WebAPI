using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using CORE_WebAPI.Models;
using CORE_WebAPI.Models.Custom;
using CORE_WebAPI.Models.Reports;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CORE_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [EnableCors("MyPolicy")]
    public class UtilityController : ControllerBase
    {
        private readonly ProjectCALServerContext _context;

        public UtilityController(ProjectCALServerContext context)
        {
            _context = context;
        }

        //// GET: api/Utility
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Utility/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Utility/image
        //[HttpPost]
        //public void Post([FromBody] string image)
        //{

        //    byte[] test = Convert.FromBase64String(image);
        //}

        #region IMAGE
        
        #endregion

        #region EMAIL
        // POST: api/Utility/TestMail
        [HttpPost("TestMail")]
        public string TestMail([FromBody] MailModel mail)
        {
            //mail by shipment ID
            //API gathers sender & shipment & costing info from the db
            try
            {
                sendMail(mail.ReceiverMailAddress,mail.MailBody,mail.Subject);
                return "Sent";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        public static void sendMail(string destination, string content, string subject)
        {
            MailMessage outMail = new MailMessage();
            outMail.To.Add(destination);
            outMail.From = new MailAddress("projectcal@aurum.net.za");
            outMail.Subject = subject;
            outMail.Body = content;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail.aurum.net.za";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("projectcal@aurum.net.za", "Aurum370!");
            smtp.Send(outMail);

            //figure out mail attachment code
            //outMail.Attachment attachment = new Attachment("filee"); ;
            
        }
        #endregion

        #region PAYMENT
        public struct CardDetailsModel
        {
            public int senderId { get; set; }
            public int penaltyId { get; set; } //or just transactionId -> penalty or payment
            //boolean value for whether it is a penalty or a payment
            public string cardNo { get; set; } //check format for this
            public string expDate { get; set; } //check format for this
            public string cvv { get; set; }
        }

        //POST: api/Utility/payment
        [HttpPost("Payment")]
        public string Payment([FromBody] CardDetailsModel details)
        {
        try
            {
                //System.Diagnostics.Debugger.Break();

                Payhost.SinglePaymentRequest1 payment = new Payhost.SinglePaymentRequest1();
                Payhost.CardPaymentRequestType request = new Payhost.CardPaymentRequestType();

                request.Account = new Payhost.PayGateAccountType();
                request.Account.PayGateId = "10011064270";
                request.Account.Password = "test";

                //get Sender details
                Sender sender = new Sender();
                sender = _context.Sender.Include(l => l.Login)
                                        .FirstOrDefault(m => m.SenderId == details.senderId);

                request.Customer = new Payhost.PersonType();
                request.Customer.FirstName = sender.SenderName;
                request.Customer.LastName = sender.SenderSurname;
                request.Customer.Mobile = new string[] { sender.Login.PhoneNo };
                request.Customer.Email = new string[] { sender.SenderEmail };

                //System.Diagnostics.Debugger.Break();
                request.ItemsElementName = new Payhost.ItemsChoiceType[]
                {
                        Payhost.ItemsChoiceType.CardNumber,
                        Payhost.ItemsChoiceType.CardExpiryDate
                };
                
                request.Items = new string[] { details.cardNo, details.expDate };

                request.CVV = details.cvv;
                request.BudgetPeriod = "0";

                //if-statement for penalty or shipment

                //System.Diagnostics.Debugger.Break();
                Penalty penalty = new Penalty();
                penalty = _context.Penalty
                                        .FirstOrDefault(m => m.PentaltyId == details.penaltyId);
                
                request.Order = new Payhost.OrderType();
                request.Order.MerchantOrderId = penalty.PentaltyId.ToString(); //shipmentId or penaltyId
                request.Order.Currency = Payhost.CurrencyType.ZAR;
                request.Order.Amount = Convert.ToInt32((penalty.PenaltyAmount*100));

                payment.SinglePaymentRequest = new Payhost.SinglePaymentRequest();
                payment.SinglePaymentRequest.Item = request;

                Payhost.PayHOST paygateInterface = new Payhost.PayHOSTClient();

                Payhost.SinglePaymentResponse1 response = paygateInterface.SinglePayment(payment);

                var r = response.SinglePaymentResponse.Item as Payhost.CardPaymentResponseType;

                //System.Diagnostics.Debugger.Break();
                
                //error handling
                if (r.Status.StatusName.ToString() == "ValidationError")
                {
                    var lastResponse = r.Status.StatusDetail;
                }

                var status = r.Status as Payhost.StatusType;
                var redirect = r.Redirect as Payhost.RedirectResponseType;

                //store response in database
                PaymentReference payRef = new PaymentReference();
                payRef.TransactionId = Convert.ToInt32(r.Status.TransactionId);
                payRef.StatusName = r.Status.StatusName.ToString();
                payRef.TxStatusCode = Convert.ToInt32(r.Status.TransactionStatusCode);
                payRef.TxStatusDescr = r.Status.TransactionStatusDescription;
                payRef.ResultCode = r.Status.ResultCode;
                payRef.ResultDescr = r.Status.ResultDescription;
                payRef.Amount = r.Status.Amount/100;
                payRef.RiskIndicator = r.Status.RiskIndicator;
                payRef.PaymentTypeMethod = r.Status.PaymentType.Method.ToString();
                payRef.PaymentTypeDetail = r.Status.PaymentType.Detail.ToString();
                payRef.ShipmentId = penalty.ShipmentId;
                payRef.TxDateTime = DateTime.Now;

                //System.Diagnostics.Debugger.Break();

                _context.PaymentReference.Add(payRef);
                
                penalty.DatePaid = DateTime.Now;
                _context.Entry(penalty).State = EntityState.Modified;

                //change Paid attribute in Shipment table to 1

                _context.SaveChangesAsync();
                return status.TransactionStatusDescription.ToString();

                //NB - ERROR HANDLING TO SEND RESPONSE TO APP
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        
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
        #region 1DownloadLoc - working (except date range)
        // POST: api/reports/downloadLoc
        [HttpPost("reports/downloadloc")]
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
                    line.province = city.Province.ProvinceName;
                    line.city = city.CityName;
                    lines.Add(line);
                    report.TotalDownloads += line.totalDownloads;
                }
                //System.Diagnostics.Debugger.Break();
                lines.Sort((x,y) => x.province.CompareTo(y.province));
                report.Lines = lines;
                //System.Diagnostics.Debugger.Break();
                return report;
            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debugger.Break();
                return new DownloadLocReport();
            }
        }
        #endregion

        #region 3PackageType - report & maybe graph
        // POST: api/reports/packagetype
        [HttpPost("reports/packagetype")]
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
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                return new PackageTypeReport();
            }
        }
        #endregion
    
        #region 4ShipmentDuration
        // POST: api/reports/shipmentduration
        [HttpPost("reports/shipmentduration")]
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
            catch (Exception ex)
            {
                //System.Diagnostics.Debugger.Break();
                return new ShipmentDurationReport();
            }
        }
        #endregion
        /*
        #region 5DailyShipments - graph (only this graph is necessary)
        // POST: api/reports/dailyshipments
        [HttpPost("reports/dailyshipments")]
        public DailyShipmentsReport GetDailyShipmentsReport([FromBody] ReportModel repModel)
        {
            try
            {
                System.Diagnostics.Debugger.Break();
                DailyShipmentsReport report = new DailyShipmentsReport();

                
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
        #endregion 

        #region 6AgentSalary
        // POST: api/reports/salary
        [HttpPost("reports/salary")]
        public AgentSalaryReport GetAgentSalaryReport([FromBody] ReportModel repModel)
        {
            try
            {
                System.Diagnostics.Debugger.Break();
                AgentSalaryReport report = new AgentSalaryReport();

                //TODO: restrict report to dates provided
                report.StartDate = repModel.StartDate;
                report.EndDate = repModel.EndDate;
                report.EmpFullName = _context.Employee
                                                .FirstOrDefault(e => e.EmployeeId == repModel.EmployeeId)
                                                .getFullName();

                List<SalaryReportLine> lines = new List<SalaryReportLine>();

                //for each agent in the Agent table, calculate their salary
                //find shipments with the "selected" agent ID and add the totalCost of all the agent's shipments
                //multiply the total cost the agent's shipments with the fixed agent salary % rate

                foreach ()
                {
                    //also 2 loops?
                    SalaryReportLine line = new SalaryReportLine();
                    
                    lines.Add(line);
                }
                report.Lines = lines;
                System.Diagnostics.Debugger.Break();
                return report;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                return new AgentSalaryReport();
            }
        }
        #endregion

        #region 7Income
        // POST: api/reports/downloadLoc
        [HttpPost("reports/downloadloc")]
        public IncomeReport GetIncomeReport([FromBody] ReportModel repModel)
        {
            try
            {
                System.Diagnostics.Debugger.Break();
                IncomeReport report = new IncomeReport();

                //TODO: restrict report to dates provided
                report.StartDate = repModel.StartDate;
                report.EndDate = repModel.EndDate;
                report.EmpFullName = _context.Employee
                                                .FirstOrDefault(e => e.EmployeeId == repModel.EmployeeId)
                                                .getFullName();

                //total income from shipments = add all totalCost for the selected period

                //total income from penalties = add all penaltyAmount for the selected period
                
                //total income received = shipments + penalties

                //payments to agents = total income * agent income %

                System.Diagnostics.Debugger.Break();
                return report;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                return new IncomeReport();
            }
        }
        #endregion

        #region 2PopularArea - not gonna happen
        #endregion
    */
        #endregion
    }
}
