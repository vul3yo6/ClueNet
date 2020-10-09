using ClueNet.Core.Daq.Interfaces;
using ClueNet.Core.Signal;
using System;
using Xunit;

namespace ClueNet.Tests
{
    public class SignalManagerTest
    {
        private readonly SignalManager _manager;

        public SignalManagerTest()
        {
            _manager = new SignalManager();
        }

        [Fact]
        public void AddValue_IsCorrect()
        {
            // Arrange
            double actual = 0;
            double expected = 0;

            // Act
            string groupName = "DemoGroup";
            string signalName = "DemoTemperature";
            _manager.AddSignalItem(groupName, signalName, 0.99);
            actual = _manager.GetSignalItemCount(groupName, signalName);

            // Assert
            Assert.Equal(expected, actual);

            // Act
            _manager.SetSignalGroupEnabled(groupName, SignalState.Start);
            _manager.SetSignalItemEnabled(groupName, signalName, SignalState.Start);
            _manager.AddSignalItem(groupName, signalName, 0.99);
            expected = 1;
            actual = _manager.GetSignalItemCount(groupName, signalName);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
