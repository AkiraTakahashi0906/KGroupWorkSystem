using KGroupWorkSystem.Domain.Entities;
using KGroupWorkSystem.Domain.Repositories;
using KGroupWorkSystem.Domain.StaticValues;
using KGroupWorkSystem.Infrastructure.SQLServer;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using static KGroupWorkSystem.Domain.Entities.PerformanceEntity;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.ViewModels
{
    public class WorkerViewModel : BindableBase
    {
        private IWorkOrderRepository _workOrderRepository;
        private ITimeManagementRepository _timeManagementRepository;
        private IDisposable observableTimer;
        public WorkerViewModel()
        {
            _workOrderRepository = new WorkOrderSQLServer();
            Worker1StartButton = new DelegateCommand(Worker1StartButtonExecute);
            Worker2StartButton = new DelegateCommand(Worker2StartButtonExecute);
            Worker3StartButton = new DelegateCommand(Worker3StartButtonExecute);
            _timeManagementRepository = new TimeManagementSQLServer();

            observableTimer = Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
                                       .Subscribe(_ =>
                                       {
                                           LabelContent.Value += 1;
                                           WorkerWorkings.Value = new ObservableCollection<WorkingEntity>(WorkingData.GetWorkings(WorkId.Value, WorkerId.Value));
                                       });

            NextCommand = CanNext.Select(x => x == false).ToReactiveCommand();
            NextCommand.Subscribe(_ => { _workOrderRepository.ToNext(WorkId.Value, WorkerId.Value);});

            WorkerId.Subscribe(x =>
            {
            });

        }
        public ReactivePropertySlim<bool> CanNext { get; } = new ReactivePropertySlim<bool>(false);
        public ReactivePropertySlim<int> LabelContent { get; } = new ReactivePropertySlim<int>(0);
        public ReactivePropertySlim<int> WorkId { get; } = new ReactivePropertySlim<int>(1000);
        public ReactivePropertySlim<int> WorkerId { get; } = new ReactivePropertySlim<int>(0);
        public ReactivePropertySlim<ObservableCollection<WorkingEntity>> WorkerWorkings { get; } = new ReactivePropertySlim<ObservableCollection<WorkingEntity>>();
        public ReactiveCommand NextCommand { get; private set; }

        public DelegateCommand Worker1StartButton { get; }
        public DelegateCommand Worker2StartButton { get; }
        public DelegateCommand Worker3StartButton { get; }

        private void Worker1StartButtonExecute()
        {
            WorkerId.Value = 1;
        }
        private void Worker2StartButtonExecute()
        {
            WorkerId.Value = 2;
        }
        private void Worker3StartButtonExecute()
        {
            WorkerId.Value = 3;
        }
    }
}
