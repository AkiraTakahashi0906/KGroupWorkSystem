using KGroupWorkSystem.Domain.Repositories;
using KGroupWorkSystem.Domain.services;
using KGroupWorkSystem.Infrastructure.SQLServer;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Transactions;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.ViewModels
{
    public class WorkOrderViewModel : BindableBase
    {
        private IWorkOrderRepository _workOrderRepository;
        private ITimeManagementRepository _timeManagementRepository;
        private TimeManagementServices _timeRecorder;
        public WorkOrderViewModel()
        {
            _workOrderRepository = new WorkOrderSQLServer();
            _timeManagementRepository = new TimeManagementSQLServer();
            StartButton = new DelegateCommand(StartButtonExecute);
            TimeButton = new DelegateCommand(TimeButtonExecute);

            _timeRecorder = new TimeManagementServices(_timeManagementRepository.GetWorks());
        }
        public ReactivePropertySlim<string> TimeText { get; private set; } = new ReactivePropertySlim<string>();

        public DelegateCommand StartButton { get; }
        public DelegateCommand TimeButton { get; }

        private void TimeButtonExecute()
        {
            TimeText.Value = WorkingActivity.GetInstance().DisplayElapsedTime;
        }

        private void StartButtonExecute()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var result = _timeRecorder.ActiityChange(ActivityName.PartsBarcodeReading);
                _timeManagementRepository.ActiityChangeSave(result);
                scope.Complete();
            }
            
            _workOrderRepository.ReStart();
        }
    }
}
