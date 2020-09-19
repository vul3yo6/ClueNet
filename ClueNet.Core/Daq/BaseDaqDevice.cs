using ClueNet.Core.Daq.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;

namespace ClueNet.Core.Daq
{
    [InheritedExport(typeof(IDaqDevice))]
    public abstract class BaseDaqDevice : IDaqDevice
    {
        public string Name { get; private set; }
        public bool IsConnected { get; protected set; }

        public BaseDaqDevice(string name)
        {
            Name = name;
        }

        public event EventHandler NeedReconnected;

        protected void TriggerNeedReconnected()
        {
            NeedReconnected?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<DeviceConnectionStateEventArgs> ConnectionStateChanged;

        protected void TriggerConnectionStateChanged(ConnectionState state)
        {
            ConnectionStateChanged?.Invoke(this, new DeviceConnectionStateEventArgs(state));
        }

        public event EventHandler<DaqDataEventArgs> DataReceived;

        protected void TriggerDataReceived(double value)
        {
            DataReceived?.Invoke(this, new DaqDataEventArgs(Name, value));
        }

        public abstract void Initial();

        public abstract void Connect();

        public abstract void Disconnect();

        public abstract void Reconnect();

        #region IDisposable

        // To detect redundant calls
        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        //private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            // Dispose of managed resources here. (managed objects)
            if (disposing)
            {
                DisposeManagedObjects();
            }

            // Dispose of any unmanaged resources not wrapped in safe handles.
            DisposeUnmanagedObjects();

            _disposed = true;
        }

        protected virtual void DisposeUnmanagedObjects() { }
        protected virtual void DisposeManagedObjects() { }

        #endregion
    }
}
