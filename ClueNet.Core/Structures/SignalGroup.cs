using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Structures
{
    internal class SignalGroupManager
    {
        private ConcurrentDictionary<string, SignalGroup> SignalDict { get; set; }
            = new ConcurrentDictionary<string, SignalGroup>();

        public void AddValue(string groupName, string signalName, double value)
        {
            SignalDict[groupName].AddValue(signalName, value);
        }

        public SignalGroup this[string groupName]
        {
            get { return SignalDict[groupName]; }
        }
    }

    internal class SignalGroup
    {
        // 訊號名稱, 例如: Machine01
        public string Name { get; set; }
        private ConcurrentDictionary<string, SignalItem> SignalDict { get; set; } 
            = new ConcurrentDictionary<string, SignalItem>();

        // 是否接收數值
        private readonly object _lockOfReceiveEnabled = new object();
        private bool _receiveEnabled;
        public bool ReceiveEnabled
        {
            get { return _receiveEnabled; }
            set
            {
                lock(_lockOfReceiveEnabled)
                {
                    _receiveEnabled = value;
                }
            }
        }

        public void AddValue(string signalName, double value)
        {
            if (ReceiveEnabled)
            {
                SignalDict[signalName].AddValue(value);
            }
        }

        public SignalItem this[string signalName]
        {
            get { return SignalDict[signalName]; }
        }
    }

    internal class SignalItem
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        private ConcurrentQueue<double> Values { get; set; } = new ConcurrentQueue<double>();

        // 是否接收數值
        private readonly object _lockOfReceiveEnabled = new object();
        private bool _receiveEnabled;
        public bool ReceiveEnabled
        {
            get { return _receiveEnabled; }
            set
            {
                lock (_lockOfReceiveEnabled)
                {
                    _receiveEnabled = value;
                }
            }
        }

        public void AddValue(double value)
        {
            if (ReceiveEnabled)
            {
                Values.Enqueue(value);
            }
        }
    }
}
