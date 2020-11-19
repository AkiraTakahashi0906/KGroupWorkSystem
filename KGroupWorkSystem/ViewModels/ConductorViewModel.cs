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

namespace KGroupWorkSystem.ViewModels
{
    public class ConductorViewModel : BindableBase
    {
        private IWorkOrderRepository _workOrderRepository;
        private IDisposable observableTimer;
        public ConductorViewModel()
        {
            _workOrderRepository = new WorkOrderSQLServer();
            StartButton = new DelegateCommand(StartButtonExecute);

            observableTimer = Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
               .Subscribe(_ =>
               {
                   LabelContent.Value += 1;
                   Worker1Workings.Value = new ObservableCollection<WorkingEntity>(WorkingData.GetWorkings(WorkId.Value, 1));
                   Worker2Workings.Value = new ObservableCollection<WorkingEntity>(WorkingData.GetWorkings(WorkId.Value, 2));
                   Worker3Workings.Value = new ObservableCollection<WorkingEntity>(WorkingData.GetWorkings(WorkId.Value, 3));
                   CurrentWorker1Workings.Value = Worker1Workings.Value.ToList().Where(x=>x.IsDone==false).OrderBy(x => x.WorkId).FirstOrDefault();
                   CurrentWorker2Workings.Value = Worker2Workings.Value.ToList().Where(x => x.IsDone == false).OrderBy(x => x.WorkId).FirstOrDefault();
                   CurrentWorker3Workings.Value = Worker3Workings.Value.ToList().Where(x => x.IsDone == false).OrderBy(x => x.WorkId).FirstOrDefault();
               });
        }
        public ReactivePropertySlim<int> LabelContent { get; } = new ReactivePropertySlim<int>(0);
        public ReactivePropertySlim<int> WorkId { get; } = new ReactivePropertySlim<int>(1000);
        public ReactivePropertySlim<ObservableCollection<WorkingEntity>> Workings { get; } = new ReactivePropertySlim<ObservableCollection<WorkingEntity>>();
        public ReactivePropertySlim<ObservableCollection<WorkingEntity>> Worker1Workings { get; } = new ReactivePropertySlim<ObservableCollection<WorkingEntity>>();
        public ReactivePropertySlim<ObservableCollection<WorkingEntity>> Worker2Workings { get; } = new ReactivePropertySlim<ObservableCollection<WorkingEntity>>();
        public ReactivePropertySlim<ObservableCollection<WorkingEntity>> Worker3Workings { get; } = new ReactivePropertySlim<ObservableCollection<WorkingEntity>>();
        public ReactivePropertySlim<WorkingEntity> CurrentWorker1Workings { get; } = new ReactivePropertySlim<WorkingEntity>();
        public ReactivePropertySlim<WorkingEntity> CurrentWorker2Workings { get; } = new ReactivePropertySlim<WorkingEntity>();
        public ReactivePropertySlim<WorkingEntity> CurrentWorker3Workings { get; } = new ReactivePropertySlim<WorkingEntity>();
        public DelegateCommand StartButton { get; }

        private void StartButtonExecute()
        {
        }
    }
}
