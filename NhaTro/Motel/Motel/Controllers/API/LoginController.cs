using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.Models.API.Logins;

namespace Motel.Controllers.API
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;
        private readonly IChuTroRepository _chuTroRepository;
        private readonly IKhachHangRepository _khachHangRepository;

        public LoginController(ITaiKhoanRepository taiKhoanRepository, IChuTroRepository chuTroRepository, IKhachHangRepository khachHangRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
            _chuTroRepository = chuTroRepository;
            _khachHangRepository = khachHangRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest request)
        {
            /*
             0: Thành cồng,
             1: Không tìm thấy account,
             2: Sai mật khẩu,
             */
            var result = new LoginResponse();

            if (result == null)
            {
                result.StatusCode = 1;
                result.Message = "Không tìm thấy tài khoản";
                return result;
            }
            
            var user = _taiKhoanRepository.Gets().FirstOrDefault(e => e.TenTaiKhoan == request.TenTaiKhoan);
         
            if(user == null)
            {
                result.StatusCode = 1;
                result.Message = "Không tìm thấy tài khoản";
                return result;
            }
            else if (user.MatKhau != request.MatKhau)
            {
                result.StatusCode = 2;
                result.Message = "Mật khẩu không đúng";
                return result;
            }
            else
            {
                result.StatusCode = 0;
                result.AccessToken = "OK";
                result.LoaiTaiKhoan = user.LoaiTaiKhoan;
                result.Message = "OK";

                result.IdUser = _taiKhoanRepository.GetIdUserByTenTaiKhoan(request.TenTaiKhoan, Convert.ToBoolean(result.LoaiTaiKhoan));
            }
            return result;
        }
    }
}