﻿using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Sender
    {
        public Sender()
        {
            Shipment = new HashSet<Shipment>();
        }

        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderNationalId { get; set; }
        public string SenderPassportNo { get; set; }
        public string SenderEmail { get; set; }
        public byte SenderActive { get; set; }
        public int AccessRoleId { get; set; }
        public int LoginId { get; set; }

        public AccessRole AccessRole { get; set; }
        public Login Login { get; set; }
        public ICollection<Shipment> Shipment { get; set; }
    }
}
