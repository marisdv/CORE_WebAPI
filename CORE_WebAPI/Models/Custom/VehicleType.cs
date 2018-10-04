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

            if (vehicleType.VehiclePacakageLine.Count > 0)
            {
                foreach (var pack in vehicleType.VehiclePacakageLine)
                {
                    if (pack.PackageTypeId != 0 && pack.VehicleTypeId != 0)
                    {
                        this.VehiclePacakageLine.Add(pack);
                    }
                }
            }
        }
    }
}
