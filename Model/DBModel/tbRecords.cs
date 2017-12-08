using System;

namespace Model.DBModel
{
    /// <summary>
    /// 
    /// </summary>
    public class tbRecords : BaseModel
    {
        public static string PrimaryKey { get; set; } = "RecordID";
        public static string IdentityKey { get; set; } = "RecordID";

        /// <summary>
        /// 主键
        /// </summary>
        public int RecordID { get; set; }

        /// <summary>
        /// 项目编号多个以,隔开
        /// </summary>
        public string Prodjects { get; set; } = string.Empty;

        /// <summary>
        /// 微信公开id
        /// </summary>
        public string OpenId { get; set; } = string.Empty;

        /// <summary>
        /// 投票日期
        /// </summary>
        public DateTime RecordDate { get; set; } = ToDateTime("getdate");

    }


    public enum tbRecordsEnum
    {
        /// <summary>
        /// 主键
        /// </summary>
        RecordID,
        /// <summary>
        /// 项目编号多个以,隔开
        /// </summary>
        Prodjects,
        /// <summary>
        /// 微信公开id
        /// </summary>
        OpenId,
        /// <summary>
        /// 投票日期
        /// </summary>
        RecordDate,
    }
}
