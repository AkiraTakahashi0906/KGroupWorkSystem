using KGroupWorkSystem.Domain.StaticValues;
using KGroupWorkSystem.Infrastructure.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KGroupWorkSystem.BackgroundWorkers
{
    internal static class WorkOrderTimer
    {
        private static Timer _timer;
        private static bool _isWork = false;
        static  WorkOrderTimer()
        {
            _timer = new Timer(Callback);
        }

        internal static void Start()
        {
            _timer.Change(0, 2000);
        }

        internal static void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private static void Callback(object o)
        {
            if (_isWork)
            {
                return;
            }

            try
            {
                _isWork = true;
                WorkingData.Create(new WorkOrderSQLServer());
            }
            finally
            {
                _isWork = false;
            }
        }
    }
}
