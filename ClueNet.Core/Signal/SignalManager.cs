using ClueNet.Core.Daq.Interfaces;
using ClueNet.Core.Structures;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("ClueNet.Tests")]

namespace ClueNet.Core.Signal
{
    internal class SignalManager
    {
        public SignalGroupManager Signals { get; private set; }

        public SignalManager(List<string> groupNames, List<string> channelNames)
        {
            Signals = new SignalGroupManager(groupNames, channelNames);
        }

        // 依據 IO 變更 flag 後, 才收集數值
        public void AddSignalItem(string groupName, string signalName, double value)
        {
            Signals.AddValue(groupName, signalName, value);
        }

        public void SetSignalItemEnabled(string groupName, string signalName, SignalState state)
        {
            Signals[groupName][signalName].SignalState = state;
        }

        public int GetSignalItemCount(string groupName, string signalName)
        {
            return Signals[groupName][signalName].Count;
        }
    }
}
