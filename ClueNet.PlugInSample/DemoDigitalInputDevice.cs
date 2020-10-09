using ClueNet.Core.Daq;
using ClueNet.Core.Daq.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ClueNet.PlugInSample
{
    // the sample ablout Inheritance BaseDaqDevice
    public class DemoDigitalInputDevice : BaseDigitalInputDevice
    {
        private Timer _timer;
        private bool _previousDiOfTemperature;
        private bool _previousDiOfVoltage;

        public DemoDigitalInputDevice() : base("DemoDigitalInput")
        {
        }

        public override void Initial()
        {
            _timer = new Timer(500);
            _timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                bool isEnabledOfTemperature = DateTime.Now.Second / 30 == 0;
                bool isEnabledOfVoltage = !isEnabledOfTemperature;

                if (_previousDiOfTemperature != isEnabledOfTemperature)
                {
                    _previousDiOfTemperature = isEnabledOfTemperature;
                    TriggerDigitalInputReceived("DemoTemperature", 
                        isEnabledOfTemperature ? SignalState.Start : SignalState.Complete);
                }

                if (_previousDiOfVoltage != isEnabledOfVoltage)
                {
                    _previousDiOfVoltage = isEnabledOfVoltage;
                    TriggerDigitalInputReceived("DemoVoltage", 
                        isEnabledOfVoltage ? SignalState.Start : SignalState.Complete);
                }
            };
        }

        public override void Reconnect()
        {
            Disconnect();
            Connect();
        }

        public override void Connect()
        {
            if (IsConnected)
            {
                return;
            }

            IsConnected = true;
            TriggerConnectionStateChanged(ConnectionState.Connected);
            _timer.Start();
        }

        public override void Disconnect()
        {
            if (IsConnected == false)
            {
                return;
            }

            IsConnected = false;
            TriggerConnectionStateChanged(ConnectionState.Disconnected);
            _timer.Stop();
        }
    }
}
