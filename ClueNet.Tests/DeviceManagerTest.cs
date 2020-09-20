using ClueNet.Core.Daq;
using ClueNet.Core.Daq.Interfaces;
using System;
using Xunit;

namespace ClueNet.Tests
{
    public class DeviceManagerTest : IDisposable
    {
        private readonly DeviceManager _deviceManager;

        public DeviceManagerTest()
        {
            _deviceManager = new DeviceManager();
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
        public void DaqDevice_WhenConnected_IsCorrect()
        {
            // Arrange
            double actual = 0;
            double expected = 25;
            _deviceManager.Initial();
            _deviceManager.DataReceived += (object sender, DaqDataEventArgs e) =>
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
            _deviceManager.Connect();
            System.Threading.Thread.Sleep(1200);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DiDevice_WhenConnected_IsCorrect()
        {
            // Arrange
            bool actual = false;
            bool expected = DateTime.Now.Second / 30 == 0;
            _deviceManager.Initial();
            _deviceManager.DigitalInputReceived += (object sender, DaqDigitalInputEventArgs e) =>
            {
                if (e.Name == "DemoTemperature")    // only TemperatureDemo
                {
                    actual = e.Enabled;
                }
                else
                {
                    bool temp = e.Enabled;
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
