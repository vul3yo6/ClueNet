using ClueNet.Client.Models;
using ClueNet.Client.Models.Structures;
using ClueNet.Core.Daq.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClueNet.Client.Views.Pages
{
    /// <summary>
    /// SignalPage.xaml 的互動邏輯
    /// </summary>
    public partial class SignalPage : Page
    {
        private Queue<DaqDataEventArgs> _queue = new Queue<DaqDataEventArgs>();

        public SignalPage()
        {
            InitializeComponent();

            Global.NavigationService.LoadCompleted += NavigationService_LoadCompleted;
            Global.Devices.DataReceived += Devices_DataReceived;
        }

        private void Devices_DataReceived(object sender, DaqDataEventArgs e)
        {
            if (_queue.Count > 50)
            {
                //CreateDataChart(_queue.ToList());
                _queue.Clear();
            }

            string name = e.Name;
            double value = e.Value;
            _queue.Enqueue(e);
        }

        private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            var obj = e.ExtraData as Uri;
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            viewer.Load();
        }
    }
}
