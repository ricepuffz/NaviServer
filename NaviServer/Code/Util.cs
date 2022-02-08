using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
using NaviServer.Models;
using System.Security.Cryptography;
using System.Text;

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

        public static string HashSHA256(string input)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
