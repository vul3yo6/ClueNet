using ClueNet.Core.Daq.Interfaces;
using ClueNet.Core.Signal;
using ClueNet.Core.Structures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Xunit;

namespace ClueNet.Tests
{
    public class VectorClockTest
    {
        private readonly string _channelName01 = "DemoTemperature";
        private readonly string _channelName02 = "DemoVoltage";
        private readonly List<string> _channelNames = new List<string>()
        {
            "DemoTemperature", "DemoVoltage"
        };

        private readonly VectorClock _clock;

        public VectorClockTest()
        {
            _clock = new VectorClock(_channelName01, _channelNames);
        }

        [Fact]
        public void AddValue_IsCorrect()
        {
            // Arrange
            double actual = 0;
            double expected = 1;

            // Act
            _clock.Add();
            actual = _clock.Vectors[_channelName01].Num;

            // Assert
            Assert.Equal(expected, actual);

            // Act
            var dict = new ConcurrentDictionary<string, VectorNum>();
            dict[_channelName01] = new VectorNum();
            dict[_channelName02] = new VectorNum(5);
            _clock.Add(dict);

            expected = 5;
            actual = _clock.Vectors[_channelName02].Num;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
