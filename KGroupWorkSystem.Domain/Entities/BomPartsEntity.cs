using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.Entities
{
    public sealed class BomPartsEntity
    {
        public BomPartsEntity(string assyNumber,
                                         string partsNumber,
                                         string partsName,
                                         int partsQuantity,
                                         int usedPartsQuantity)
        {
            AssyNumber = assyNumber;
            PartsNumber = partsNumber;
            PartsName = partsName;
            PartsQuantity = partsQuantity;
            UsedPartsQuantity = usedPartsQuantity;
        }
        public string AssyNumber { get; }
        public string PartsNumber { get; }
        public string PartsName { get; }
        public int PartsQuantity { get; }
        public int UsedPartsQuantity { get; }
        public int CanUsePartsQuantity => PartsQuantity - UsedPartsQuantity;

    }
}
