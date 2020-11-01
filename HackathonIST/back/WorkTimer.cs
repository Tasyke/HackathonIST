using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HackathonIST.back
{
    
    class WorkTimer
    {
        BuilderStart Build = new BuilderStart();
        Stopwatch stopwatch = new Stopwatch();
        string Time;
        public string ToogleStopwatch()
        {
            stopwatch.Start();
            Device.StartTimer(TimeSpan.FromMinutes(0), () =>
            {
                Time= stopwatch.Elapsed.ToString();
                return true;
            }
            );
            return Time;
        }

        public string DisableStopwatch()
        {
            stopwatch.Stop();
            Time = "0";
            return Time;
        }
    }
}
