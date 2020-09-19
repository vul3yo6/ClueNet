using ClueNet.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Daq.Interfaces
{
    public class TestDaqDevice : BaseDaqDevice
    {
        private KtTimer _timer;

        public TestDaqDevice() : base(nameof(TestDaqDevice))
        {
        }

        public override void Initial()
        {
            _timer = new KtTimer(nameof(TestDaqDevice), 500, Pooling);
        }

        private void Pooling()
        {
            TriggerDataReceived(100);
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
