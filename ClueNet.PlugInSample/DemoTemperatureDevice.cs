using ClueNet.Core.Daq.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Timers;

namespace ClueNet.PlugInSample
{
    // the sample ablout Inheritance IDaqDevice
    [Export(typeof(IDaqDevice))]
    public class DemoTemperatureDevice : IDaqDevice
    {
        private Timer _timer;

        public string Name { get { return "DemoTemperature"; } }

        public bool IsConnected { get; private set; }

        public event EventHandler NeedReconnected;
        public event EventHandler<DeviceConnectionStateEventArgs> ConnectionStateChanged;
        public event EventHandler<DaqDataEventArgs> DataReceived;

        public void Initial()
        {
            _timer = new Timer(500);
            _timer.Elapsed += (object sender, ElapsedEventArgs e) => 
            {
                DataReceived?.Invoke(this, new DaqDataEventArgs(Name, 25));
            };
        }

        public void Reconnect()
        {
            Disconnect();
            Connect();
        }

        public void Connect()
        {
            if (IsConnected)
            {
                return;
            }

            IsConnected = true;
            ConnectionStateChanged?.Invoke(this, new DeviceConnectionStateEventArgs(ConnectionState.Connected));
            _timer.Start();
        }

        public void Disconnect()
        {
            if (IsConnected == false)
            {
                return;
            }

            IsConnected = false;
            ConnectionStateChanged?.Invoke(this, new DeviceConnectionStateEventArgs(ConnectionState.Disconnected));
            _timer.Stop();
        }

        public void Dispose()
        {
        }
    }
}
