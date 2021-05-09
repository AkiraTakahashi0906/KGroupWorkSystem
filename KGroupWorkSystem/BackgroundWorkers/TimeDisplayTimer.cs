using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KGroupWorkSystem.BackgroundWorkers
{
    internal class TimeDisplayTimer
    {
        private Timer _timer;
        private bool _isWork = false;


        public TimeDisplayTimer(TimerCallback o)
        {
            _timer = new Timer(o);
        }

        internal void Start()
        {
            _timer.Change(0, 1000);
        }

        internal void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        private void Callback(object o)
        {

            if (_isWork)
            {
                return;
            }

            try
            {
                _isWork = true;
            }
            finally
            {
                _isWork = false;
            }
        }
    }
}
