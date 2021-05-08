using KGroupWorkSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.Domain.services
{
    public sealed class WorkingActivity
    {
        private static WorkingActivity _singleInstance = new WorkingActivity();
        private WorkingActivity()
        {
            StartTime = DateTime.Now;
            StopTime = DateTime.Now;
            ActivityName = ActivityName.AutoWaitMode;
        }
        public static WorkingActivity GetInstance()
        {
            return _singleInstance;
        }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public ActivityName ActivityName { get; set; }
        public TimeSpan elapsedTime => DateTime.Now - StartTime;
        public string DisplayElapsedTime => elapsedTime.TotalSeconds.ToString();
    }
}
