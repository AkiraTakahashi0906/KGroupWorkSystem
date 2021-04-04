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
                                               string assyNumber,
                                               string partsNumber,
                                               string partsName,
                                               int partsQuantity,
                                               string placeKey,
                                               string subAssemblyKey,
                                               bool isDeleted)
        {
            Paletteid = paletteid;
            PaletteDetailsid = paletteDetailsid;
            AssyNumber = assyNumber;
            PartsNumber = partsNumber;
            PartsName = partsName;
            PartsQuantity = partsQuantity;
            PlaceKey = placeKey;
            SubAssemblyKey = subAssemblyKey;
            IsDeleted = isDeleted;
        }
        public int Paletteid { get;}
        public int PaletteDetailsid { get; }
        public string AssyNumber { get; }
        public string PartsNumber { get; }
        public string PartsName { get; }
        public int PartsQuantity { get; }
        public string PlaceKey { get; }
        public string SubAssemblyKey { get; }
        public bool IsDeleted { get; }
    }
}
