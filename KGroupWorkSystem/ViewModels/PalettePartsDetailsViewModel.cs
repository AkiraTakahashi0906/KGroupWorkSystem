using KGroupWorkSystem.Domain.Entities;
using KGroupWorkSystem.Domain.Repositories;
using KGroupWorkSystem.Infrastructure.SQLServer;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KGroupWorkSystem.ViewModels
{
    public class PalettePartsDetailsViewModel : BindableBase
    {
        IPalettePartsRepository _palettePartsRepository;
        IBomRepository _bomRepository;

        public PalettePartsDetailsViewModel()
        {
            _palettePartsRepository = new PalettePartsSQLServer();
            _bomRepository = new BomSQLServer();
            GetPalettes();
            Boms.Value = new ObservableCollection<BomPartsEntity>(_bomRepository.GetBomParts("1")); 
            SelectedBom.Subscribe(_ => SelectedBomChangeExecute());
            PlusCommand.Subscribe(_ => PlusCommandExecute());
            MinusCommand.Subscribe(_ => MinusCommandExecute());
        }
        public ReactivePropertySlim<ObservableCollection<BomPartsEntity>> Boms { get; }
                    = new ReactivePropertySlim<ObservableCollection<BomPartsEntity>>();
        public ReactivePropertySlim<BomPartsEntity> SelectedBom { get; }
            = new ReactivePropertySlim<BomPartsEntity>(mode: ReactivePropertyMode.None);
        public ReactivePropertySlim<ObservableCollection<PaletteEntity>> Palettes { get; }
                    = new ReactivePropertySlim<ObservableCollection<PaletteEntity>>();
        public ReactivePropertySlim<PaletteEntity> SelectedPalette { get; } = new ReactivePropertySlim<PaletteEntity>();
        public ReactivePropertySlim<int> AddQuantity { get; } = new ReactivePropertySlim<int>(mode: ReactivePropertyMode.None);

        public ReactiveCommand PlusCommand { get; } = new ReactiveCommand();
        public ReactiveCommand MinusCommand { get; } = new ReactiveCommand();

        private void PlusCommandExecute()
        {
            AddQuantity.Value++;
        }

        private void MinusCommandExecute()
        {
            AddQuantity.Value--;
        }

        private void SelectedBomChangeExecute()
        {
            AddQuantity.Value = SelectedBom.Value.PartsQuantity;
        }

        private void GetPalettes()
        {
            Palettes.Value = new ObservableCollection<PaletteEntity>(_palettePartsRepository.GetPalettes());
        }
    }
}
