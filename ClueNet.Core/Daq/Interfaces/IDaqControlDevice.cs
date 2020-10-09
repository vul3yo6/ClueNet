using System;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Daq.Interfaces
{
    public interface IDaqControlDevice : IBasicDevice
    {
        // 控制收值
        event EventHandler<DaqControlEventArgs> DigitalInputReceived;
    }

    // Digital Input 數值 (DI)
    public class DaqControlEventArgs : EventArgs
    {
        public string Name { get; set; }

        // todo: 實作  vector clock
        public SignalState State { get; set; }

        public DaqControlEventArgs(string name, SignalState state)
        {
            Name = name;
            State = state;
        }
    }

    public enum SignalState { None, Start, Complete, Pause }
}
