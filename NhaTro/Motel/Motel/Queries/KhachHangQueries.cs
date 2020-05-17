using Common;
using DAL;
using Dapper;
using Motel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Queries
{
    public class KhachHangQueries: BaseRepository
    {
        private const string SP_GETS = "KhachHangQueries_Gets";
        private const string SP_GET = "KhachHangQueries_Get";
        public async Task<IEnumerable<KhachHang>> Gets()
        {
            var param = new DynamicParameters();
            return await DalHelper.SPExecuteQuery<KhachHang>(SP_GETS, param, connection: DbConnection);
        }

        public async Task<KhachHang> Get(int id)
        {
            var param = new DynamicParameters();
            param.Add("@maKH", id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            return (await DalHelper.SPExecuteQuery<KhachHang>(SP_GET, param, connection: DbConnection)).FirstOrDefault();
        }
    }
}
