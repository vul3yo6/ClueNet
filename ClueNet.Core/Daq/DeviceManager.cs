﻿using ClueNet.Core.Daq.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Text;

namespace ClueNet.Core.Daq
{
    public class DeviceManager : IDisposable
    {
        #region Event

        private readonly object _lockOfDataReceived = new object();

        // reference
        // https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/add
        // https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/events/how-to-implement-custom-event-accessors
        // https://docs.microsoft.com/zh-tw/dotnet/csharp/programming-guide/events/how-to-implement-interface-events
        public event EventHandler<DaqDataEventArgs> DataReceived
        {
            add
            {
                lock (_lockOfDataReceived)
                {
                    foreach (var device in _daqDevices)
                    {
                        device.Value.DataReceived += value;
                    }
                }
            }
            remove
            {
                lock (_lockOfDataReceived)
                {
                    foreach (var device in _daqDevices)
                    {
                        device.Value.DataReceived -= value;
                    }
                }
            }
        }

        private readonly object _lockOfDigitalInputReceived = new object();

        public event EventHandler<DaqControlEventArgs> DigitalInputReceived
        {
            add
            {
                lock (_lockOfDigitalInputReceived)
                {
                    foreach (var device in _diDevices)
                    {
                        device.Value.DigitalInputReceived += value;
                    }
                }
            }
            remove
            {
                lock (_lockOfDigitalInputReceived)
                {
                    foreach (var device in _diDevices)
                    {
                        device.Value.DigitalInputReceived -= value;
                    }
                }
            }
        }

        #endregion

        public bool CheckHealth()
        {
            throw new NotImplementedException();
        }

        [ImportMany(typeof(IDaqDevice))]
        private IEnumerable<Lazy<IDaqDevice>> _daqDevices;

        [ImportMany(typeof(IDaqControlDevice))]
        private IEnumerable<Lazy<IDaqControlDevice>> _diDevices;

        public void Initial()
        {
            LoadPlugIn();

            foreach (var device in _daqDevices)
            {
                device.Value.Initial();
            }

            foreach (var device in _diDevices)
            {
                device.Value.Initial();
            }
        }

        public void Connect()
        {
            foreach (var device in _daqDevices)
            {
                device.Value.Connect();
            }

            foreach (var device in _diDevices)
            {
                device.Value.Connect();
            }
        }

        private void LoadPlugIn()
        {
            // 获取当前程序集
            //Assembly assCurr = Assembly.GetExecutingAssembly();

            // 确定组件搜索范围
            //AssemblyCatalog catalog = new AssemblyCatalog(assCurr);
            DirectoryCatalog catalog = new DirectoryCatalog(@"plugin");

            container = new CompositionContainer(catalog);

            //Fill the imports of this object
            //try
            //{
            container.SatisfyImportsOnce(this);
            //}
            //catch (CompositionException compositionException)
            //{
            //    Console.WriteLine(compositionException.ToString());
            //}


            //// 获取已发现的组件
            //var components = container.GetExportedValues<IDaqDevice<double>>();

            //// 分别调用一下
            //foreach (IDaqDevice<double> m in components)
            //{
            //    m.Initial();
            //}
        }

        #region IDisposable

        // To detect redundant calls
        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        //private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);
        private CompositionContainer container;

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
                container.Dispose();
            }

            // Dispose of any unmanaged resources not wrapped in safe handles.
            // ...

            _disposed = true;
        }

        #endregion
    }
}
