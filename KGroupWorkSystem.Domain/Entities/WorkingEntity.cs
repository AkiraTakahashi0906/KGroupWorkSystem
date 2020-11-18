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
                                        string workTitle,
                                        string workName,
                                        string workDetails,
                                        string caution,
                                        bool isDone,
                                        bool isSync)
        {
            Id = id;
            WorkId = workId;
            WorkerId = workerId;
            WorkOpId = workOpId;
            WorkTitle = workTitle;
            WorkName = workName;
            WorkDetails = workDetails;
            Caution = caution;
            IsDone = isDone;
            IsSync = isSync;
        }
        public int Id { get;}
        public int WorkId { get; }
        public int WorkerId { get; }
        public int WorkOpId { get; }
        public string WorkTitle { get; }
        public string WorkName { get; }
        public string WorkDetails { get; }
        public string Caution { get; }
        public bool IsDone { get; }
        public bool IsSync { get; }

        public bool IsCurrentId
        {
            get
            {
                //if (NeedQuantity == ReceivedQuantity) return true;
                return false;
            }
        }

    }
}
