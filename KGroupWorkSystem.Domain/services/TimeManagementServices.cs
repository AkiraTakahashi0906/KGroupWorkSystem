using KGroupWorkSystem.Domain.Entities;
using KGroupWorkSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.Domain.services
{
    public sealed class TimeManagementServices
    {
        private  PerformanceEntity _performance;
        private List<WorkEntity> _workEntitis;

        public TimeManagementServices(List<WorkEntity> workEntitis)
        {
            _workEntitis = workEntitis;
            SetPerformance();
        }

        private void SetPerformance()
        {
            var work = WorkActivityService.GetWork(
                                                                         _workEntitis,
                                                                         WorkingActivity.GetInstance().ActivityName);
            _performance = new PerformanceEntity(
                                                                        WorkingActivity.GetInstance().StartTime,
                                                                        WorkingActivity.GetInstance().StopTime,
                                                                        work);
        }

        public PerformanceEntity ActiityChange(ActivityName activityName)
        {
            WorkingActivity.GetInstance().StopTime = DateTime.Now;
            SetPerformance();
            WorkingActivity.GetInstance().StartTime = _performance.EndTime;
            WorkingActivity.GetInstance().ActivityName= activityName;
            return _performance;
        }
    }
}
