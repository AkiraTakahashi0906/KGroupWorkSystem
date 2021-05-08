using KGroupWorkSystem.BackgroundWorkers;
using KGroupWorkSystem.Domain.Repositories;
using KGroupWorkSystem.Infrastructure.SQLServer;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using static KGroupWorkSystem.Domain.Entities.PerformanceEntity;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.ViewModels
{
    public class WorkOrderViewModel : BindableBase
    {
        private IWorkOrderRepository _workOrderRepository;
        private ITimeManagementRepository _timeManagementRepository;
        public WorkOrderViewModel()
        {
            _workOrderRepository = new WorkOrderSQLServer();
            _timeManagementRepository = new TimeManagementSQLServer();
            _timeManagementRepository.ActiityChange(ActivityName.PartsShelving);
            StartButton = new DelegateCommand(StartButtonExecute);
        }
        public DelegateCommand StartButton { get; }

        private void StartButtonExecute()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _timeManagementRepository.ActiityChange(ActivityName.PartsBarcodeReading);
                scope.Complete();
            }
            
            _workOrderRepository.ReStart();
        }
    }
}
