using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NaviServer.Models
{
    public partial class Player
    {
        public uint PlayerId { get; set; }
        public ulong IsAdmin { get; set; }
        public uint ShipId { get; set; }

        public virtual Ship Ship { get; set; }
        public virtual Credentials Credentials { get; set; }
    }
}
