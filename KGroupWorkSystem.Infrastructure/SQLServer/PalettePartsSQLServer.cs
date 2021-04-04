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

        public void PaletteDetailsUpdate(PaletteDetailsEntity paletteDetails)
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

        public void PaletteDetailsInsert(PaletteDetailsEntity paletteDetails)
        {
            var parameters = new List<SqlParameter>();
            var sql = @"
insert into
    [KGWS].[dbo].[palette_details]
    (
     [palette_id]
    ,[assy_number]
    ,[parts_number]
    ,[parts_name]
    ,[parts_quantity]
    ,[place_key]
    ,[subAssembly_key]
    ,[is_deleted]
    )
values
    (
     @palette_id
    ,@assy_number
    ,@parts_number
    ,@parts_name
    ,@parts_quantity
    ,@place_key
    ,@subAssembly_key
    ,@is_deleted
    )
";
            parameters.Clear();
            parameters.Add(new SqlParameter("@palette_id", paletteDetails.Paletteid));
            parameters.Add(new SqlParameter("@assy_number", paletteDetails.AssyNumber));
            parameters.Add(new SqlParameter("@parts_number", paletteDetails.PartsNumber));
            parameters.Add(new SqlParameter("@parts_name", paletteDetails.PartsName));
            parameters.Add(new SqlParameter("@parts_quantity", paletteDetails.PartsQuantity));
            parameters.Add(new SqlParameter("@place_key", paletteDetails.PlaceKey));
            parameters.Add(new SqlParameter("@subAssembly_key", paletteDetails.SubAssemblyKey));
            parameters.Add(new SqlParameter("@is_deleted", paletteDetails.IsDeleted));
            SQLServerHelper.Execute(sql, parameters.ToArray());
        }

        public void PaletteDetailsSave(PaletteDetailsEntity paletteDetails)
        {
            var parameters = new List<SqlParameter>();
            var sql = @"
update
    [KGWS].[dbo].[palette_details]
set
    [parts_quantity]=[parts_quantity]+@parts_quantity
where
    [parts_name]=@parts_name
and
    [parts_quantity]=@parts_quantity
";

            parameters.Clear();
            parameters.Add(new SqlParameter("@parts_number", paletteDetails.PartsNumber));
            parameters.Add(new SqlParameter("@parts_name", paletteDetails.PartsName));
            parameters.Add(new SqlParameter("@parts_quantity", paletteDetails.PartsQuantity));
            SQLServerHelper.Execute(sql, parameters.ToArray());
        }

        public List<PaletteDetailsEntity> GetPaletteDetails(int paletteid)
        {
            var parameters = new List<SqlParameter>();
            var list = new List<PaletteDetailsEntity>();
            var sql = @"
select
    [palette_details_id]
    ,[palette_id]
    ,[assy_number]
    ,[parts_number]
    ,[parts_name]
    ,[parts_quantity]
    ,[place_key]
    ,[subAssembly_key]
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
                        Convert.ToString(reader["assy_number"]),
                        Convert.ToString(reader["parts_number"]),
                        Convert.ToString(reader["parts_name"]),
                        Convert.ToInt32(reader["parts_quantity"]),
                        Convert.ToString(reader["place_key"]),
                        Convert.ToString(reader["subAssembly_key"]),
                        Convert.ToBoolean(reader["is_deleted"])));
                });
            return list;
        }

        public void PaletteDetailsDelete(int paletteDetailsid)
        {
            var parameters = new List<SqlParameter>();
            var sql = @"
update
    [KGWS].[dbo].[palette_details]
set
     [is_deleted]=@is_deleted
where
    [palette_details_id]=@palette_details_id
";

            parameters.Clear();
            parameters.Add(new SqlParameter("@palette_details_id", paletteDetailsid));
            parameters.Add(new SqlParameter("@is_deleted", true));
            SQLServerHelper.Execute(sql, parameters.ToArray());
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
    ,[use_seg]
    ,[use_place]
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
                        Convert.ToString(reader["use_seg"]),
                        Convert.ToString(reader["use_place"]),
                        Convert.ToBoolean(reader["is_deleted"])));
                });
            return list;
        }

        public void PaletteSave(PaletteEntity palette)
        {
            if (IsExistsPalette(palette))
            {
                var parameters = new List<SqlParameter>();
                var sql = @"
update
    [KGWS].[dbo].[palettes]
set
     [user_id]=@user_id
    ,[palette_name]=@palette_name
    ,[use_seg]=@use_seg
    ,[use_place]=@use_place
    ,[is_deleted]=@is_deleted
where
    [palette_id]=@palette_id
";

                parameters.Clear();
                parameters.Add(new SqlParameter("@palette_id", palette.PaletteId));
                parameters.Add(new SqlParameter("@user_id", palette.UserId));
                parameters.Add(new SqlParameter("@palette_name", palette.PaletteName));
                parameters.Add(new SqlParameter("@use_seg", palette.UseSeg));
                parameters.Add(new SqlParameter("@use_place", palette.UsePlace));
                parameters.Add(new SqlParameter("@is_deleted", palette.IsDeleted));
                SQLServerHelper.Execute(sql, parameters.ToArray());
            }
            else
            {
                var parameters = new List<SqlParameter>();
                var sql = @"
insert into
    [KGWS].[dbo].[palettes]
    (
     [user_id]
    ,[palette_name]
    ,[use_seg]
    ,[use_place]
    ,[is_deleted]
    )
values
    (
     @user_id
    ,@palette_name
    ,@use_seg
    ,@use_place
    ,@is_deleted
    )
";
                parameters.Clear();
                parameters.Add(new SqlParameter("@user_id", palette.UserId));
                parameters.Add(new SqlParameter("@palette_name", palette.PaletteName));
                parameters.Add(new SqlParameter("@use_seg", palette.UseSeg));
                parameters.Add(new SqlParameter("@use_place", palette.UsePlace));
                parameters.Add(new SqlParameter("@is_deleted", palette.IsDeleted));
                SQLServerHelper.Execute(sql, parameters.ToArray());
            }
        }

        private bool IsExistsPalette(PaletteEntity palette)
        {
            var parameters = new List<SqlParameter>();
            bool IsExistsPalette = false;
            int paletteCount = 0;
            var sql = @"
select
    count([palette_id]) as palette_id
from     [KGWS].[dbo].[palettes]
where
    [palette_id]=@palette_id
";
            parameters.Clear();
            parameters.Add(new SqlParameter("@palette_id", palette.PaletteId));

            SQLServerHelper.Query(
                                                sql,
                                                parameters.ToArray(),
                                                reader =>
                                                {
                                                    paletteCount = Convert.ToInt32(reader["palette_id"]);
                                                });
            if (paletteCount == 0)
            {
                IsExistsPalette = false;
            }
            else
            {
                IsExistsPalette = true;
            }
            return IsExistsPalette;
        }
    }
}
