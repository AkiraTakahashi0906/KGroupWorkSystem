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
        void PaletteSave(PaletteEntity palette);
        List<PaletteDetailsEntity> GetPaletteDetails(int paletteid);
        void PaletteDetailsSave(PaletteDetailsEntity paletteDetails);
        void PaletteDetailsDelete(int paletteDetailsid);
    }
}
