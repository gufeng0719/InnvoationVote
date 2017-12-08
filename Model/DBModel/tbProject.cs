using System;

namespace Model.DBModel
{
    /// <summary>
    /// 
    /// </summary>
    public class tbProject : BaseModel
    {
        public static string PrimaryKey { get; set; } = "ProjectId";
        public static string IdentityKey { get; set; } = "ProjectId";

        /// <summary>
        /// 序号
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目类别编号 1 党建和精神文明类 2 公共管理类 3经济建设类
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 项目类别名称
        /// </summary>
        public string TypeName { get; set; } = string.Empty;

        /// <summary>
        /// 单位名称
        /// </summary>
        public string OrganName { get; set; } = string.Empty;

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 项目简介
        /// </summary>
        public string ProjectIntro { get; set; } = string.Empty;

        /// <summary>
        /// 项目图片
        /// </summary>
        public string ProjectImages { get; set; } = string.Empty;

        /// <summary>
        /// 得票数
        /// </summary>
        public int VoteNumber { get; set; }

        /// <summary>
        /// 项目状态 1可用 0 不可用
        /// </summary>
        public int ProjectState { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string ProjectRemark { get; set; } = string.Empty;

    }


    public enum tbProjectEnum
    {
        /// <summary>
        /// 序号
        /// </summary>
        ProjectId,
        /// <summary>
        /// 项目类别编号 1 党建和精神文明类 2 公共管理类 3经济建设类
        /// </summary>
        TypeId,
        /// <summary>
        /// 项目类别名称
        /// </summary>
        TypeName,
        /// <summary>
        /// 单位名称
        /// </summary>
        OrganName,
        /// <summary>
        /// 项目名称
        /// </summary>
        ProjectName,
        /// <summary>
        /// 项目简介
        /// </summary>
        ProjectIntro,
        /// <summary>
        /// 项目图片
        /// </summary>
        ProjectImages,
        /// <summary>
        /// 得票数
        /// </summary>
        VoteNumber,
        /// <summary>
        /// 项目状态 1可用 0 不可用
        /// </summary>
        ProjectState,
        /// <summary>
        /// 备注
        /// </summary>
        ProjectRemark,
    }
}
