using ClueNet.Core.Daq;
using ClueNet.Core.Daq.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ClueNet.PlugInSample
{
    // the sample ablout Inheritance BaseDaqDevice
    public class VoltageDevice : BaseDaqDevice
    {
        private Timer _timer;

        public VoltageDevice() : base("VoltageDemo")
        {
        }

        public override void Initial()
        {
            _timer = new Timer(500);
            _timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                TriggerDataReceived(5);
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
