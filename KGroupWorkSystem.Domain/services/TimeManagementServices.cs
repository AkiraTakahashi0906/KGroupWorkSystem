using KGroupWorkSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KGroupWorkSystem.Domain.Entities.PerformanceEntity;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.Domain.services
{
    public sealed class TimeManagementServices
    {
        private static TimeManagementServices _singleInstance = new TimeManagementServices();
        private PerformanceEntity _performance;
        private DateTime _startTime;
        private DateTime _endTime;

        private TimeManagementServices()
        {
            _startTime = DateTime.Now;
            _endTime = DateTime.Now;
            var work = WorkActivityService.GetWork(WorkEntitis, ActivityName.PartsBarcodeReading);
            _performance = new PerformanceEntity(_startTime, _endTime, work);
        }
        public static TimeManagementServices GetInstance()
        {
            return _singleInstance;
        }

        public List<WorkEntity> WorkEntitis { get; set; }
        public PerformanceEntity ActiityChange(ActivityName activityName)
        {
            _performance.WorkEntity = WorkActivityService.GetWork(WorkEntitis, activityName);
            _performance.EndTime = DateTime.Now;
            var tmp = new PerformanceEntity(_performance.StartTime, _performance.EndTime, _performance.WorkEntity);
            _performance.StartTime = _performance.EndTime;
            return tmp;
        }
    }
}
