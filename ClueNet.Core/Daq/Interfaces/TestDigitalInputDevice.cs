using ClueNet.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Daq.Interfaces
{
    public class TestDigitalInputDevice : BaseDigitalInputDevice
    {
        private KtTimer _timer;
        private bool _previousDi;

        public TestDigitalInputDevice() : base(nameof(TestDigitalInputDevice))
        {
        }

        public override void Initial()
        {
            _timer = new KtTimer(nameof(TestDaqDevice), 500, Pooling);
        }

        private void Pooling()
        {
            bool isEnabled = DateTime.Now.Second / 30 == 0;

            if (_previousDi != isEnabled)
            {
                _previousDi = isEnabled;
                TriggerDigitalInputReceived("Test-Channel1", isEnabled ? SignalState.Start : SignalState.Complete);
            }
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
