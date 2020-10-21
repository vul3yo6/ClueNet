using ClueNet.Client.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueNet.Client.Models.Managers
{
    internal class ConfigManager
    {
        internal SignalConfig Signal { get; private set; }

        internal void Read()
        {
            Signal = new SignalConfig();
            Signal.Groups = new HashSet<string>();
            Signal.Channels = new HashSet<string>();
        }
    }
}
