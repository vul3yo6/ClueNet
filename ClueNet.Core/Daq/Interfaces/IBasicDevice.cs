using System;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Daq.Interfaces
{
    public interface IBasicDevice : IDisposable
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
}
