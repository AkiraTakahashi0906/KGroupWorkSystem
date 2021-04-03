using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.Entities
{
    public sealed class PaletteDetailsEntity
    {
        public PaletteDetailsEntity(int paletteid,
                                               int paletteDetailsid,
                                               string partsNumber,
                                               string partsName,
                                               int partsQuantity,
                                               bool isDeleted)
        {
            Paletteid = paletteid;
            PaletteDetailsid = paletteDetailsid;
            PartsNumber = partsNumber;
            PartsName = partsName;
            PartsQuantity = partsQuantity;
            IsDeleted = isDeleted;
        }
        public int Paletteid { get;}
        public int PaletteDetailsid { get; }
        public string PartsNumber { get; }
        public string PartsName { get; }
        public int PartsQuantity { get; }
        public bool IsDeleted { get; }
    }
}
