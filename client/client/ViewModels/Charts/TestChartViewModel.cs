using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.ViewModels.Charts
{
    public class TestChartViewModel 
    {
        public ISeries[] Series { get; set; } =
        {
            new LineSeries<double>{
                Values = new double[] { 100,3,3,3,5,5,6,88,7 },
                Fill=null
            }
        };
        public LabelVisual Title { get; set; } = new LabelVisual()
        {
            Text = "Titttleee",
            TextSize = 20,
            Padding = new LiveChartsCore.Drawing.Padding(5),
            Paint = new SolidColorPaint(SKColors.LightBlue)
        };
    }
}
