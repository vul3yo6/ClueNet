using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ClueNet.Core.Structures
{
    // VectorClock 內含多組 VectorNum
    public class VectorClock
    {
        public string Name { get; private set; }
        private ConcurrentDictionary<string, VectorNum> _dict { get; set; }
            = new ConcurrentDictionary<string, VectorNum>();

        public ReadOnlyDictionary<string, VectorNum> Vectors
        {
            get { return new ReadOnlyDictionary<string, VectorNum>(_dict); }
        }

        public VectorClock(string name, IEnumerable<string> channelNames)
        {
            Name = name;

            foreach (string channelName in channelNames)
            {
                _dict[channelName] = new VectorNum();
            }
        }

        // 單次加總
        public void Add()
        {
            _dict[Name].Add();
        }

        // 所有加總
        public void Add(ConcurrentDictionary<string, VectorNum> clocksDict)
        {
            foreach (var kvp in clocksDict)
            {
                if (kvp.Key == Name)
                {
                    _dict[Name].Add();
                }
                else
                {
                    _dict[kvp.Key].Set(kvp.Value.Num);
                }
            }
        }

        public override string ToString()
        {
            return $"{string.Join(",", Vectors.Select(x => "[" + x.Key + ":" + x.Value + "]"))}";
        }
    }

    // VectorClock 內的數值
    public class VectorNum
    {
        private readonly object _lock = new object();
        private byte _num;
        public byte Num
        {
            get { return _num; }
            set 
            {
                lock (_lock)
                {
                    _num = value;  
                }
            }
        }

        public VectorNum()
        {
            Reset();
        }

        public VectorNum(byte num)
        {
            Num = num;
        }

        public void Add()
        {
            if (Num == byte.MaxValue)
            {
                Reset();
            }

            Num++;
        }

        public void Set(byte value)
        {
            Num = value;
        }

        private void Reset()
        {
            Num = byte.MinValue;
        }

        public override string ToString()
        {
            return Num.ToString();
        }
    }
}
