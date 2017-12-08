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
    public partial class tbProjectDal : ItbProjectDal
    {
        public bool Exists(int primaryKey)
        {
            var strSql = "SELECT COUNT(1) FROM InnovationVote.dbo.[tbProject] WHERE 1 = @primaryKey";
            var parameters = new { primaryKey };
            return DbClient.Excute(strSql, parameters) > 0;
        }

        public bool ExistsByWhere(string where)
            => DbClient.ExecuteScalar<int>($"SELECT COUNT(1) FROM InnovationVote.dbo.[tbProject] WHERE 1 = 1 {where};") > 0;

        public int Add(tbProject model)
        {
            var strSql = new StringBuilder();
            strSql.Append("INSERT INTO InnovationVote.dbo.[tbProject] (");
            strSql.Append("TypeId,TypeName,OrganName,ProjectName,ProjectIntro,ProjectImages,VoteNumber,ProjectState,ProjectRemark");
            strSql.Append(") VALUES (");
            strSql.Append("@TypeId,@TypeName,@OrganName,@ProjectName,@ProjectIntro,@ProjectImages,@VoteNumber,@ProjectState,@ProjectRemark);");
            strSql.Append("SELECT @@IDENTITY");
            return DbClient.ExecuteScalar<int>(strSql.ToString(), model);
        }

        public bool Update(tbProject model)
        {
            var strSql = new StringBuilder();
            strSql.Append("UPDATE InnovationVote.dbo.[tbProject] SET ");
            strSql.Append("TypeId = @TypeId,TypeName = @TypeName,OrganName = @OrganName,ProjectName = @ProjectName,ProjectIntro = @ProjectIntro,ProjectImages = @ProjectImages,VoteNumber = @VoteNumber,ProjectState = @ProjectState,ProjectRemark = @ProjectRemark");
            strSql.Append(" WHERE ProjectId = @ProjectId");
            return DbClient.Excute(strSql.ToString(), model) > 0;
        }

        public bool Update(Dictionary<tbProjectEnum, object> updates, string where)
        {
            var strSql = new StringBuilder();
            strSql.Append("UPDATE InnovationVote.dbo.[tbProject] SET ");
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
            var strSql = "DELETE FROM InnovationVote.dbo.[tbProject] WHERE ProjectId = @primaryKey";
            return DbClient.Excute(strSql, new { primaryKey }) > 0;
        }

        public int DeleteByWhere(string where)
            => DbClient.Excute($"DELETE FROM InnovationVote.dbo.[tbProject] WHERE 1 = 1 {where}");

        public tbProject GetModel(int primaryKey)
        {
            var strSql = "SELECT * FROM InnovationVote.dbo.[tbProject] WHERE ProjectId = @primaryKey";
            return DbClient.Query<tbProject>(strSql, new { primaryKey }).FirstOrDefault();
        }

        public List<tbProject> GetModelList(string where)
        {
            var strSql = $"SELECT * FROM InnovationVote.dbo.[tbProject] WHERE 1 = 1 {where}";
            return DbClient.Query<tbProject>(strSql).ToList();
        }

        public List<tbProject> GetModelPage(tbProjectEnum order, string where, int pageIndex, int pageSize, out int total)
        {
            var strSql = new StringBuilder();
            strSql.Append($"SELECT * FROM ( SELECT TOP ({pageSize})");
            strSql.Append($"ROW_NUMBER() OVER ( ORDER BY {order} DESC ) AS ROWNUMBER,* ");
            strSql.Append(" FROM  InnovationVote.dbo.[tbProject] ");
            strSql.Append($" WHERE 1 = 1 {where} ");
            strSql.Append(" ) A");
            strSql.Append($" WHERE ROWNUMBER BETWEEN {(pageIndex - 1) * pageSize + 1} AND {pageIndex * pageSize}; ");
            total = DbClient.ExecuteScalar<int>($"SELECT COUNT(1) FROM InnovationVote.dbo.[tbProject] WHERE 1 = 1 {where};");
            return DbClient.Query<tbProject>(strSql.ToString()).ToList();
        }

    }
}
