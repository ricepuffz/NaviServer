using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NaviServer.Models
{
    public partial class Movement
    {
        public uint MovementId { get; set; }
        public uint ShipId { get; set; }
        public uint CoordinatesFromId { get; set; }
        public uint CoordinatesToId { get; set; }

        public virtual Coordinates CoordinatesFrom { get; set; }
        public virtual Coordinates CoordinatesTo { get; set; }
        public virtual Ship Ship { get; set; }
    }
}
