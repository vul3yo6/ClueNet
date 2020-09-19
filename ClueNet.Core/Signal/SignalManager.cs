using ClueNet.Core.Structures;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void SetSignalGroupEnabled(string groupName, bool enabled)
        {
            _signalGroup[groupName].ReceiveEnabled = enabled;
        }

        public void SetSignalItemEnabled(string groupName, string signalName, bool enabled)
        {
            _signalGroup[groupName][signalName].ReceiveEnabled = enabled;
        }
    }
}
