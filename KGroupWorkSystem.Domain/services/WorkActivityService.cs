using KGroupWorkSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.Domain.services
{
    public static class WorkActivityService
    {
        private readonly static WorkEntity _errorWorkEntity = new WorkEntity(0, "error", 0, "error", 0, "error");
        public static WorkEntity GetWork(List<WorkEntity> WorkEntities, ActivityName activityName)
        {
            WorkEntity tmp;
            if (WorkEntities == null)
            {
                tmp = _errorWorkEntity;
                return tmp;
            }
            tmp = WorkEntities.Find(x => x.WorkActivity_id == (int)activityName);
            if (tmp == null)
            {
                tmp = _errorWorkEntity;
                return tmp;
            }
            return tmp;
        }
    }
}
