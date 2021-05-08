using KGroupWorkSystem.Domain.Entities;
using KGroupWorkSystem.Domain.Repositories;
using KGroupWorkSystem.Domain.services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KGroupWorkSystem.Domain.Entities.WorkEntity;

namespace KGroupWorkSystem.Infrastructure.SQLServer
{
    public sealed class TimeManagementSQLServer : ITimeManagementRepository //アクセス修飾子　変更
    {
        public List<WorkEntity> GetWorks()
        {
            var parameters = new List<SqlParameter>();
            var list = new List<WorkEntity>();
            var sql = @"
select
    block.[work_block_id]
    ,block.[work_block_name]
    ,section.[work_section_id]
    ,section.[work_section_name]
    ,activity.[work_activity_id]
    ,activity.[work_activity_name]

from [KGWS].[dbo].[work_activity] activity

inner join  [KGWS].[dbo].[work_section] section
on activity.[work_section_id]=section.[work_section_id]

inner join  [KGWS].[dbo].[work_block] block
on section.[work_block_id]=block.[work_block_id]
";
            parameters.Clear();

            SQLServerHelper.Query(
                sql,
                parameters.ToArray(),
                reader =>
                {
                    list.Add(new WorkEntity(
                        Convert.ToInt32(reader["work_block_id"]),
                        Convert.ToString(reader["work_block_name"]),
                        Convert.ToInt32(reader["work_section_id"]),
                        Convert.ToString(reader["work_section_name"]),
                        Convert.ToInt32(reader["work_activity_id"]),
                        Convert.ToString(reader["work_activity_name"])));
                });
            return list;
        }

        public void ActiityChangeSave(PerformanceEntity performance)
        {
            var parameters = new List<SqlParameter>();

                var sql = @"
    insert into
        [KGWS].[dbo].[work_action]
        (
         [block_name]
        ,[section_name]
        ,[activity_name]
        )
    values
        (
         @block_name
        ,@section_name
        ,@activity_name
        )
    select SCOPE_IDENTITY() as id
    ";
                parameters.Clear();
                parameters.Add(new SqlParameter("@block_name", performance.WorkEntity.WorkBlockName));
                parameters.Add(new SqlParameter("@section_name", performance.WorkEntity.WorkSectionName));
                parameters.Add(new SqlParameter("@activity_name", performance.WorkEntity.WorkActivityName));
                var id = 0;
                SQLServerHelper.Query(
                                                    sql,
                                                    parameters.ToArray(),
                                                    reader =>
                                                    {
                                                        id = Convert.ToInt32(reader["id"]);
                                                    });

                sql = @"
    insert into
        [KGWS].[dbo].[Time]
        (
         [start_time]
        ,[end_time]
        ,[action_id]
        ,[milliseconds]
        ,[seconds]
        ,[minutes]
        ,[hours]
        ,[days]
        ,[total_milliseconds]
        ,[total_seconds]
        ,[total_minutes]
        ,[total_hours]
        ,[total_days]
        )
    values
        (
         @start_time
        ,@end_time
        ,@action_id
        ,@milliseconds
        ,@seconds
        ,@minutes
        ,@hours
        ,@days
        ,@total_milliseconds
        ,@total_seconds
        ,@total_minutes
        ,@total_hours
        ,@total_days
        )
    ";
                parameters.Clear();
                parameters.Add(new SqlParameter("@start_time", performance.StartTime));
                parameters.Add(new SqlParameter("@end_time", performance.EndTime));
                parameters.Add(new SqlParameter("@action_id", id));
                parameters.Add(new SqlParameter("@milliseconds", performance.TimeSpanMs));
                parameters.Add(new SqlParameter("@seconds", performance.TimeSpanS));
                parameters.Add(new SqlParameter("@minutes", performance.TimeSpanM));
                parameters.Add(new SqlParameter("@hours", performance.TimeSpanH));
                parameters.Add(new SqlParameter("@days", performance.TimeSpanD));
                parameters.Add(new SqlParameter("@total_milliseconds", performance.TotalTimeMs));
                parameters.Add(new SqlParameter("@total_seconds", performance.TotalTimeS));
                parameters.Add(new SqlParameter("@total_minutes", performance.TotalTimeM));
                parameters.Add(new SqlParameter("@total_hours", performance.TotalTimeH));
                parameters.Add(new SqlParameter("@total_days", performance.TotalTimeD));
                SQLServerHelper.Execute(sql, parameters.ToArray());
        }
    }
}
