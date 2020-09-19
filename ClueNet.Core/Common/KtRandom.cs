using System;
using System.Collections.Generic;
using System.Text;

namespace ClueNet.Core.Common
{
    public static class KtRandom
    {
        private readonly static Random _rnd = new Random(Guid.NewGuid().GetHashCode());

        public static int Next(int maxValue)
        {
            return _rnd.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return _rnd.Next(minValue, maxValue);
        }

        public static double NextDouble()
        {
            return _rnd.NextDouble();
        }
    }
}
