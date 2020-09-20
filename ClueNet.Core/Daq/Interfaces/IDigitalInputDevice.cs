using System;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Daq.Interfaces
{
    public interface IDigitalInputDevice : IBasicDevice
    {
        // 控制收值
        event EventHandler<DaqDigitalInputEventArgs> DigitalInputReceived;
    }

    // Digital Input 數值 (DI)
    public class DaqDigitalInputEventArgs : EventArgs
    {
        public string Name { get; set; }

        // todo: 實作  vector clock
        public bool Enabled { get; set; }

        public DaqDigitalInputEventArgs(string name, bool enabled)
        {
            Name = name;
            Enabled = enabled;
        }
    }
}
