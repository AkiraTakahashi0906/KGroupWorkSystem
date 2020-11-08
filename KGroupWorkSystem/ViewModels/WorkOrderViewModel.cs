using KGroupWorkSystem.BackgroundWorkers;
using KGroupWorkSystem.Domain.Repositories;
using KGroupWorkSystem.Infrastructure.SQLServer;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KGroupWorkSystem.ViewModels
{
    public class WorkOrderViewModel : BindableBase
    {
        private IWorkOrderRepository _workOrderRepository;
        public WorkOrderViewModel()
        {
            _workOrderRepository = new WorkOrderSQLServer();
            StartButton = new DelegateCommand(StartButtonExecute);
        }
        public DelegateCommand StartButton { get; }

        private void StartButtonExecute()
        {
            _workOrderRepository.ReStart();
        }
    }
}
