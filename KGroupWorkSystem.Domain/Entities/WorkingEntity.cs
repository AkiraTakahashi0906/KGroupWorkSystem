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
                                        int workId,
                                        int workerId,
                                        int workOpId,
                                        string workName,
                                        string workDetails,
                                        bool isDone,
                                        bool isSync)
        {
            Id = id;
            WorkId = workId;
            WorkerId = workerId;
            WorkOpId = workOpId;
            WorkName = workName;
            WorkDetails = workDetails;
            IsDone = isDone;
            IsSync = isSync;
        }
        public int Id { get;}
        public int WorkId { get; }
        public int WorkerId { get; }
        public int WorkOpId { get; }
        public string WorkName { get; }
        public string WorkDetails { get; }
        public bool IsDone { get; }
        public bool IsSync { get; }
    }
}
