using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.ViewModels.Charts.ChartItems
{
    public class GanttItem
    {
        public string TaskName { get; set; }
        public string TaskType { get; set; } // запрос | ответ
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }



        public int StartExample { get; set; }
        public int EndExample { get; set; }



        public string Color { get; set; }


    }
}
