using ClueNet.Core.Daq.Interfaces;
using System;
using Xunit;

namespace ClueNet.Tests
{
    public class DaqDeviceTest
    {
        [Fact]
        public void DataReceived_IsCorrect()
        {
            // Arrange
            double actual = 0;
            double expected = 100;
            var device = new TestDaqDevice();
            device.Initial();
            device.DataReceived += (object sender, DaqDataEventArgs e) =>
            {
                actual = e.Value;
            };

            // Act
            device.Connect();
            System.Threading.Thread.Sleep(1000);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
