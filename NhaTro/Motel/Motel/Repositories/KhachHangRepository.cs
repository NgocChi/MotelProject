using Common;
using DAL;
using Dapper;
using Motel.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class KhachHangRepository : BaseRepository
    {
        private const string SP_Add = "KhachHangRepository_Add";
        public async Task<int> Add(KhachHang kh)
        {
            var param = new DynamicParameters();
            param.Add("@Name", kh.MaKh, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@Description", kh.TenKH, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@IsActive", kh.GioiTinh, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@IsDeleted", kh.HinhDaiDien, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@CreatedDate", kh.NgaySinh, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@CreatedBy", kh.Mail, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedDate", kh.MaNguoiThan, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedBy", kh.SoDienThoai, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            param.Add("@ModifiedBy", kh.TenTaiKhoan, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);

            return (await DalHelper.SPExecuteQuery<int>(SP_Add, param, connection: DbConnection)).First();
        }
    }
}
