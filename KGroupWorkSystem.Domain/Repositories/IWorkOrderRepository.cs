using KGroupWorkSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.Repositories
{
    public interface IWorkOrderRepository
    {
        void ReStart();
        void Registration();
        void ToNext(int workId, int workerId);
        List<WorkingEntity> GetWorkingData();
        void UpdateWorkingData(WorkingEntity workingEntity);
        void UpdateCurrentWorkingData(WorkingEntity workingEntity);
    }
}
