using ClueNet.Core.Daq;
using ClueNet.Core.Daq.Interfaces;
using System;
using Xunit;

namespace ClueNet.Tests
{
    public class DeviceManagerTest : IDisposable
    {
        private readonly DaqDeviceManager _deviceManager;

        public DeviceManagerTest()
        {
            _deviceManager = new DaqDeviceManager();
        }

        public void Dispose()
        {
            _deviceManager.Dispose();
        }

        [Fact]
        public void CheckHealth_WhenDisconnected_ReturnFalse()
        {
            bool result = _deviceManager.CheckHealth();

            Assert.False(result, "1 should not be prime");
        }

        [Fact]
        public void Develop_Test()
        {
            // Arrange
            double actual = 0;
            double expected = 25;
            _deviceManager.Initial();
            _deviceManager.DataReceived += (object sender, DaqDataEventArgs e) =>
            {
                if (e.Name == "TemperatureDemo")    // only TemperatureDemo
                {
                    actual = e.Value;
                }
                else
                {
                    double temp = e.Value;
                }
            };

            // Act
            _deviceManager.Connect();
            System.Threading.Thread.Sleep(1200);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
