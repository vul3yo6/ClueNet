using ClueNet.Core.Daq.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClueNet.Core.Structures
{
    internal class SignalGroupManager
    {
        private ConcurrentDictionary<string, SignalGroup> SignalDict { get; set; }
            = new ConcurrentDictionary<string, SignalGroup>();

        public SignalGroupManager(List<string> groupNames, List<string> channelNames)
        {
            foreach (string groupName in groupNames)
            {
                SignalDict[groupName] = new SignalGroup(groupName, channelNames);
            }
        }

        public void AddValue(string groupName, string signalName, double value)
        {
            SignalDict[groupName].AddValue(signalName, value);
        }

        public SignalGroup this[string groupName]
        {
            get 
            {
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

        public override string ToString()
        {
            return $"{string.Join(",", SignalDict.Values)}";
        }
    }

    internal class SignalGroup
    {
        // 訊號群組名稱, 例如: Machine01
        public string Name { get; set; }
        private ConcurrentDictionary<string, SignalChannel> SignalDict { get; set; } 
            = new ConcurrentDictionary<string, SignalChannel>();

        public SignalGroup(string groupName, List<string> channelNames)
        {
            Name = groupName;
            foreach (string channelName in channelNames)
            {
                SignalDict[channelName] = new SignalChannel(channelName, channelNames);
            }
        }

        public void AddValue(string signalName, double value)
        {
            SignalDict[signalName].AddValue(value);
        }

        public void Clear()
        {
            foreach (var signalItem in SignalDict.Values)
            {
                signalItem.Clear();
            }
        }

        public SignalChannel this[string signalName]
        {
            get
            {
                return SignalDict[signalName]; 
            }
        }

        public override string ToString()
        {
            return $"{Name}: {{{string.Join(",", SignalDict.Values)}}}]";
        }
    }

    internal class SignalChannel
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

        public SignalChannel(string name, List<string> channelNames)
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

    internal class SignalItem
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
