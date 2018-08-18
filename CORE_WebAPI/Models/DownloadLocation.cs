using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class DownloadLocation
    {
        public int DownloadId { get; set; }
        public DateTime DownloadDateTime { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }
    }
}
