using ClueNet.Core.Daq.Interfaces;
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
            if (SignalDict.ContainsKey(groupName) == false)
            {
                SignalDict[groupName] = new SignalGroup();
            }

            SignalDict[groupName].AddValue(signalName, value);
        }

        public SignalGroup this[string groupName]
        {
            get 
            {
                if (SignalDict.ContainsKey(groupName) == false)
                {
                    SignalDict[groupName] = new SignalGroup();
                }

                return SignalDict[groupName]; 
            }
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
        // 訊號群組名稱, 例如: Machine01
        public string Name { get; set; }
        private ConcurrentDictionary<string, SignalItem> SignalDict { get; set; } 
            = new ConcurrentDictionary<string, SignalItem>();

        // 是否接收數值
        private readonly object _lockOfReceiveEnabled = new object();
        private SignalState _signalState;
        public SignalState SignalState
        {
            get { return _signalState; }
            set
            {
                lock(_lockOfReceiveEnabled)
                {
                    _signalState = value;
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
            if (SignalState == SignalState.Start)
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
            get
            {
                if (SignalDict.ContainsKey(signalName) == false)
                {
                    SignalDict[signalName] = new SignalItem();
                }

                return SignalDict[signalName]; 
            }
        }
    }

    internal class SignalItem
    {
        // 訊號單元名稱, 例如: 震動
        public string Name { get; set; }
        public string DisplayName { get; set; }
        private ConcurrentQueue<double> Values { get; set; } = new ConcurrentQueue<double>();
        public int Count { get { return Values.Count; } }

        // 是否接收數值
        private readonly object _lockOfReceiveEnabled = new object();
        private SignalState _signalState;
        public SignalState SignalState
        {
            get { return _signalState; }
            set
            {
                lock (_lockOfReceiveEnabled)
                {
                    _signalState = value;
                }
            }
        }

        public void AddValue(double value)
        {
            if (SignalState == SignalState.Start)
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
