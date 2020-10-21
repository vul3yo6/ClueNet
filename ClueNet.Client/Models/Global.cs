using ClueNet.Client.Models.Managers;
using ClueNet.Core.Daq;
using ClueNet.Core.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace ClueNet.Client.Models
{
    internal static class Global
    {
        internal static ConfigManager Configs = new ConfigManager();
        internal static DeviceManager Devices = new DeviceManager();
        internal static SignalManager Signal = null;

        internal static NavigationService NavigationService
        {
            get { return (Application.Current.MainWindow as MainWindow)?.pnlBody.NavigationService; }
        }

        internal static void Init()
        {
            Configs.Read();
            Devices.Initial();
            Signal = new SignalManager(
                Configs.Signal.Groups.ToList(), 
                Configs.Signal.Channels.ToList());

        }
    }
}
