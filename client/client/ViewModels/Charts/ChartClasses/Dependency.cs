using client.ViewModels.Charts.ChartItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.ViewModels.Charts.ChartClasses
{
    public class Dependency
    {
        public GanttItem From { get; set; }
        public GanttItem To { get; set; }
    }
}
