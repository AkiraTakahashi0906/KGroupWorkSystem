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
        }
        public ReactivePropertySlim<ObservableCollection<PaletteEntity>> Palettes { get; } 
                            = new ReactivePropertySlim<ObservableCollection<PaletteEntity>>();
        public ReactivePropertySlim<PaletteEntity> SelectedPalette { get; } = new ReactivePropertySlim<PaletteEntity>();
        public ReactivePropertySlim<string> SelectedUserIdText { get; } = new ReactivePropertySlim<string>(string.Empty);
        public ReactivePropertySlim<string> SelectedPaletteNameText { get; } = new ReactivePropertySlim<string>(string.Empty);
        public ReactiveCommand UpdateCommand { get; } = new ReactiveCommand();
        public ReactiveCommand DeleteCommand { get; } = new ReactiveCommand();

        private void Delete()
        {
            var deleteParameter = true;
            var delete = new PaletteEntity(SelectedPalette.Value.PaletteId,
                                                          SelectedPalette.Value.UserId,
                                                          SelectedPalette.Value.PaletteName,
                                                          deleteParameter);
            _palettePartsRepository.PaletteSave(delete);
            GetPalettes();
        }

        private void Update()
        {
            var update = new PaletteEntity(SelectedPalette.Value.PaletteId,
                                                           Convert.ToInt32(SelectedUserIdText.Value),
                                                           SelectedPaletteNameText.Value,
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
