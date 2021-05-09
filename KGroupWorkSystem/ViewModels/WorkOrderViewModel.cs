using KGroupWorkSystem.BackgroundWorkers;
using KGroupWorkSystem.Domain.Entities;
using KGroupWorkSystem.Domain.Repositories;
using KGroupWorkSystem.Domain.services;
using KGroupWorkSystem.Infrastructure.SQLServer;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using System;
using System.Threading;
using System.Transactions;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.ViewModels
{
    public class WorkOrderViewModel : BindableBase, IRegionMemberLifetime
    {
        private IWorkOrderRepository _workOrderRepository;
        private ITimeManagementRepository _timeManagementRepository;
        private TimeManagementServices _timeRecorder;
        private TimeDisplayTimer _displayTimer;

        public WorkOrderViewModel()
        {
            _workOrderRepository = new WorkOrderSQLServer();
            _timeManagementRepository = new TimeManagementSQLServer();
            StartButton = new DelegateCommand(StartButtonExecute);
            TimeButton = new DelegateCommand(TimeButtonExecute);
            Time2Button = new DelegateCommand(Time2ButtonExecute);

            _timeRecorder = new TimeManagementServices(_timeManagementRepository.GetWorks());
            _displayTimer = new TimeDisplayTimer(new TimerCallback(TimeTest));
            _displayTimer.Start();
        }

        ~WorkOrderViewModel()
        {
            _displayTimer.Stop();
        }

        public ReactivePropertySlim<string> TimeText { get; private set; } = new ReactivePropertySlim<string>();
        public DelegateCommand StartButton { get; }
        public DelegateCommand TimeButton { get; }
        public DelegateCommand Time2Button { get; }

        public bool KeepAlive => false;

        private void Time2ButtonExecute()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var result = _timeRecorder.ActiityChange(ActivityName.KitRead);
                _timeManagementRepository.ActiityChangeSave(result);
                scope.Complete();
            }
        }

        private void TimeButtonExecute()
        {
            TimeText.Value = WorkingActivity.GetInstance().DisplayElapsedTime;
        }

        private void TimeTest(object o)
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
