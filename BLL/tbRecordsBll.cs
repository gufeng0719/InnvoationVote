using DAL;
using IDAL;
using Model.DBModel;
namespace BLL
{
    /// <summary>
    ///   逻辑层
    /// </summary>
    public class tbRecordsBll : BaseBll<tbRecords, tbRecordsEnum, int>
    {
        public tbRecordsBll() : base(new tbRecordsDal()) { }

        public tbRecordsBll(IBaseDal<tbRecords, tbRecordsEnum, int> dal) : base(dal) { }
    }
}
