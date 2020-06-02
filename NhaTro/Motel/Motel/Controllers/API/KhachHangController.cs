using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motel.Models;
using Motel.Queries;
using Motel.Repositories;
using Web.Controls;

namespace Motel.Controllers.API
{
    [Route("api/[controller]")]
    public class KhachHangController : Controller
    {
        private readonly IConfiguration Configuration = null;
        public KhachHangQueries Queries = null;
        public KhachHangRepository Repository = null;
        public KhachHangController(IConfiguration configuration)
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
        public async Task<ApiResult> Get(int attributeId)
        {
            var rs = await Queries.Get(attributeId);
            return new ApiResult()
            {
                Result = 0,
                Data = rs
            };
        }

        [HttpPost]
        [Route("add")]
        public async Task<ApiResult> Add(KhachHang khachHang)
        {
            var rs = await Repository.Add(khachHang);
            return new ApiResult()
            {
                Result = rs > 0 ? 0 : -1,
                Data = rs
            };
        }
    }
}