using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class Penalty
    {
        public void UpdateChangedFields(Penalty penalty)
        {
            if (penalty.DateCharged != null && penalty.DateCharged != new DateTime())
            {
                this.DateCharged = penalty.DateCharged;
            }
            if (penalty.DatePaid != null && penalty.DatePaid != new DateTime())
            {
                this.DatePaid = penalty.DatePaid;
            }
            if (penalty.PenaltyAmount != 0)
            {
                this.PenaltyAmount = penalty.PenaltyAmount;
            }
            if (penalty.ShipmentId != 0)
            {
                this.ShipmentId = penalty.ShipmentId;
            }
        }
    }
}
