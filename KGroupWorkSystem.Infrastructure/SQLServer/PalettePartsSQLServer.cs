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
    ,[is_deleted]
from [KGWS].[dbo].[palettes]
where 
    [is_deleted]=@is_deleted
";
            parameters.Clear();
            parameters.Add(new SqlParameter("@is_deleted", false));

            SQLServerHelper.Query(
                sql,
                parameters.ToArray(),
                reader =>
                {
                    list.Add(new PaletteEntity(
                        Convert.ToInt32(reader["palette_id"]),
                        Convert.ToInt32(reader["user_id"]),
                        Convert.ToString(reader["palette_name"]),
                        Convert.ToBoolean(reader["is_deleted"])));
                });
            return list;
        }

        public void PaletteSave(PaletteEntity palette)
        {
            var parameters = new List<SqlParameter>();
            var sql = @"
update
    [KGWS].[dbo].[palettes]
set
     [user_id]=@user_id
    ,[palette_name]=@palette_name
    ,[is_deleted]=@is_deleted
where
    [palette_id]=@palette_id
";

            parameters.Clear();
            parameters.Add(new SqlParameter("@palette_id", palette.PaletteId));
            parameters.Add(new SqlParameter("@user_id", palette.UserId));
            parameters.Add(new SqlParameter("@palette_name", palette.PaletteName));
            parameters.Add(new SqlParameter("@is_deleted", palette.IsDeleted));
            SQLServerHelper.Execute(sql, parameters.ToArray());
        }
    }
}
