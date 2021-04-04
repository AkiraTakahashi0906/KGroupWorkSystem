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
        private bool _isDeleted = false;
        private string _defaultPlaceKey = "未登録";
        private string _defaultSubAssemblyKey = "無し";

        public PalettePartsDetailsViewModel()
        {
            _palettePartsRepository = new PalettePartsSQLServer();
            _bomRepository = new BomSQLServer();
            AssyNumbers.Value = new ObservableCollection<string>();

            GetAssyNumbers();
            GetPalettes();
            SelectedAssyNumber.Subscribe(_ => SelectedAssyNumberChangeEcecute());
            SelectedPaletteDetails.Subscribe(_ => SelectedPaletteDetailsChangeExecute());
            SelectedBom.Subscribe(_ => SelectedBomChangeExecute());
            PlusCommand.Subscribe(_ => PlusCommandExecute());
            MinusCommand.Subscribe(_ => MinusCommandExecute());
            SelectedPalette.Subscribe(_ => SearchCommandExecute());
            PaletteDetailsPlusCommand.Subscribe(_ => PaletteDetailsPlusCommandExecute());
            PaletteDetailsMinusCommand.Subscribe(_ => PaletteDetailsMinusommandExecute());
            PaletteDetailsUpdateCommand.Subscribe(_ => PaletteDetailsSaveExecute());
            PaletteDetailsAddCommand.Subscribe(_ => PaletteDetailsAddCommandExecute());
            PaletteDetailsDeleteCommand.Subscribe(_ => PaletteDetailsDeleteCommandExecute());
        }
        public ReactivePropertySlim<ObservableCollection<string>> AssyNumbers { get; }
            = new ReactivePropertySlim<ObservableCollection<string>>();

        public ReactivePropertySlim<string> SelectedAssyNumber { get; }
            = new ReactivePropertySlim<string>(mode: ReactivePropertyMode.None);

        public ReactivePropertySlim<ObservableCollection<BomPartsEntity>> Boms { get; }
                    = new ReactivePropertySlim<ObservableCollection<BomPartsEntity>>();
        public ReactivePropertySlim<BomPartsEntity> SelectedBom { get; }
            = new ReactivePropertySlim<BomPartsEntity>(mode: ReactivePropertyMode.None);
        public ReactivePropertySlim<ObservableCollection<PaletteEntity>> Palettes { get; }
                    = new ReactivePropertySlim<ObservableCollection<PaletteEntity>>();
        public ReactivePropertySlim<PaletteEntity> SelectedPalette { get; } = new ReactivePropertySlim<PaletteEntity>(mode: ReactivePropertyMode.None);
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
        public ReactiveCommand PaletteDetailsDeleteCommand { get; } = new ReactiveCommand();

        private void PaletteDetailsDeleteCommandExecute()
        {
            _palettePartsRepository.PaletteDetailsDelete(SelectedPaletteDetails.Value.PaletteDetailsid);
            UpdateQuantity.Value = 0;
            GetPaletteDetails();
            GetBom();
        }

        private void GetAssyNumbers()
        {
            AssyNumbers.Value.Add("1");
            AssyNumbers.Value.Add("2");
        }

        private void SelectedAssyNumberChangeEcecute()
        {
            GetBom();
        }

        private void SearchCommandExecute()
        {
            GetPaletteDetails();
        }

        private void PaletteDetailsAddCommandExecute()
        {
            // 0 のときは例外を投げる
            var paletteDetailsSave = new PaletteDetailsEntity(SelectedPalette.Value.PaletteId,
                                                                            0,
                                                                            SelectedBom.Value.AssyNumber,
                                                                            SelectedBom.Value.PartsNumber,
                                                                            SelectedBom.Value.PartsName,
                                                                            AddQuantity.Value,
                                                                            _defaultPlaceKey,
                                                                            _defaultSubAssemblyKey,
                                                                            _isDeleted);

            _palettePartsRepository.PaletteDetailsInsert(paletteDetailsSave);
            GetPaletteDetails();
            GetBom();
            AddQuantity.Value = 0;
        }

        private void PaletteDetailsPlusCommandExecute()
        {
            if (UpdateQuantity.Value < SelectedPaletteDetails.Value.PartsQuantity)
            {
                UpdateQuantity.Value++;
            }
        }

        private void PaletteDetailsMinusommandExecute()
        {
            if (UpdateQuantity.Value > 1)
            {
                UpdateQuantity.Value--;
            }
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
            // 0 のときは例外を投げる
            var paletteDetailsSave = new PaletteDetailsEntity(SelectedPaletteDetails.Value.Paletteid,
                                                                                     SelectedPaletteDetails.Value.PaletteDetailsid,
                                                                                     SelectedPaletteDetails.Value.AssyNumber,
                                                                                     SelectedPaletteDetails.Value.PartsNumber,
                                                                                     SelectedPaletteDetails.Value.PartsName,
                                                                                     UpdateQuantity.Value,
                                                                                     SelectedPaletteDetails.Value.PlaceKey,
                                                                                     SelectedPaletteDetails.Value.SubAssemblyKey,
                                                                                     _isDeleted);

            _palettePartsRepository.PaletteDetailsUpdate(paletteDetailsSave);
            GetPaletteDetails();
            GetBom();
        }
        private void PlusCommandExecute()
        {
            if (AddQuantity.Value < SelectedBom.Value.CanUsePartsQuantity)
            {
                AddQuantity.Value++;
            }
        }

        private void MinusCommandExecute()
        {
            if (AddQuantity.Value > 1)
            {
                AddQuantity.Value--;
            }
        }

        private void SelectedBomChangeExecute()
        {
            if (SelectedBom.Value != null)
            {
                AddQuantity.Value = SelectedBom.Value.CanUsePartsQuantity;
            }
        }

        private void GetPalettes()
        {
            Palettes.Value = new ObservableCollection<PaletteEntity>(_palettePartsRepository.GetPalettes());
        }

        private void GetPaletteDetails()
        {
            if (SelectedPalette.Value != null)
            {
                PaletteDetails.Value = new ObservableCollection<PaletteDetailsEntity>
                                (_palettePartsRepository.GetPaletteDetails(SelectedPalette.Value.PaletteId));
            }
        }

        private void GetBom()
        {
            if (SelectedAssyNumber.Value != null)
            {
                Boms.Value = new ObservableCollection<BomPartsEntity>(_bomRepository.GetBomParts(SelectedAssyNumber.Value));
            }
        }
    }
}
