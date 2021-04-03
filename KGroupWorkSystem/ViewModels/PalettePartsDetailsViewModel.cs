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
        private IPalettePartsRepository _palettePartsRepository;
        private IBomRepository _bomRepository;
        private readonly int _paletteid = 1;
        private bool _isDeleted = false;

        public PalettePartsDetailsViewModel()
        {
            _palettePartsRepository = new PalettePartsSQLServer();
            _bomRepository = new BomSQLServer();

            GetPalettes();
            GetPaletteDetails();
            Boms.Value = new ObservableCollection<BomPartsEntity>(_bomRepository.GetBomParts("1"));

            SelectedPaletteDetails.Subscribe(_ => SelectedPaletteDetailsChangeExecute());
            SelectedBom.Subscribe(_ => SelectedBomChangeExecute());
            PlusCommand.Subscribe(_ => PlusCommandExecute());
            MinusCommand.Subscribe(_ => MinusCommandExecute());

            PaletteDetailsPlusCommand.Subscribe(_ => PaletteDetailsPlusCommandExecute());
            PaletteDetailsMinusCommand.Subscribe(_ => PaletteDetailsMinusommandExecute());
            PaletteDetailsUpdateCommand.Subscribe(_ => PaletteDetailsSaveExecute());
            PaletteDetailsAddCommand.Subscribe(_ => PaletteDetailsAddCommandExecute());
        }
        public ReactivePropertySlim<ObservableCollection<BomPartsEntity>> Boms { get; }
                    = new ReactivePropertySlim<ObservableCollection<BomPartsEntity>>();
        public ReactivePropertySlim<BomPartsEntity> SelectedBom { get; }
            = new ReactivePropertySlim<BomPartsEntity>(mode: ReactivePropertyMode.None);
        public ReactivePropertySlim<ObservableCollection<PaletteEntity>> Palettes { get; }
                    = new ReactivePropertySlim<ObservableCollection<PaletteEntity>>();
        public ReactivePropertySlim<PaletteEntity> SelectedPalette { get; } = new ReactivePropertySlim<PaletteEntity>();
        public ReactivePropertySlim<int> AddQuantity { get; } = new ReactivePropertySlim<int>();

        public ReactivePropertySlim<ObservableCollection<PaletteDetailsEntity>> PaletteDetails { get; }
            = new ReactivePropertySlim<ObservableCollection<PaletteDetailsEntity>>();
        public ReactivePropertySlim<PaletteDetailsEntity> SelectedPaletteDetails { get; } = new ReactivePropertySlim<PaletteDetailsEntity>(mode: ReactivePropertyMode.None);
        public ReactivePropertySlim<int> UpdateQuantity { get; } = new ReactivePropertySlim<int>();

        public ReactiveCommand PlusCommand { get; } = new ReactiveCommand();
        public ReactiveCommand MinusCommand { get; } = new ReactiveCommand();

        public ReactiveCommand PaletteDetailsPlusCommand { get; } = new ReactiveCommand();
        public ReactiveCommand PaletteDetailsMinusCommand { get; } = new ReactiveCommand();
        public ReactiveCommand PaletteDetailsUpdateCommand { get; } = new ReactiveCommand();
        public ReactiveCommand PaletteDetailsAddCommand { get; } = new ReactiveCommand();

        private void PaletteDetailsAddCommandExecute()
        {
            var paletteDetailsSave = new PaletteDetailsEntity(_paletteid,
                                                                            0,
                                                                            SelectedBom.Value.PartsNumber,
                                                                            SelectedBom.Value.PartsName,
                                                                            AddQuantity.Value,
                                                                            _isDeleted);

            _palettePartsRepository.PaletteDetailsSave(paletteDetailsSave);
            GetPaletteDetails();
        }

        private void PaletteDetailsPlusCommandExecute()
        {
            UpdateQuantity.Value++;
        }

        private void PaletteDetailsMinusommandExecute()
        {
            UpdateQuantity.Value--;
        }

        private void SelectedPaletteDetailsChangeExecute()
        {
            if (SelectedPaletteDetails.Value != null)
            {
                UpdateQuantity.Value = SelectedPaletteDetails.Value.PartsQuantity;
            }
        }

        private void PaletteDetailsSaveExecute()
        {
            var paletteDetailsSave = new PaletteDetailsEntity(SelectedPaletteDetails.Value.Paletteid,
                                                                                     SelectedPaletteDetails.Value.PaletteDetailsid,
                                                                                     SelectedPaletteDetails.Value.PartsNumber,
                                                                                     SelectedPaletteDetails.Value.PartsName,
                                                                                     UpdateQuantity.Value,
                                                                                     _isDeleted);

            _palettePartsRepository.PaletteDetailsSave(paletteDetailsSave);
            GetPaletteDetails();
        }
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

        private void GetPaletteDetails()
        {
            PaletteDetails.Value = new ObservableCollection<PaletteDetailsEntity>(_palettePartsRepository.GetPaletteDetails(_paletteid));
        }
    }
}
