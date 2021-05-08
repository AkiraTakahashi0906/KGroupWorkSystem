using KGroupWorkSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KGroupWorkSystem.Domain.Entities.PerformanceEntity;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.Domain.Repositories
{
    public interface ITimeManagementRepository
    {
        void ActiityChangeSave(PerformanceEntity performance);
        List<WorkEntity> GetWorks();
    }
}
