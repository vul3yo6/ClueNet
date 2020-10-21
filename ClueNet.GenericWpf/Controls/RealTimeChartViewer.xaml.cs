using ChartDirector;
using ClueNet.Client.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClueNet.GenericWpf.Controls
{
    /// <summary>
    /// RealTimeChartViewer.xaml 的互動邏輯
    /// </summary>
    public partial class RealTimeChartViewer : UserControl
    {
        public RealTimeChartViewer()
        {
            InitializeComponent();
        }

        public void Load()
        {
            CreateSampleChart();
        }

        private void CreateDataChart(ICollection<double> datas)
        {
            Dictionary<string, LineChartInfo> lineInfos = new Dictionary<string, LineChartInfo>()
            {
                { "Target", new LineChartInfo(0x0000FF, 2, 0)  }
            };

            List<LineChartPoint> listOfPoint = new List<LineChartPoint>();
            for (int i = 0; i < datas.Count; i++)
            {
                double target = datas.ElementAt(i);

                listOfPoint.Add(new LineChartPoint(i, new Dictionary<string, double>() {
                    { "Target", target }
                }));
            }

            XYChart c = GetChart((int)ActualWidth, (int)ActualHeight,
                "Count", "", (int)AppConstant.GFontSize, lineInfos, listOfPoint);

            viewer.Chart = c;
        }

        private void CreateSampleChart()
        {
            Dictionary<string, LineChartInfo> lineInfos = new Dictionary<string, LineChartInfo>()
            {
                { "USL", new LineChartInfo(0xFF0000, 2, 0, LineStyle.Dash) },
                { "LSL", new LineChartInfo(0xFF0000, 2, 0, LineStyle.Dash) },
                { "Target", new LineChartInfo(0x0000FF, 2, 0)  }
            };

            List<LineChartPoint> listOfPoint = new List<LineChartPoint>();
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < 50; i++)
            {
                double target = rnd.Next(20);

                listOfPoint.Add(new LineChartPoint(i, new Dictionary<string, double>() {
                    { "USL", target + 1 },
                    { "LSL", target - 1 },
                    { "Target", target }
                }));
            }

            XYChart c = GetChart((int)ActualWidth, (int)ActualHeight,
                "Count", "", (int)AppConstant.GFontSize, lineInfos, listOfPoint);

            viewer.Chart = c;
        }

        private static XYChart GetChart(int width, int height, string xTitle, string yTitle, int fontSize,
            Dictionary<string, LineChartInfo> lineInfos, List<LineChartPoint> points)
        {
            //Chart.setLicenseCode("XXXXX");

            XYChart c = new XYChart(width, height);

            List<string> labels = new List<string>();
            foreach (LineChartPoint point in points)
            {
                labels.Add(point.XValue.ToString());
            }

            var labelStep = 2;

            while (Math.Round(labels.Count / (double)labelStep) > 10)
            {
                labelStep = labelStep * 5;
            }

            c.xAxis().setLabels(labels.ToArray());
            c.xAxis().setLabelStep(labelStep);

            List<string> lineNames = lineInfos.Keys.ToList();
            foreach (string lineName in lineNames)
            {
                LineChartInfo lineInfo = lineInfos[lineName];

                LineLayer layer = c.addLineLayer2();
                layer.setLineWidth(lineInfo.LineWidth);

                List<double> yValues = points.ConvertAll(o => o.YValues[lineName]);
                int color = lineInfo.Color;

                DataSet ds;

                if (lineInfo.LineStyle == LineStyle.Continuous)
                {
                    ds = layer.addDataSet(yValues.ToArray(), color, lineName);
                }
                else
                {
                    ds = layer.addDataSet(yValues.ToArray(), c.dashLineColor(color, Chart.DashLine), lineName);
                }

                if (!lineInfo.IsSpecialStar)
                {
                    ds.setDataSymbol(lineInfo.Symbol, lineInfo.SymbolSize, lineInfo.Color);
                }
                else
                {
                    ds.setDataSymbol(GetStar(), lineInfo.SymbolSize, lineInfo.Color, lineInfo.Color);
                }
            }

            ChartDirector.TextBox tbXTitle = c.xAxis().setTitle(xTitle, "", fontSize);
            ChartDirector.TextBox tbYTitle = c.yAxis().setTitle(yTitle, "", fontSize);

            //c.xAxis().setLinearScale(minX, maxX);
            //GenericMethods.SetRangeY(c, maxY, minY);
            SetFontSize(c, fontSize);
            SetOptions(c, width, height, fontSize, 20 + tbXTitle.getHeight(), 10 + tbYTitle.getHeight());

            return c;
        }

        private static int[] GetStar()
        {
            List<int> p = new List<int>();

            for (int i = 0; i <= 7; i++)
            {
                p.AddRange(Rotation(33.5, 450, i * 45));
                p.AddRange(Rotation(-33.5, 450, i * 45));
                p.AddRange(Rotation(-33.5, 81, i * 45));
            }

            return p.ToArray();
        }
        private static List<int> Rotation(double x, double y, double angle)
        {
            double t = angle / 180 * Math.PI;

            double x2 = x * Math.Cos(t) - y * Math.Sin(t);
            double y2 = x * Math.Sin(t) + y * Math.Cos(t);


            return new List<int>() {
                (int)Math.Round(x2, MidpointRounding.AwayFromZero),
                (int)Math.Round(y2, MidpointRounding.AwayFromZero) +500
            };
        }

        private static XYChart SetFontSize(XYChart c, int fontSize, int xFontAngle = 0)
        {
            c.xAxis().setLabelStyle("Arial", fontSize).setFontAngle(xFontAngle);
            c.yAxis().setLabelStyle("Arial", fontSize);

            return c;
        }

        public static XYChart SetOptions(XYChart c, int width, int height, int fontSize, int yLabelWidth = 20, int bottomLabelHeight = 10, bool isShowLegendBox = true)
        {
            // 加入 Title
            //ChartDirector.TextBox tbTitle = c.addTitle("", "Arial Bold Italic", 15.0, 0);
            //tbTitle.getHeight();

            // 手動調整 x 軸的範圍
            //c.xAxis().setLinearScale(min.X, max.X);
            //c.xAxis().setRounding(false, false);
            //c.xAxis().setTickDensity(75);

            // 設定繪圖區大小
            int paddingWidth = 15;
            int paddingHeight = 10;

            yLabelWidth += fontSize;
            bottomLabelHeight += fontSize;

            int paddingLeft_X = paddingWidth + yLabelWidth;
            int paddingTop_Y = paddingHeight;
            int plotWidth = width - paddingLeft_X - paddingWidth;
            int plotHeight = height - paddingTop_Y - paddingHeight;

            c.setPlotArea(paddingLeft_X, paddingTop_Y, plotWidth, plotHeight);

            // 設定 Legend
            if (isShowLegendBox)
            {
                LegendBox lb = c.addLegend(paddingLeft_X, paddingHeight / 2, false, "", fontSize);
                lb.setBackground(Chart.Transparent);

                c.layoutLegend();

                int lbHeight = lb.getHeight();

                if (lbHeight < height * 0.4)
                {
                    paddingTop_Y = lbHeight + paddingHeight / 2;
                    plotHeight -= lbHeight;
                }
                else
                {
                    // hide LegendBox
                    lb.setPos(width * 10, height * 10);
                    plotHeight -= 8;
                }
            }

            // 重新設定繪圖區大小
            c.setPlotArea(paddingLeft_X, paddingTop_Y, plotWidth, plotHeight - bottomLabelHeight);

            // Enable clipping mode to clip the part of the data that is outside the plot area.
            c.setClipping();

            //c.setBackground(0xFFFFFF);
            //c.getPlotArea().setBackground(0xFFFFEE);
            c.setBackground(0xE5E5E5);
            c.setBorder(0x333333);

            return c;
        }
    }
}
