using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_WebAPI.Models.Custom
{
    public class Transaction
    {

        public string Payment()
        {
            Payhost.SinglePaymentRequest1 payment = new Payhost.SinglePaymentRequest1();
            Payhost.CardPaymentRequestType request = new Payhost.CardPaymentRequestType();

            request.Account = new Payhost.PayGateAccountType();
            request.Account.PayGateId = "10011064270";
            request.Account.Password = "test";

            string name = "Marissa";
            string surname = "de Villiers";
            string phone = "0796861912";
            string email = "marissadev@gmail.com";

            request.Customer = new Payhost.PersonType();
            request.Customer.FirstName = name;
            request.Customer.LastName = surname;
            request.Customer.Mobile = new string[] { phone };
            request.Customer.Email = new string[] { email };

            request.ItemsElementName = new Payhost.ItemsChoiceType[]
            {
                Payhost.ItemsChoiceType.CardNumber,
                Payhost.ItemsChoiceType.CardExpiryDate
            };

            string cardNo = "4000000000000002";
            string date = "012020";
            string cvv = "001";
            string budget = "0";

            int id = 3;
            int amt = 4000; //R40 //remove comma - payhost format

            request.Items = new string[] { cardNo, date };

            request.CVV = cvv;
            request.BudgetPeriod = budget;

            request.Order = new Payhost.OrderType();
            request.Order.MerchantOrderId = id.ToString(); ;//shipmentID (indicate that it's a penalty?)
            request.Order.Currency = Payhost.CurrencyType.ZAR;
            request.Order.Amount = amt;

            payment.SinglePaymentRequest = new Payhost.SinglePaymentRequest();
            payment.SinglePaymentRequest.Item = request;

            Payhost.PayHOST paygateInterface = new Payhost.PayHOSTClient();

            //SinglePaymentResponse1 response = paygateInterface.SinglePaymentAsync(payment);

            //var r = response.SinglePaymentResponse.Item as Payhost.CardPaymentResponseType;

            ////error handling
            //if (r.Status.StatusName.ToString() == "ValidationError")
            //{
            //    var lastResponse = r.StatusDetail;
            //}

            //var status = r.Status as Payhost.StatusType;
            //var redirect = r.Redirect as Payhost.RedirectResponseType;

            //return status.TransactionStatusDescription.ToString();'
            return "This does not work yet;";
        }
    }
}
