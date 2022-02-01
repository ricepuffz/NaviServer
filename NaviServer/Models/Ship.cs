using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NaviServer.Models
{
    public partial class Ship
    {
        public uint ShipId { get; set; }
        public uint CoordinatesId { get; set; }

        public virtual Coordinates Coordinates { get; set; }
        public virtual Movement Movement { get; set; }
        public virtual Player Player { get; set; }
    }
}
