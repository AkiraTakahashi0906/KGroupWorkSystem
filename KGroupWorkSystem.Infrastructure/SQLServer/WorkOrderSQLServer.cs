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
    public sealed class WorkOrderSQLServer : IWorkOrderRepository
    {
        public List<WorkingEntity> GetWorkingData()
        {
            StringBuilder query = new StringBuilder();
            var parameters = new List<SqlParameter>();
            var list = new List<WorkingEntity>();

            query.AppendLine("select");
            query.AppendLine("        [id]");
            query.AppendLine("        ,[workId]");
            query.AppendLine("        ,[workerId]");
            query.AppendLine("        ,[workOpId]");
            query.AppendLine("        ,[workTitle]");
            query.AppendLine("        ,[workName]");
            query.AppendLine("        ,[workDetails]");
            query.AppendLine("        ,[caution]");
            query.AppendLine("        ,[isDone]");
            query.AppendLine("        ,[isSync]");
            query.AppendLine("        ,[isCurrent]");
            query.AppendLine("from");
            query.AppendLine("        [KGWS].[dbo].[Working]");
            //query.AppendLine("where");
            //query.AppendLine("        [SQKID]=@SQKID");

            parameters.Clear();
            //parameters.Add(new SqlParameter("@SQKID", sqkId));

            SQLServerHelper.Query(
                query.ToString(),
                parameters.ToArray(),
                reader =>
                {
                    list.Add(new WorkingEntity(
                        Convert.ToInt32(reader["id"]),
                        Convert.ToInt32(reader["workId"]),
                        Convert.ToInt32(reader["workerId"]),
                        Convert.ToInt32(reader["workOpId"]),
                        Convert.ToString(reader["workTitle"]),
                        Convert.ToString(reader["workName"]),
                        Convert.ToString(reader["workDetails"]),
                        Convert.ToString(reader["caution"]),
                        Convert.ToBoolean(reader["isDone"]),
                        Convert.ToBoolean(reader["isSync"]),
                        Convert.ToBoolean(reader["isCurrent"])));
                });
            return list;
        }

        public void Registration()
        {
            throw new NotImplementedException();
        }

        public void ReStart()
        {
            StringBuilder query = new StringBuilder();
            var parameters = new List<SqlParameter>();

            query.AppendLine("update");
            query.AppendLine("        [KGWS].[dbo].[Working]");
            query.AppendLine("set [isDone]='false'");

            parameters.Clear();
            SQLServerHelper.Execute(query.ToString(), parameters.ToArray());
        }

        public void ToNext(int workId, int workerId)
        {
            StringBuilder query = new StringBuilder();
            var parameters = new List<SqlParameter>();
            var id = GetWorkingId(workId, workerId);

            query.AppendLine("update");
            query.AppendLine("        [KGWS].[dbo].[Working]");
            query.AppendLine("set [isDone]='true'");
            query.AppendLine("where");
            query.AppendLine("        [id]=@id");

            parameters.Clear();
            parameters.Add(new SqlParameter("@id", id));
            SQLServerHelper.Execute(query.ToString(), parameters.ToArray());
        }

        private int GetWorkingId(int workId, int workerId)
        {
            StringBuilder query = new StringBuilder();
            var parameters = new List<SqlParameter>();
            int id=0; 

            query.AppendLine("select top 1");
            query.AppendLine("        [id]");
            query.AppendLine("from");
            query.AppendLine("        [KGWS].[dbo].[Working]");
            query.AppendLine("where");
            query.AppendLine("        [workId]=@workId");
            query.AppendLine("and");
            query.AppendLine("        [workerId]=@workerId");
            query.AppendLine("and");
            query.AppendLine("        [isDone]='false'");
            query.AppendLine("order by [workOpId] ");

            parameters.Clear();
            parameters.Add(new SqlParameter("@workId", workId));
            parameters.Add(new SqlParameter("@workerId", workerId));

            SQLServerHelper.Query(
                query.ToString(),
                parameters.ToArray(),
                reader =>
                {
                    id = Convert.ToInt32(reader["id"]);
                });
            return id;
        }
    }
}
