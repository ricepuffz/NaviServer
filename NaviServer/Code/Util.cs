using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using NaviServer.Models;

namespace NaviServer.Code
{
    public static class Util
    {
        public static string DBConnectionString()
        {
            return System.IO.File.ReadAllText(Environment.GetEnvironmentVariable("DB_CON_STR_LOC"));
        }

        public static float MovementDistance(Movement movement)
        {
            Coordinates fromCoord = movement.CoordinatesFrom;
            Coordinates toCoord = movement.CoordinatesTo;

            Vector2 from = new Vector2(fromCoord.PosX, fromCoord.PosY);
            Vector2 to = new Vector2(toCoord.PosX, toCoord.PosY);
            return (to - from).Length();
        }
    }
}
