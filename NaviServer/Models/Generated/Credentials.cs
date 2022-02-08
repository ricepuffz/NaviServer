using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NaviServer.Models
{
    public partial class Credentials
    {
        public uint PlayerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual Player Player { get; set; }
    }
}
