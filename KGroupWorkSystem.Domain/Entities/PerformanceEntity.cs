using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.Entities
{
    public sealed class PerformanceEntity
    {

        public PerformanceEntity(DateTime startTime,
                                              DateTime endTime,
                                              WorkEntity workEntity)
        {
            StartTime = startTime;
            EndTime = endTime;
            WorkEntity = workEntity;
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public WorkEntity WorkEntity { get; set; }
        private TimeSpan _timeSpan => EndTime - StartTime;
        public int TimeSpanMs => _timeSpan.Milliseconds;
        public int TimeSpanS  => _timeSpan.Seconds;
        public int TimeSpanM => _timeSpan.Minutes;
        public int TimeSpanH => _timeSpan.Hours;
        public int TimeSpanD => _timeSpan.Days;
        public double TotalTimeMs => _timeSpan.TotalMilliseconds;
        public double TotalTimeS => _timeSpan.TotalSeconds;
        public double TotalTimeM => _timeSpan.TotalMinutes;
        public double TotalTimeH => _timeSpan.TotalHours;
        public double TotalTimeD => _timeSpan.TotalDays;
    }
}
