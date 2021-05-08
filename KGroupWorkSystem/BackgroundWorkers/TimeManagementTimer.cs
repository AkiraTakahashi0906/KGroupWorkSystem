using KGroupWorkSystem.Domain.Repositories;
using KGroupWorkSystem.Domain.services;
using KGroupWorkSystem.Infrastructure.SQLServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.BackgroundWorkers
{
    internal static class TimeManagementTimer
    {
        private static Timer _timer;
        private static bool _isWork = false;
        private static ITimeManagementRepository _timeManagementRepository;
        private static TimeManagementServices _timeRecorder;

        static TimeManagementTimer()
        {
            _timer = new Timer(Callback);
            _timeManagementRepository = new TimeManagementSQLServer();
            _timeRecorder = new TimeManagementServices(_timeManagementRepository.GetWorks());
        }

        internal static void Start()
        {
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
                using (TransactionScope scope = new TransactionScope())
                {
                    var result = _timeRecorder.ActiityChange(ActivityName.AutoWaitMode);
                    _timeManagementRepository.ActiityChangeSave(result);
                    scope.Complete();
                }
            }
            finally
            {
                _isWork = false;
            }
        }
    }
}
