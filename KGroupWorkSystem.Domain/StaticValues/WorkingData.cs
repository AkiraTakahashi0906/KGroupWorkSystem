using KGroupWorkSystem.Domain.Entities;
using KGroupWorkSystem.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.StaticValues
{
    public static class WorkingData
    {
        private static List<WorkingEntity> _entities = new List<WorkingEntity>();

        public static void Create(IWorkOrderRepository workOrderRepository)
        {
            lock (((ICollection)_entities).SyncRoot)
            {
                _entities.Clear();
                _entities.AddRange(workOrderRepository.GetWorkingData());
            }
        }
        public static List<WorkingEntity> GetWorkings(int workId , int workerId)
        {
            lock (((ICollection)_entities).SyncRoot)
            {
                return _entities.Where(x => x.WorkId == workId && x.WorkerId == workerId).ToList();
            }
        }

        public static List<WorkingEntity> GetWorkings(int workId)
        {
            lock (((ICollection)_entities).SyncRoot)
            {
                return _entities.Where(x => x.WorkId == workId).ToList();
            }
        }
    }
}
