using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models.API.DangNhap;
using Web.Controls;

namespace Motel.Controllers.API
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ITaiKhoanRepository TaiKhoanRepository;

        public LoginController(ITaiKhoanRepository taiKhoanRepository)
        {
            this.TaiKhoanRepository = taiKhoanRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ApiResult> Login(DangNhapRequest request)
        {

            var rs = "hello";
            return new ApiResult()
            {
                Result = rs == null ? -1 : 0,
                Data = rs
            };
        }
    }
}