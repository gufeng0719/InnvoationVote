using DAL;
using IDAL;
using Model.DBModel;
namespace BLL
{
    /// <summary>
    ///   逻辑层
    /// </summary>
    public class tbProjectBll : BaseBll<tbProject, tbProjectEnum, int>
    {
        public tbProjectBll() : base(new tbProjectDal()) { }

        public tbProjectBll(IBaseDal<tbProject, tbProjectEnum, int> dal) : base(dal) { }
    }
}
