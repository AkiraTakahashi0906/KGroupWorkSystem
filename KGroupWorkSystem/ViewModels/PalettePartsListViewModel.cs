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
            Palettes.Value = new ObservableCollection<PaletteEntity>(_palettePartsRepository.GetPalettes());
        }
        public ReactivePropertySlim<ObservableCollection<PaletteEntity>> Palettes { get; } 
                            = new ReactivePropertySlim<ObservableCollection<PaletteEntity>>();
    }
}
