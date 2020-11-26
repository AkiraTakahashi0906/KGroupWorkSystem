using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.Entities
{
    public class WorkingEntity
    {
        public WorkingEntity(int id,
                                        int workTitleId,
                                        int workerId,
                                        int workOpId,
                                        string workTitle,
                                        string workOp,
                                        string workDetails,
                                        int workDetailsId,
                                        string caution,
                                        bool isDone,
                                        bool isSync,
                                        bool isCurrent)
        {
            Id = id;
            WorkTitleId = workTitleId;
            WorkerId = workerId;
            WorkOpId = workOpId;
            WorkTitle = workTitle;
            WorkOp = workOp;
            WorkDetails = workDetails;
            WorkDetailsId = workDetailsId;
            Caution = caution;
            IsDone = isDone;
            IsSync = isSync;
            IsCurrent = isCurrent;
        }
        public int Id { get;}
        public int WorkTitleId { get; }
        public int WorkerId { get; }
        public int WorkOpId { get; }
        public string WorkTitle { get; }
        public string WorkOp { get; }
        public string WorkDetails { get; }
        public int WorkDetailsId { get; }
        public string Caution { get; }
        public bool IsDone { get; }
        public bool IsSync { get; }
        public bool IsCurrent { get; }
    }
}
