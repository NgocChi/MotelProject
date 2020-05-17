using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Controls;
using Microsoft.Extensions.Configuration;
using Motel.Queries;
using Motel.Repositories;

namespace Motel.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IConfiguration Configuration = null;
        public KhachHangQueries Queries = null;
        public KhachHangRepository Repository = null;
        public HomeController(IConfiguration configuration)
        {
            this.Configuration = configuration;
            Queries = new KhachHangQueries();
            Repository = new KhachHangRepository();
        }

        [HttpGet]
        [Route("getCus")]
        public async Task<ApiResult> GetCus()
        {
            var kq = await Queries.Gets();
            return new ApiResult()
            {
                Result = 0,
                Data = kq
            };
        }


        [HttpGet]
        [Route("get")]
        public async Task<ApiResult> Get(int attributeId = 1)
        {
            var rs = await Queries.Get(attributeId);
            return new ApiResult()
            {
                Result = 0,
                Data = rs
            };
        }

        //[HttpPost]
        //[Route("add")]
        //public async Task<ApiResult> Add()
        //{
        //    var rs = await Repository.Add();
        //    return new ApiResult()
        //    {
        //        Result = rs > 0 ? 0 : -1,
        //        Data = rs
        //    };
        //}

    }
}