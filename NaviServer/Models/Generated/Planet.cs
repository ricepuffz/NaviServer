using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NaviServer.Models
{
    public partial class Planet
    {
        public uint PlanetId { get; set; }
        public uint CoordinatesId { get; set; }
        public string Name { get; set; }

        public virtual Coordinates Coordinates { get; set; }
    }
}
