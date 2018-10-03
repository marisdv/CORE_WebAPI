using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class PaymentReference
    {
        public int TransactionId { get; set; }
        public string StatusName { get; set; }
        public int TxStatusCode { get; set; }
        public string TxStatusDescr { get; set; }
        public string ResultCode { get; set; }
        public string ResultDescr { get; set; }
        public decimal Amount { get; set; }
        public string RiskIndicator { get; set; }
        public string PaymentTypeMethod { get; set; }
        public string PaymentTypeDetail { get; set; }
        public int ShipmentId { get; set; }
        public DateTime TxDateTime { get; set; }

        public Shipment Shipment { get; set; }
    }
}
