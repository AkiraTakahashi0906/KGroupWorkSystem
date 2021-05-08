using KGroupWorkSystem.Domain.Repositories;
using KGroupWorkSystem.Domain.services;
using KGroupWorkSystem.Infrastructure.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.BackgroundWorkers
{
    internal static class TimeManagementTimer
    {
        private static Timer _timer;
        private static bool _isWork = false;
        private static ITimeManagementRepository _timeManagementRepository 
            =  new TimeManagementSQLServer();

        static TimeManagementTimer()
        {
            _timer = new Timer(Callback);
        }

        internal static void Start()
        {
            TimeManagementServices.GetInstance().WorkEntitis = _timeManagementRepository.GetWorks();
            _timer.Change(0, 30000);
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
                _timeManagementRepository.ActiityChange(ActivityName.AutoWaitMode);
            }
            finally
            {
                _isWork = false;
            }
        }
    }
}
