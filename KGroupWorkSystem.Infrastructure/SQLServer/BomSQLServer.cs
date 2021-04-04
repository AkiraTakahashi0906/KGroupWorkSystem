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
    public class BomSQLServer : IBomRepository//アクセス修飾子　変更
    {
        public List<BomPartsEntity> GetBomParts(string assyNumber)
        {
            var parameters = new List<SqlParameter>();
            var list = new List<BomPartsEntity>();
            var sql = @"
select
    bom.[assy_number]
    ,bom.[parts_number]
    ,bom.[parts_name]
    ,bom.[parts_quantity]
    ,isnull(details_sum.[parts_quantity],0) as details_sum_parts_quantity
from [KGWS].[dbo].[bom] bom

left join (
    select 
        details.[parts_number]
        ,sum(details.[parts_quantity]) as parts_quantity
    from [KGWS].[dbo].[palette_details] details
    where 
        details.[is_deleted]=@is_deleted
    group by details.[parts_number]
    ) as details_sum

on bom.[parts_number]=details_sum.[parts_number]

where 
    bom.[assy_number]=@assy_number
";
            parameters.Clear();
            parameters.Add(new SqlParameter("@assy_number", assyNumber));
            parameters.Add(new SqlParameter("@is_deleted", false));

            SQLServerHelper.Query(
                sql,
                parameters.ToArray(),
                reader =>
                {
                    list.Add(new BomPartsEntity(
                        Convert.ToString(reader["assy_number"]),
                        Convert.ToString(reader["parts_number"]),
                        Convert.ToString(reader["parts_name"]),
                        Convert.ToInt32(reader["parts_quantity"]),
                        Convert.ToInt32(reader["details_sum_parts_quantity"])));
                });
            return list;
        }
    }
}
