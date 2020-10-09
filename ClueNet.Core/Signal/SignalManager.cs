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
        private SignalGroupManager _signalGroup;

        public SignalManager()
        {
            _signalGroup = new SignalGroupManager();
        }

        // 依據 IO 變更 flag 後, 才收集數值
        public void AddSignalItem(string groupName, string signalName, double value)
        {
            _signalGroup.AddValue(groupName, signalName, value);
        }

        public void SetSignalGroupEnabled(string groupName, SignalState state)
        {
            _signalGroup[groupName].SignalState = state;
        }

        public void SetSignalItemEnabled(string groupName, string signalName, SignalState state)
        {
            _signalGroup[groupName][signalName].SignalState = state;
        }

        public int GetSignalItemCount(string groupName, string signalName)
        {
            return _signalGroup[groupName][signalName].Count;
        }
    }
}
