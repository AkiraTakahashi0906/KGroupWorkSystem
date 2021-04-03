using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.Entities
{
    public sealed class PaletteEntity
    {
        public PaletteEntity(int paletteId,
                                     int userid,
                                     string paletteName,
                                     bool isDeleted)
        {
            PaletteId = paletteId;
            UserId = userid;
            PaletteName = paletteName;
            IsDeleted = isDeleted;
        }
        public int PaletteId { get; }
        public int UserId { get;}
        public string PaletteName { get; }
        public bool IsDeleted { get; }
    }
}
