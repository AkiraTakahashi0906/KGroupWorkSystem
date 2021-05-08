using KGroupWorkSystem.Domain.Entities;
using KGroupWorkSystem.Domain.Repositories;
using KGroupWorkSystem.Domain.services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KGroupWorkSystem.Domain.Entities.PerformanceEntity;
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

        public void ActiityChange(ActivityName activityName)
        {
            var time = TimeManagementServices.GetInstance();
            var SaveTime = time.ActiityChange(activityName);
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
                parameters.Add(new SqlParameter("@block_name", SaveTime.WorkEntity.WorkBlockName));
                parameters.Add(new SqlParameter("@section_name", SaveTime.WorkEntity.WorkSectionName));
                parameters.Add(new SqlParameter("@activity_name", SaveTime.WorkEntity.WorkActivityName));
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
                parameters.Add(new SqlParameter("@start_time", SaveTime.StartTime));
                parameters.Add(new SqlParameter("@end_time", SaveTime.EndTime));
                parameters.Add(new SqlParameter("@action_id", id));
                parameters.Add(new SqlParameter("@milliseconds", SaveTime.TimeSpanMs));
                parameters.Add(new SqlParameter("@seconds", SaveTime.TimeSpanS));
                parameters.Add(new SqlParameter("@minutes", SaveTime.TimeSpanM));
                parameters.Add(new SqlParameter("@hours", SaveTime.TimeSpanH));
                parameters.Add(new SqlParameter("@days", SaveTime.TimeSpanD));
                parameters.Add(new SqlParameter("@total_milliseconds", SaveTime.TotalTimeMs));
                parameters.Add(new SqlParameter("@total_seconds", SaveTime.TotalTimeS));
                parameters.Add(new SqlParameter("@total_minutes", SaveTime.TotalTimeM));
                parameters.Add(new SqlParameter("@total_hours", SaveTime.TotalTimeH));
                parameters.Add(new SqlParameter("@total_days", SaveTime.TotalTimeD));
                SQLServerHelper.Execute(sql, parameters.ToArray());
        }
    }
}
