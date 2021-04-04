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
    public class PalettePartsListViewModel : BindableBase 
    {
        IPalettePartsRepository _palettePartsRepository;
        public PalettePartsListViewModel()
        {
            _palettePartsRepository = new PalettePartsSQLServer();
            GetPalettes();
            UpdateCommand.Subscribe(_ => Update());
            DeleteCommand.Subscribe(_ => Delete());
            AddCommand.Subscribe(_ => AddCommandExecute());
            SelectedPalette.Subscribe(_ => SelectedPaletteChangeExecute());
        }
        public ReactivePropertySlim<ObservableCollection<PaletteEntity>> Palettes { get; } 
                            = new ReactivePropertySlim<ObservableCollection<PaletteEntity>>();
        public ReactivePropertySlim<PaletteEntity> SelectedPalette { get; } = new ReactivePropertySlim<PaletteEntity>(mode: ReactivePropertyMode.None);
        public ReactivePropertySlim<string> SelectedUserIdText { get; } = new ReactivePropertySlim<string>(string.Empty);
        public ReactivePropertySlim<string> SelectedPaletteNameText { get; } = new ReactivePropertySlim<string>(string.Empty);
        public ReactivePropertySlim<string> SelectedUseSegText { get; } = new ReactivePropertySlim<string>(string.Empty);
        public ReactivePropertySlim<string> SelectedUsePlaceText { get; } = new ReactivePropertySlim<string>(string.Empty);
        public ReactiveCommand UpdateCommand { get; } = new ReactiveCommand();
        public ReactiveCommand DeleteCommand { get; } = new ReactiveCommand();
        public ReactiveCommand AddCommand { get; } = new ReactiveCommand();

        private void AddCommandExecute()
        {
            var deleteParameter = false;
            var add = new PaletteEntity(0,
                                                     1,
                                                     SelectedPaletteNameText.Value,
                                                     SelectedUseSegText.Value,
                                                     SelectedUsePlaceText.Value,
                                                     deleteParameter);
            _palettePartsRepository.PaletteSave(add);
            GetPalettes();
        }

        private void SelectedPaletteChangeExecute()
        {
            if (SelectedPalette.Value != null)
            {
                SelectedUserIdText.Value = SelectedPalette.Value.UserId.ToString();
                SelectedPaletteNameText.Value = SelectedPalette.Value.PaletteName;
                SelectedUseSegText.Value = SelectedPalette.Value.UseSeg;
                SelectedUsePlaceText.Value = SelectedPalette.Value.UsePlace;
            }
        }

        private void Delete()
        {
            var deleteParameter = true;
            var delete = new PaletteEntity(SelectedPalette.Value.PaletteId,
                                                          SelectedPalette.Value.UserId,
                                                          SelectedPalette.Value.PaletteName,
                                                          SelectedPalette.Value.UseSeg,
                                                          SelectedPalette.Value.UsePlace,
                                                          deleteParameter);
            _palettePartsRepository.PaletteSave(delete);
            GetPalettes();
        }

        private void Update()
        {
            var update = new PaletteEntity(SelectedPalette.Value.PaletteId,
                                                           Convert.ToInt32(SelectedUserIdText.Value),
                                                           SelectedPaletteNameText.Value,
                                                           SelectedUseSegText.Value,
                                                           SelectedUsePlaceText.Value,
                                                           SelectedPalette.Value.IsDeleted);
            _palettePartsRepository.PaletteSave(update);
            GetPalettes();
        }

        private void GetPalettes()
        {
            Palettes.Value = new ObservableCollection<PaletteEntity>(_palettePartsRepository.GetPalettes());
        }

    }
}
