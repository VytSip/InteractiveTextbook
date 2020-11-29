using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveTextbook.Tools
{
    public class TimerTracker
    {
        private static TimerTracker instance = null;

        public bool StopCounting = false;
        public DateTime StartedReading = new DateTime();

        protected TimerTracker()
        {
        }

        public static TimerTracker GetInstance()
        {
            if (instance == null)
            {
                instance = new TimerTracker();
            }
            return instance;
        }
    }
}
