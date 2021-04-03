using KGroupWorkSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.Repositories
{
    public interface IBomRepository
    {
        List<BomPartsEntity> GetBomParts(string assyNumber);
    }
}
