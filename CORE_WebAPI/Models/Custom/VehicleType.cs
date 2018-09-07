using System;
using System.Collections.Generic;

namespace CORE_WebAPI.Models
{
    public partial class VehicleType
    {
        public void UpdateChangedFields(VehicleType vehicleType)
        {
            if (vehicleType.VehicleTypeDescr != null)
            {
                this.VehicleTypeDescr = vehicleType.VehicleTypeDescr;
            }
        
         }
    }
}
