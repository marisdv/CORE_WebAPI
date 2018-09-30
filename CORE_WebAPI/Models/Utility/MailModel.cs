using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CORE_WebAPI.Models.Custom
{
    public class MailModel
    {
        public string Subject {get;set;}
        public string MailBody { get; set; }
        public string ReceiverMailAddress { get; set; }
    }
}
