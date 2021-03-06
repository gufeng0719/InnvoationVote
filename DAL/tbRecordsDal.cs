using Dapper;
using System.Collections.Generic;
using System.Linq;
using IDAL;
using Model.DBModel;
using System.Text;

namespace DAL
{
    /// <summary>
    ///   数据访问层
    /// </summary>
    public partial class tbRecordsDal : ItbRecordsDal
    {
        public bool Exists(int primaryKey)
        {
            var strSql = "SELECT COUNT(1) FROM InnovationVote.dbo.[tbRecords] WHERE 1 = @primaryKey";
            var parameters = new { primaryKey };
            return DbClient.Excute(strSql, parameters) > 0;
        }

        public bool ExistsByWhere(string where)
            => DbClient.ExecuteScalar<int>($"SELECT COUNT(1) FROM InnovationVote.dbo.[tbRecords] WHERE 1 = 1 {where};") > 0;

        public int Add(tbRecords model)
        {
            var strSql = new StringBuilder();
            strSql.Append("INSERT INTO InnovationVote.dbo.[tbRecords] (");
            strSql.Append("Prodjects,OpenId,RecordDate");
            strSql.Append(") VALUES (");
            strSql.Append("@Prodjects,@OpenId,@RecordDate);");
            strSql.Append("SELECT @@IDENTITY");
            return DbClient.ExecuteScalar<int>(strSql.ToString(), model);
        }

        public bool Update(tbRecords model)
        {
            var strSql = new StringBuilder();
            strSql.Append("UPDATE InnovationVote.dbo.[tbRecords] SET ");
            strSql.Append("Prodjects = @Prodjects,OpenId = @OpenId,RecordDate = @RecordDate");
            strSql.Append(" WHERE RecordID = @RecordID");
            return DbClient.Excute(strSql.ToString(), model) > 0;
        }

        public bool Update(Dictionary<tbRecordsEnum, object> updates, string where)
        {
            var strSql = new StringBuilder();
            strSql.Append("UPDATE InnovationVote.dbo.[tbRecords] SET ");
            var para = new DynamicParameters();
            foreach (var update in updates)
            {
                strSql.Append($" {update.Key} = @{update.Key},");
                para.Add(update.Key.ToString(), update.Value);
            }
            strSql.Remove(strSql.Length - 1, 1);
            strSql.Append($" WHERE 1=1 {where}");
            return DbClient.Excute(strSql.ToString(), para) > 0;
        }

        public bool Delete(int primaryKey)
        {
            var strSql = "DELETE FROM InnovationVote.dbo.[tbRecords] WHERE RecordID = @primaryKey";
            return DbClient.Excute(strSql, new { primaryKey }) > 0;
        }

        public int DeleteByWhere(string where)
            => DbClient.Excute($"DELETE FROM InnovationVote.dbo.[tbRecords] WHERE 1 = 1 {where}");

        public tbRecords GetModel(int primaryKey)
        {
            var strSql = "SELECT * FROM InnovationVote.dbo.[tbRecords] WHERE RecordID = @primaryKey";
            return DbClient.Query<tbRecords>(strSql, new { primaryKey }).FirstOrDefault();
        }

        public List<tbRecords> GetModelList(string where)
        {
            var strSql = $"SELECT * FROM InnovationVote.dbo.[tbRecords] WHERE 1 = 1 {where}";
            return DbClient.Query<tbRecords>(strSql).ToList();
        }

        public List<tbRecords> GetModelPage(tbRecordsEnum order, string where, int pageIndex, int pageSize, out int total)
        {
            var strSql = new StringBuilder();
            strSql.Append($"SELECT * FROM ( SELECT TOP ({pageSize})");
            strSql.Append($"ROW_NUMBER() OVER ( ORDER BY {order} DESC ) AS ROWNUMBER,* ");
            strSql.Append(" FROM  InnovationVote.dbo.[tbRecords] ");
            strSql.Append($" WHERE 1 = 1 {where} ");
            strSql.Append(" ) A");
            strSql.Append($" WHERE ROWNUMBER BETWEEN {(pageIndex - 1) * pageSize + 1} AND {pageIndex * pageSize}; ");
            total = DbClient.ExecuteScalar<int>($"SELECT COUNT(1) FROM InnovationVote.dbo.[tbRecords] WHERE 1 = 1 {where};");
            return DbClient.Query<tbRecords>(strSql.ToString()).ToList();
        }

    }
}
