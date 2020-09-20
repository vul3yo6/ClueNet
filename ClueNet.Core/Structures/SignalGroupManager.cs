using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Structures
{
    internal enum SignalState { None, Started, Completed }

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

        public void Reset()
        {
            foreach (var signalGroup in SignalDict.Values)
            {
                signalGroup.Clear();
            }
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
                    if (value && (_receiveEnabled == false))
                    {
                        State = SignalState.Started;
                    }
                    else if ((value == false) && _receiveEnabled)
                    {
                        State = SignalState.Completed;
                    }

                    _receiveEnabled = value;
                }
            }
        }

        private readonly object _lockOfSignalState = new object();
        private SignalState _state = SignalState.None;
        public SignalState State
        {
            get { return _state; }
            private set
            {
                lock (_lockOfSignalState)
                {
                    _state = value;
                }
            }
        }

        public void AddValue(string signalName, double value)
        {
            if (ReceiveEnabled)
            {
                if (SignalDict.ContainsKey(signalName) == false)
                {
                    SignalDict[signalName] = new SignalItem();
                }

                SignalDict[signalName].AddValue(value);
            }
        }

        public void Clear()
        {
            foreach (var signalItem in SignalDict.Values)
            {
                signalItem.Clear();
            }

            State = SignalState.None;
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

        public void Clear()
        {
            double result;
            while (Values.TryDequeue(out result))
            {
            }
        }
    }
}
