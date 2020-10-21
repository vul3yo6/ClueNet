using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueNet.Client.Models.Structures
{
    internal class LineChartInfo
    {
        public int Color { get; set; }
        public int LineWidth { get; set; }
        public int Symbol { get; set; }
        public int SymbolSize { get; set; }
        public bool IsSpecialStar { get; set; }
        public LineStyle LineStyle { get; set; }

        public LineChartInfo(int color, int lineWidth, int symbol, LineStyle lineStyle, int symbolSize = 8, bool isSpecialStar = false)
        {
            Color = color;
            LineWidth = lineWidth;
            Symbol = symbol;
            SymbolSize = symbolSize;
            IsSpecialStar = isSpecialStar;
            LineStyle = lineStyle;
        }

        public LineChartInfo(int color, int lineWidth, int symbol, int symbolSize = 8, bool isSpecialStar = false)
        {
            Color = color;
            LineWidth = lineWidth;
            Symbol = symbol;
            SymbolSize = symbolSize;
            IsSpecialStar = isSpecialStar;
            LineStyle = LineStyle.Continuous;
        }
    }
    internal enum LineStyle { Continuous, Dash, Dot }
}
