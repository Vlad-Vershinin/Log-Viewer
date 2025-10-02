using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Sketches;
using Avalonia.Media;
using System.Reflection;
using client.ViewModels.Charts.ChartItems;
using ReactiveUI.Fody.Helpers;
using System.Runtime.Serialization;
using LiveChartsCore.SkiaSharpView.Avalonia;

namespace client.ViewModels.Charts
{

    public class GanttDiagramViewModel
    {
        public ISeries[] GanttSeries { get; set; }

        public Axis[] YAxes { get; set; }

        public SeriesCollection Series { get; set; }
        public Func<double, string> Formatter { get; set; }
        public string[] Labels { get; set; }





        public GanttDiagramViewModel()
        {
            List<GanttItem> tasks = new List<GanttItem>
            {
                new GanttItem { TaskName = "Task A", StartTime = DateTime.Today, EndTime = DateTime.Today.AddDays(5)},
                new GanttItem { TaskName = "Task B", StartTime = DateTime.Today.AddDays(2), EndTime = DateTime.Today.AddDays(4)},
                new GanttItem { TaskName = "Task C", StartTime = DateTime.Today.AddDays(1), EndTime = DateTime.Today.AddDays(2) }
            };

            string[] distinctTaskNames = tasks.Select(t => t.TaskName).Distinct().ToArray();

            Dictionary<string, int> taskNameToIndex = distinctTaskNames
            .Select((name, index) => (name, index))
            .ToDictionary(x => x.name, x => x.index);

            YAxes = new Axis[]
        {
            new Axis
            {
                // Use the distinct names as labels
                Labels = distinctTaskNames,
                SeparatorsPaint = new SolidColorPaint(new SKColor(220, 220, 220))
            }
        };

            GanttSeries = new ISeries[]
        {
            new StackedRowSeries<GanttItem>
            {
                Values = tasks,
                // Map the Y coordinate to the task's index
                Mapping = (task, pointIndex) => new LiveChartsCore.Kernel.Coordinate(taskNameToIndex[task.TaskName], task.StartTime.Day),
                Fill = new SolidColorPaint(SKColors.Transparent),
                IgnoresBarPosition = true
            },
            new StackedRowSeries<GanttItem>
            {
                Values = tasks,
                // Calculate the bar length based on duration
                Mapping = (task, pointIndex) => new LiveChartsCore.Kernel.Coordinate(taskNameToIndex[task.TaskName], (task.EndTime.Day - task.StartTime.Day)),
                Fill = new SolidColorPaint(SKColors.Blue),
                IgnoresBarPosition = true
            }
        };




        }
    }
}
