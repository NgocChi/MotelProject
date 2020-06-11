using Common;
using DAL;
using Dapper;
using Motel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Queries
{
    public class TaiKhoanQueries : BaseRepository
    {
        private const string SP_CheckDangNhap = "TaiKhoanQueries_GetDangNhap";
        public async Task<TaiKhoan> Get(string taiKhoan, string matKhau)
        {
            var param = new DynamicParameters();
            param.Add("@tenTaiKhoan", taiKhoan, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            param.Add("@matKhau", matKhau, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            return (await DalHelper.SPExecuteQuery<TaiKhoan>(SP_CheckDangNhap, param, connection: DbConnection)).FirstOrDefault();
        }
    }
}
