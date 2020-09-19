using System;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Daq.Interfaces
{
    public interface IDaqDeviceManager
    {
        /// <summary>
        /// Check the health for all the devices.
        /// </summary>
        /// <param name="reasons">the error reasons for each device</param>
        /// <returns></returns>
        bool CheckHealth(out Dictionary<string, string> reasons);
        void Connect();
    }
}
