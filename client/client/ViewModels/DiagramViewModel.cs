using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;

using SkiaSharp;
using client.ViewModels.Charts;


namespace client.ViewModels
{
    public class DiagramViewModel : ViewModelBase
    {
        public TestChartViewModel TestChart { get; set; } = new TestChartViewModel();
    }
}
