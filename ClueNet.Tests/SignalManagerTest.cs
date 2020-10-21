using ClueNet.Core.Daq.Interfaces;
using ClueNet.Core.Signal;
using System;
using System.Collections.Generic;
using Xunit;

namespace ClueNet.Tests
{
    public class SignalManagerTest
    {
        private readonly List<string> _groupNames = new List<string>()
        {
            "DemoGroup"
        };
        private readonly List<string> _channelNames = new List<string>()
        {
            "DemoTemperature", "DemoVoltage"
        };

        private readonly SignalManager _manager;

        public SignalManagerTest()
        {
            _manager = new SignalManager(_groupNames, _channelNames);
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
            _manager.SetSignalItemEnabled(groupName, signalName, SignalState.Start);
            _manager.AddSignalItem(groupName, signalName, 0.99);
            expected = 1;
            actual = _manager.GetSignalItemCount(groupName, signalName);

            string temp = _manager.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddValue_VectorClock_IsCorrect()
        {
            // Arrange
            string groupName = "DemoGroup";
            string signalName1 = "DemoTemperature";
            string signalName2 = "DemoVoltage";
            double actual = 0;
            double expected = 0;

            // Act
            _manager.SetSignalItemEnabled(groupName, signalName1, SignalState.Start);
            _manager.AddSignalItem(groupName, signalName1, 0.99);

            _manager.SetSignalItemEnabled(groupName, signalName2, SignalState.Start);
            _manager.AddSignalItem(groupName, signalName2, 0.88);
            _manager.AddSignalItem(groupName, signalName1, 0.99);

            string temp = _manager.ToString();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
