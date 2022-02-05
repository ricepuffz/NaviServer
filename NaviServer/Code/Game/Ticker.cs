using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;

namespace NaviServer.Code.Game
{
    public static class Ticker
    {
        private static DateTime s_lastTickTime = DateTime.Now;
        private static readonly double s_tickDelayInSeconds = 2;
        private static Timer s_timer;

        public static bool Running { get; private set; } = false;
        public static double SecondsSinceLastTick { get { return (DateTime.Now - s_lastTickTime).TotalSeconds; } }

        public static void Run()
        {
            s_timer = new Timer(s_tickDelayInSeconds * 1000);
            s_timer.Elapsed += (source, e) =>
            {
                Tick();
            };
            s_timer.AutoReset = true;
            s_timer.Enabled = true;
        }

        public static void Stop()
        {
            s_timer.Stop();
            s_timer.Dispose();
        }

        private static void Tick()
        {
            MovementTicker.Tick();
            s_lastTickTime = DateTime.Now;
        }
    }
}
