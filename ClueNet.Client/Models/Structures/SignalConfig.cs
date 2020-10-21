using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueNet.Client.Models.Structures
{
    public class SignalConfig
    {
        public HashSet<string> Groups { get; set; }
        public HashSet<string> Channels { get; set; }
    }
}
