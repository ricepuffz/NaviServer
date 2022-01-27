using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NaviServer.Code
{
    public static class Util
    {
        public static string DBConnectionString()
        {
            return System.IO.File.ReadAllText(Environment.GetEnvironmentVariable("DB_CON_STR_LOC"));
        }
    }
}
