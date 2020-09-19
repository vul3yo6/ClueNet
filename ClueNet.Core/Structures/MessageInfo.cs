using System;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Structures
{
    /// <summary>
    /// 訊息結構
    /// </summary>
    internal class MessageInfo
    {
        public DateTime Timetag { get; set; }
        public string Message { get; set; }
        public MessageLevel Level { get; set; }
    }

    /// <summary>
    /// 訊息等級
    /// </summary>
    internal enum MessageLevel
    {
        Debug,
        Info,
        Warn,
        Error
    }
}
