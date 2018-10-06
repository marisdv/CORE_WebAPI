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
            //the mail does not work on server??

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
            //outMail.Attachment attachment = new Attachment("file");
        }
        #endregion

        #region PAYMENT
        public struct CardDetailsModel
        {
            //update this in the API
            public int senderId { get; set; }
            public int transactId { get; set; } //or just transactionId -> penalty or payment
            public string cardNo { get; set; }
            public string expDate { get; set; }
            public string cvv { get; set; }
            public int type { get; set; }
        }

        //POST: api/Utility/payment
        [HttpPost("Payment")]
        public IActionResult Payment([FromBody] CardDetailsModel details)
        {
            Sender sender = new Sender();
            Penalty penalty = new Penalty();
            Shipment shipment = new Shipment();
            PaymentReference payRef = new PaymentReference();
            AuditLog log = new AuditLog();

            DateTime now = new DateTime();
            now = DateTime.Now;
            try
            {
                //System.Diagnostics.Debugger.Break();

                Payhost.SinglePaymentRequest1 payment = new Payhost.SinglePaymentRequest1();
                Payhost.CardPaymentRequestType request = new Payhost.CardPaymentRequestType();

                request.Account = new Payhost.PayGateAccountType();
                request.Account.PayGateId = "10011064270";
                request.Account.Password = "test";

                //get Sender details
                
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
                
                request.Order = new Payhost.OrderType();

                string orderId;
                decimal amount;

                if (details.type == 0)
                {
                    //SHIPMENT
                    //System.Diagnostics.Debugger.Break();
                    //create only shipment
                    shipment = _context.Shipment
                                                .FirstOrDefault(s => s.ShipmentId == details.transactId);

                    orderId = shipment.ShipmentId.ToString();
                    amount = shipment.TotalCost;
                }
                else
                {
                    //PENALTY
                    //System.Diagnostics.Debugger.Break();
                    //create penalty
                    penalty = _context.Penalty
                                              .FirstOrDefault(m => m.PentaltyId == details.transactId);
                    //create shipment
                    shipment = _context.Shipment
                                                .FirstOrDefault(s => s.ShipmentId == penalty.ShipmentId);

                    orderId = penalty.ShipmentId.ToString();
                    amount = penalty.PenaltyAmount;
                }

                if (amount <= 0)
                {
                    return BadRequest();
                }

                request.Order.MerchantOrderId = orderId;
                request.Order.Currency = Payhost.CurrencyType.ZAR;
                request.Order.Amount = Convert.ToInt32((amount * 100));

                payment.SinglePaymentRequest = new Payhost.SinglePaymentRequest();
                payment.SinglePaymentRequest.Item = request;

                Payhost.PayHOST paygateInterface = new Payhost.PayHOSTClient();
                
                //System.Diagnostics.Debugger.Break();

                Payhost.SinglePaymentResponse1 response = paygateInterface.SinglePayment(payment);

                var r = response.SinglePaymentResponse.Item as Payhost.CardPaymentResponseType;

                //System.Diagnostics.Debugger.Break();
                
                //error handling - not sure where this should fit in?
                if (r.Status.StatusName.ToString() == "ValidationError")
                {
                    //var lastResponse = r.Status.StatusDetail;
                    return BadRequest();
                }

                var status = r.Status as Payhost.StatusType;
                var redirect = r.Redirect as Payhost.RedirectResponseType;

                System.Diagnostics.Debugger.Break();

                //PAYMENT RESPONSE
                
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

                if (details.type == 0)
                {
                    //SHIPMENT
                    payRef.ShipmentId = shipment.ShipmentId; //this should only save the shipment ID
                }
                else
                {
                    //PENALTY
                    payRef.ShipmentId = penalty.ShipmentId;
                }

                payRef.TxDateTime = now;

                //System.Diagnostics.Debugger.Break();
                
                _context.PaymentReference.Add(payRef);
                _context.SaveChangesAsync();

                //System.Diagnostics.Debugger.Break();

                
                if (r.Status.ResultCode == "990017")
                {
                    if (details.type == 0)
                    {
                        //SHIPMENT
                        //System.Diagnostics.Debugger.Break();

                        //save shipment
                        shipment.Paid = 1;
                        shipment.ShipmentStatusId = 3;
                        _context.Entry(shipment).State = EntityState.Modified;
                        _context.SaveChangesAsync();

                        //AUDIT LOG - shipment payment
                        log.AuditUserName = sender.getFullName();
                        log.UserTypeId = 1;
                        log.ItemAffected = "Sender: Payment successfully made for shipment requested on " 
                            + shipment.ShipmentDate.ToLongDateString() + ". " + "ShipmentID: " + shipment.ShipmentId.ToString();
                        log.AuditTypeId = 5;
                        log.TxAmount = amount;
                        log.AuditDateTime = now;

                        _context.AuditLog.Add(log);
                        _context.SaveChangesAsync();
                        
                        
                        //send shipment paid email
                        string content = "Good day " + sender.getFullName() + ",\n\n" +
                                         "The payment for your Shipment has been received.\n\n" +
                                         //shipment details?
                                         "Amount: R" + amount.ToString() +
                                         "\nDate & Time: " + now.ToString() +
                                         "\n\nKind regards,\nThe Project CAL Team";
                        
                        sendMail(sender.SenderEmail, content, "Project CAL: Shipment Invoice");
                        

                        return Ok();
                    }
                    else
                    {
                        //PENALTY
                        //System.Diagnostics.Debugger.Break();
                        
                        //save penalty
                        penalty.DatePaid = now;
                        _context.Entry(penalty).State = EntityState.Modified;
                        _context.SaveChangesAsync();

                        //AUDIT LOG - penalty payment
                        log.AuditUserName = sender.getFullName();
                        log.UserTypeId = 1;
                        log.ItemAffected = "Sender: Penalty paid for shipment delivered on " + shipment.ShipmentDate + " at " + shipment.DeliveryTime.ToString() + ". " + "ShipmentID: " + shipment.ShipmentId.ToString();
                        log.AuditTypeId = 5;
                        log.TxAmount = amount;
                        log.AuditDateTime = now;

                        _context.AuditLog.Add(log);
                        _context.SaveChangesAsync();

                        //FAILURE TO SEND MAIL
                        //what kind of response does the mail server send?
                        
                        //send penalty paid email
                        string content = "Good day " + sender.getFullName() + ",\n\n" +
                                         "The payment for your Shipment Penalty has been received.\n\n" +
                                         //shipment & penalty details?
                                         "Amount: R" + amount.ToString() +
                                         "\nDate & Time: " + now.ToString() +
                                         "\n\nKind regards,\nThe Project CAL Team";

                        sendMail(sender.SenderEmail, content, "Project CAL: Shipment Penalty Invoice");
                        

                        return Ok();
                    }
                }
                else
                {
                    //check that penalty audit log entries aren't swapped...
                    //AUDIT LOG - payment failed
                    log.AuditUserName = sender.getFullName();
                    log.UserTypeId = 1;
                    if (details.type == 0)
                    {
                        log.ItemAffected = "Sender: Payment declined for shipment requested on " + shipment.ShipmentDate.ToLongDateString() + ". " + "ShipmentID: " + shipment.ShipmentId.ToString();
                            
                    }
                    else
                    {
                        log.ItemAffected = "Sender: Penalty payment declined for shipment delivered on " + shipment.ShipmentDate + " at " + shipment.DeliveryTime.ToString() + ". " + "ShipmentID: " + shipment.ShipmentId.ToString();
                    }
                    log.AuditTypeId = 6;
                    log.TxAmount = null;
                    log.AuditDateTime = now;

                    _context.AuditLog.Add(log);
                    _context.SaveChangesAsync();

                    return BadRequest();
                }
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debugger.Break();

                log.AuditUserName = sender.getFullName();
                log.UserTypeId = 1;
                //payment went through by the time we get here... - that should not happen

                log.ItemAffected = "Sender: Email failed to send. " + ex.Message;
                log.AuditTypeId = 5;
                log.TxAmount = null;
                log.AuditDateTime = now;

                _context.AuditLog.Add(log);
                _context.SaveChangesAsync();

                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
