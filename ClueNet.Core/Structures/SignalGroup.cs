using ClueNet.Core.Daq.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClueNet.Core.Structures
{
    public class SignalGroup
    {
        // 訊號群組名稱, 例如: Machine01
        public string Name { get; set; }
        private ConcurrentDictionary<string, SignalQueue> _dict { get; set; } 
            = new ConcurrentDictionary<string, SignalQueue>();

        public SignalGroup(string groupName, List<string> channelNames)
        {
            Name = groupName;
            foreach (string channelName in channelNames)
            {
                _dict[channelName] = new SignalQueue(channelName, channelNames);
            }
        }

        public void AddValue(string signalName, double value)
        {
            _dict[signalName].AddValue(value);
        }

        public void Clear()
        {
            foreach (var signalItem in _dict.Values)
            {
                signalItem.Clear();
            }
        }

        public SignalQueue this[string signalName]
        {
            get
            {
                return _dict[signalName]; 
            }
        }

        public override string ToString()
        {
            return $"{Name}: {{{string.Join(",", _dict.Values)}}}]";
        }
    }

    public class SignalQueue
    {
        // 訊號單元名稱, 例如: 震動
        public string Name { get; private set; }
        public List<string> ChannelNames { get; private set; }
        public string DisplayName { get; set; }
        private ConcurrentQueue<SignalItem> Values { get; set; } = new ConcurrentQueue<SignalItem>();
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

        public SignalQueue(string name, List<string> channelNames)
        {
            Name = name;
            ChannelNames = channelNames;
        }

        public void AddValue(double value)
        {
            if (SignalState == SignalState.Start)
            {
                Values.Enqueue(new SignalItem(Name, ChannelNames, value));
            }
        }

        public void Clear()
        {
            SignalItem result;
            while (Values.TryDequeue(out result))
            {
            }
        }

        public override string ToString()
        {
            return $"{Name} [{string.Join(",", Values)}]";
        }
    }

    public class SignalItem
    {
        public VectorClock Clock { get; private set; }
        public double Value { get; private set; }

        public SignalItem(string name, IEnumerable<string> channelNames, double value)
        {
            Clock = new VectorClock(name, channelNames);
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value} [{string.Join(",", Clock.Vectors.Select(x => x.Key + "=" + x.Value))}]";
        }
    }
}
