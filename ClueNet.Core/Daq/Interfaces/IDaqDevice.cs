using ClueNet.Core.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Daq.Interfaces
{
    /// <summary>
    /// 硬體擷取卡 / IO 控制卡
    /// </summary>
    public interface IDaqDevice : IBasicDevice
    {
        // 收到數值
        event EventHandler<DaqDataEventArgs> DataReceived;
    }

    // 資料數值
    public class DaqDataEventArgs : EventArgs
    {
        public string Name { get; set; }

        // todo: 實作  vector clock
        public double Value { get; set; }

        public DaqDataEventArgs(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
