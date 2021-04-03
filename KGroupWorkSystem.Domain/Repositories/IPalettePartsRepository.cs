using KGroupWorkSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.Repositories
{
    public interface IPalettePartsRepository
    {
        List<PaletteEntity> GetPalettes();
        void RegistrationPalette(PaletteEntity palette);
    }
}
