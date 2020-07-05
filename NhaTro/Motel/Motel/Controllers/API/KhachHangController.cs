using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.Queries;
using Motel.Repositories;
using Web.Controls;

namespace Motel.Controllers.API
{
    [Route("api/[controller]")]
    public class KhachHangController : Controller
    {
        private readonly IKhachHangRepository Repository = null;


        public KhachHangController(IKhachHangRepository queries)
        {
            this.Repository = queries;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ApiResult> GetById(int id)
        {
            var rs = await Repository.GetsById(id);
            return new ApiResult()
            {
                Result = rs == null ? -1 : 0,
                Data = rs
            };
        }

        [HttpGet]
        [Route("Gets")]
        public async Task<ApiResult> Gets()
        {
            var rs = Repository.Gets();
            return new ApiResult()
            {
                Result = rs == null ? -1 : 0,
                Data = rs
            };
        }







    }
}