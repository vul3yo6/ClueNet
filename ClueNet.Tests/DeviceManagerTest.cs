using ClueNet.Core.Daq;
using ClueNet.Core.Daq.Interfaces;
using System;
using Xunit;

namespace ClueNet.Tests
{
    public class DeviceManagerTest : IDisposable
    {
        private readonly DeviceManager _manager;

        public DeviceManagerTest()
        {
            _manager = new DeviceManager();
        }

        public void Dispose()
        {
            _manager.Dispose();
        }

        [Fact]
        public void DaqDevice_WhenConnected_IsCorrect()
        {
            // Arrange
            double actual = 0;
            double expected = 25;
            _manager.Initial();
            _manager.DataReceived += (object sender, DaqDataEventArgs e) =>
            {
                if (e.Name == "DemoTemperature")    // only TemperatureDemo
                {
                    actual = e.Value;
                }
                else
                {
                    double temp = e.Value;
                }
            };

            // Act
            _manager.Connect();
            System.Threading.Thread.Sleep(1200);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DiDevice_WhenConnected_IsCorrect()
        {
            // Arrange
            SignalState actual = SignalState.Complete;
            SignalState expected = DateTime.Now.Second / 30 == 0 ? 
                SignalState.Start : SignalState.Complete;
            _manager.Initial();
            _manager.DigitalInputReceived += (object sender, DaqControlEventArgs e) =>
            {
                if (e.Name == "DemoTemperature")    // only TemperatureDemo
                {
                    actual = e.State;
                }
                else
                {
                    SignalState temp = e.State;
                }
            };

            // Act
            _manager.Connect();
            System.Threading.Thread.Sleep(1200);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
