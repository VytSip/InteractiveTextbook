using InteractiveTextbook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveTextbook.Models
{
    public class AnalysisViewModel
    {
        public AnalysisViewModel()
        {
            Timers = new List<PageTimer>();
            AverageTime = new TimeSpan();
        }

        public List<PageTimer> Timers { get; set; }
        public TimeSpan AverageTime { get; set; }
    }
}
