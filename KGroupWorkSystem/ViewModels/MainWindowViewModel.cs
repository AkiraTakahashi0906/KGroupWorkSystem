using KGroupWorkSystem.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace KGroupWorkSystem.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            WorkOrderButton = new DelegateCommand(WorkOrderButtonExecute);
            ConductorButton = new DelegateCommand(ConductorButtonExecute);
            WorkerButton = new DelegateCommand(WorkerButtonExecute);
        }
        public DelegateCommand WorkOrderButton { get; }
        public DelegateCommand ConductorButton { get; }
        public DelegateCommand WorkerButton { get; }

        private void WorkerButtonExecute()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(WorkerView));
        }

        private void ConductorButtonExecute()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(ConductorView));
        }

        private void WorkOrderButtonExecute()
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(WorkOrderView));
        }
    }
}
