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
    [assy_number]
    ,[parts_number]
    ,[parts_name]
    ,[parts_quantity]
from [KGWS].[dbo].[bom]
where 
    [assy_number]=@assy_number
";
            parameters.Clear();
            parameters.Add(new SqlParameter("@assy_number", assyNumber));

            SQLServerHelper.Query(
                sql,
                parameters.ToArray(),
                reader =>
                {
                    list.Add(new BomPartsEntity(
                        Convert.ToString(reader["assy_number"]),
                        Convert.ToString(reader["parts_number"]),
                        Convert.ToString(reader["parts_name"]),
                        Convert.ToInt32(reader["parts_quantity"])));
                });
            return list;
        }
    }
}
