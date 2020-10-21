using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueNet.Client.Models.Structures
{
    internal class LineChartPoint
    {
        public int XValue { get; set; }
        public Dictionary<string, double> YValues { get; set; }

        public LineChartPoint(int xValue, Dictionary<string, double> yValues)
        {
            XValue = xValue;
            YValues = yValues;
        }
    }
}
