using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaviServer.Code.ControllerPOD
{
    public class ShipMovement
    {
        public uint ShipID { get; set; }
        public float TargetX { get; set; }
        public float TargetY { get; set; }
    }
}
