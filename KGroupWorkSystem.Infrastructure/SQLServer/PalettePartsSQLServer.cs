using KGroupWorkSystem.Domain.Entities;
using KGroupWorkSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Infrastructure.SQLServer
{
    public sealed class PalettePartsSQLServer : IPalettePartsRepository //アクセス修飾子　変更
    {
        public List<PaletteEntity> GetPalettes()
        {
            var parameters = new List<SqlParameter>();
            var list = new List<PaletteEntity>();
            var sql = @"
select
    [palette_id]
    ,[user_id]
    ,[palette_name]
from [KGWS].[dbo].[palettes]
";
            parameters.Clear();

            SQLServerHelper.Query(
                sql,
                parameters.ToArray(),
                reader =>
                {
                    list.Add(new PaletteEntity(
                        Convert.ToInt32(reader["palette_id"]),
                        Convert.ToInt32(reader["user_id"]),
                        Convert.ToString(reader["palette_name"])));
                });
            return list;
        }
    }
}
