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

        }
        #endregion

        #region PAYMENT
        //// POST: api/Utility/payment
        //[HttpPost("Payment")]
        //public string Payment()//([FromBody] Transaction tx)
        //{
        //    //accept cardNo, CVV, expDate and ShipmentID (penalty) from app
        //    try
        //    {
        //        //return tx.Payment();
        //        return Payment();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        #endregion
        public struct JsonModel
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public int EmployeeId { get; set; }
        }
        #region REPORTS
        //should it be a GET or a POST?
        // GET: api/reports/downloadLoc
        [HttpPost("reports/downloadloc")]
        public DownloadLocReport GetDownloadLocReport([FromBody] JsonModel searchModel)
        {
            try
            {
                System.Diagnostics.Debugger.Break();
                DownloadLocReport report = new DownloadLocReport();

                report.StartDate = searchModel.StartDate;
                report.EndDate = searchModel.EndDate;
                report.EmpFullName = _context.Employee
                                                .FirstOrDefault(e => e.EmployeeId == searchModel.EmployeeId)
                                                .getFullName();

                List<ReportLine> lines = new List<ReportLine>();
                //var cities = _context.City.Include(c => c.DownloadLocation).Include(c => c.Province);
                foreach (City city in _context.City.Include(c => c.DownloadLocation).Include(c => c.Province))
                {
                    ReportLine line = new ReportLine();
                    line.totalDownloads = city.DownloadLocation.Count();
                    line.province = city.Province.ProvinceName;
                    line.city = city.CityName;
                    lines.Add(line);
                    report.TotalDownloads += line.totalDownloads;
                }
                report.Lines = lines;
                System.Diagnostics.Debugger.Break();
                return report;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();
                return new DownloadLocReport();
            }
        }

        // POST: api/Utility/reports/income
        //[HttpPost("reports/income")]
        //public void PostIncomeReport([FromBody] string empId, DateTime start, DateTime end)
        //{
        //    try
        //    {

        //        GenerateIncomeReport();
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}


        #endregion
    }
}
