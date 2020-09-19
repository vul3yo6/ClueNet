using ClueNet.Core.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Daq.Interfaces
{
    /// <summary>
    /// 硬體擷取卡 / IO 控制卡
    /// </summary>
    public interface IDaqDevice : IDisposable
    {
        // 設備顯示的名稱 (內部用 GUID 來識別較佳)
        string Name { get; }

        bool IsConnected { get; }

        // 呼叫 Connect 連線之前, 會先呼叫此方法
        // 請先在此設定好相關設定
        void Initial();

        // 連線方法
        void Connect();
        void Disconnect();
        void Reconnect();

        // 斷線重連的提示
        event EventHandler NeedReconnected;

        // 連線狀態改變
        event EventHandler<DeviceConnectionStateEventArgs> ConnectionStateChanged;

        // 收到數值
        event EventHandler<DaqDataEventArgs> DataReceived;
    }

    public enum ConnectionState
    {
        None,
        Connected,
        Disconnected,
    }

    // 連線狀態
    public class DeviceConnectionStateEventArgs : EventArgs
    {
        public ConnectionState State { get; set; }

        public DeviceConnectionStateEventArgs(ConnectionState state)
        {
            State = state;
        }
    }

    // 資料數值
    public class DaqDataEventArgs : EventArgs
    {
        public string Name { get; set; }

        // todo: 實作  vector clock
        public double Value { get; set; }

        public DaqDataEventArgs(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
