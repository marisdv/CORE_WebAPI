using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class SenderGrid
    {
        public int totalCount { get; set; }
        public IEnumerable<Sender> senders { get; set; }
    }
}
