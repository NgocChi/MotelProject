using Common;
using DAL;
using Dapper;
using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly AppDBContext _appDBContext;

        public KhachHangRepository(AppDBContext appDBContext)
        {
            this._appDBContext = appDBContext;
        }

        public KhachHangViewModel Gets()
        {
            var kq = new KhachHangViewModel();
            List<KhachHang> list = new List<KhachHang>();
            list = _appDBContext.KhachHangs.ToList();
            kq.list = list;
            return kq;
        }
        #region
        //private const string SP_Add = "KhachHangRepository_Add";
        //public async Task<int> Add(KhachHang kh)
        //{
        //    var param = new DynamicParameters();
        //    param.Add("@maKh", kh.MaKh, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
        //    param.Add("@tenKH", kh.TenKH, System.Data.DbType.String, System.Data.ParameterDirection.Input);
        //    param.Add("@gioiTinh", kh.GioiTinh, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
        //    param.Add("@hinhDaiDien", kh.HinhDaiDien, System.Data.DbType.String, System.Data.ParameterDirection.Input);
        //    param.Add("@ngaySinh", kh.NgaySinh, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
        //    param.Add("@mail", kh.Mail, System.Data.DbType.String, System.Data.ParameterDirection.Input);
        //    param.Add("@maNguoiThan", kh.MaNguoiThan, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
        //    param.Add("@soDienThoai", kh.SoDienThoai, System.Data.DbType.String, System.Data.ParameterDirection.Input);
        //    param.Add("@tenTaiKhoan", kh.TenTaiKhoan, System.Data.DbType.String, System.Data.ParameterDirection.Input);

        //    return (await DalHelper.SPExecuteQuery<int>(SP_Add, param, connection: DbConnection)).First();
        //}
        #endregion
    }
}
