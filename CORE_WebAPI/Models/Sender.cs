using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Sender
    {
        public Sender()
        {
            Basket = new HashSet<Basket>();
        }

        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderNationalId { get; set; }
        public string SenderPassportNo { get; set; }
        public string SenderEmail { get; set; }
        public byte SenderActive { get; set; }
        public int LoginId { get; set; }

        public Login Login { get; set; }
        public ICollection<Basket> Basket { get; set; }
    }
}
