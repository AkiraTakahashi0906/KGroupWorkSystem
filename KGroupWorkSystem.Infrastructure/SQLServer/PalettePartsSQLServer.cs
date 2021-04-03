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
        private int GetExistsCountPaletteDetails(PaletteDetailsEntity paletteDetails)
        {
            var parameters = new List<SqlParameter>();
            int isExistsCountPaletteDetails = 0;
            var sql = @"
select
    [parts_quantity]
from [KGWS].[dbo].[palette_details]
where 
    [palette_id]=@palette_id
and
    [parts_number]=@parts_number
and
    [is_deleted]=@is_deleted
";
            parameters.Clear();
            parameters.Add(new SqlParameter("@palette_id", paletteDetails.Paletteid));
            parameters.Add(new SqlParameter("@parts_number", paletteDetails.PartsNumber));
            parameters.Add(new SqlParameter("@is_deleted", paletteDetails.IsDeleted));

            SQLServerHelper.Query(
                                                sql,
                                                parameters.ToArray(),
                                                reader =>
                                                {
                                                    isExistsCountPaletteDetails = Convert.ToInt32(reader["parts_quantity"]);
                                                });
            return isExistsCountPaletteDetails;
        }

        public void PaletteDetailsSave(PaletteDetailsEntity paletteDetails)
        {
            var existsCount = GetExistsCountPaletteDetails(paletteDetails);
            if (existsCount == 0)
            {
                var parameters = new List<SqlParameter>();
                var sql = @"
insert into
    [KGWS].[dbo].[palette_details]
    (
     [palette_id]
    ,[parts_number]
    ,[parts_name]
    ,[parts_quantity]
    ,[is_deleted]
    )
values
    (
     @palette_id
    ,@parts_number
    ,@parts_name
    ,@parts_quantity
    ,@is_deleted
    )
";
                parameters.Clear();
                parameters.Add(new SqlParameter("@palette_id", paletteDetails.Paletteid));
                parameters.Add(new SqlParameter("@parts_number", paletteDetails.PartsNumber));
                parameters.Add(new SqlParameter("@parts_name", paletteDetails.PartsName));
                parameters.Add(new SqlParameter("@parts_quantity", paletteDetails.PartsQuantity));
                parameters.Add(new SqlParameter("@is_deleted", paletteDetails.IsDeleted));
                SQLServerHelper.Execute(sql, parameters.ToArray());
            }
            else
            {
                var parameters = new List<SqlParameter>();
                var sql = @"
update
    [KGWS].[dbo].[palette_details]
set
     [parts_number]=@parts_number
    ,[parts_name]=@parts_name
    ,[parts_quantity]=@parts_quantity
where
    [palette_details_id]=@palette_details_id
";

                parameters.Clear();
                parameters.Add(new SqlParameter("@palette_details_id", paletteDetails.PaletteDetailsid));
                parameters.Add(new SqlParameter("@parts_number", paletteDetails.PartsNumber));
                parameters.Add(new SqlParameter("@parts_name", paletteDetails.PartsName));
                parameters.Add(new SqlParameter("@parts_quantity", paletteDetails.PartsQuantity));
                SQLServerHelper.Execute(sql, parameters.ToArray());
            }
        }

        public List<PaletteDetailsEntity> GetPaletteDetails(int paletteid)
        {
            var parameters = new List<SqlParameter>();
            var list = new List<PaletteDetailsEntity>();
            var sql = @"
select
    [palette_details_id]
    ,[palette_id]
    ,[parts_number]
    ,[parts_name]
    ,[parts_quantity]
    ,[is_deleted]
from [KGWS].[dbo].[palette_details]
where 
    [palette_id]=@palette_id
and
    [is_deleted]=@is_deleted
order by [parts_number]
";
            parameters.Clear();
            parameters.Add(new SqlParameter("@palette_id", paletteid));
            parameters.Add(new SqlParameter("@is_deleted", false));

            SQLServerHelper.Query(
                sql,
                parameters.ToArray(),
                reader =>
                {
                    list.Add(new PaletteDetailsEntity(
                        Convert.ToInt32(reader["palette_id"]),
                        Convert.ToInt32(reader["palette_details_id"]),
                        Convert.ToString(reader["parts_number"]),
                        Convert.ToString(reader["parts_name"]),
                        Convert.ToInt32(reader["parts_quantity"]),
                        Convert.ToBoolean(reader["is_deleted"])));
                });
            return list;
        }

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
order by [palette_name]
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
