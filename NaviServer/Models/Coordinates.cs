using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace NaviServer.Models
{
    public partial class Coordinates
    {
        public Coordinates()
        {
            MovementCoordinatesFrom = new HashSet<Movement>();
            MovementCoordinatesTo = new HashSet<Movement>();
            Ship = new HashSet<Ship>();
        }

        public uint CoordinatesId { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }

        public virtual Planet Planet { get; set; }
        public virtual ICollection<Movement> MovementCoordinatesFrom { get; set; }
        public virtual ICollection<Movement> MovementCoordinatesTo { get; set; }
        public virtual ICollection<Ship> Ship { get; set; }
    }
}
