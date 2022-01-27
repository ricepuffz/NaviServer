using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NaviServer.Models
{
    public partial class Coordinates
    {
        public uint CoordinatesId { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }

        public virtual Planet Planet { get; set; }
        public virtual Player Player { get; set; }
    }
}
