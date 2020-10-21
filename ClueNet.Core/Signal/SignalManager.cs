using ClueNet.Core.Daq.Interfaces;
using ClueNet.Core.Structures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("ClueNet.Tests")]

namespace ClueNet.Core.Signal
{
    public class SignalManager
    {
        private ConcurrentDictionary<string, SignalGroup> _signalGroupDict { get; set; }
            = new ConcurrentDictionary<string, SignalGroup>();

        private static string _lastChannelName = string.Empty;

        public SignalManager(List<string> groupNames, List<string> channelNames)
        {
            foreach (string groupName in groupNames)
            {
                _signalGroupDict[groupName] = new SignalGroup(groupName, channelNames);
            }
        }

        // 依據 IO 變更 flag 後, 才收集數值
        public void AddSignalItem(string groupName, string signalName, double value)
        {
            _signalGroupDict[groupName].AddValue(signalName, value);

            // todo
            if (_lastChannelName.Equals(signalName) == false)
            {
                _lastChannelName = signalName;
            }
        }

        public void SetSignalItemEnabled(string groupName, string signalName, SignalState state)
        {
            _signalGroupDict[groupName][signalName].SignalState = state;
        }

        public int GetSignalItemCount(string groupName, string signalName)
        {
            return _signalGroupDict[groupName][signalName].Count;
        }

        public void Reset()
        {
            foreach (var signalGroup in _signalGroupDict.Values)
            {
                signalGroup.Clear();
            }
        }

        public override string ToString()
        {
            return $"{string.Join(",", _signalGroupDict.Values)}";
        }
    }
}
