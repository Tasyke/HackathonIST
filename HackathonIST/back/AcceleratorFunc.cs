using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace HackathonIST.back
{
    class AcceleratorFunc
    {
        double MinAcceleration = 4.5;
        public void ToggleAccelerator()
        {
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.UI);
        }

        public void DisableAccelerator()
        {
            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
            Accelerometer.Stop();
        }

        public void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            SOSButton SOSCalling = new SOSButton();
            if (MinAcceleration < Math.Abs(e.Reading.Acceleration.Z))
            {
                SOSCalling.SOSCall();
            }
            if (MinAcceleration < Math.Abs(e.Reading.Acceleration.Y))
            {
                SOSCalling.SOSCall();
            }
            if (MinAcceleration < Math.Abs(e.Reading.Acceleration.X))
            {
                SOSCalling.SOSCall();
            }
        }
    }
}
