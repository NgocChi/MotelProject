using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models.API.Rooms;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motel.Controllers.API
{
    [Route("api/[controller]")]
    public class PhongController : Controller
    {
        private readonly IPhongRepository _phongRepository;

        public PhongController(IPhongRepository phongRepository)
        {
            _phongRepository = phongRepository;
        }


        [HttpGet]
        [Route("GetPhongDangHoatDongByKhachThue")]
        public async Task<ListPhongResponse> GetPhongDangHoatDongByKhachThue([FromBody] ListPhongRequest request)
        {
            var result = new ListPhongResponse();

            if (request.AccessToken != "OK")
            {
                result.StatusCode = 401; //HTTP 401 (Unauthorized)
                return result;
            }
            /*
             1: chưa thuê,
             2: đặt cọc
             3: Đang thuê
             */
            var listData = _phongRepository.GetPhongDangHoatDongByKhachThue(request.MaKhachThue);
            result.PhongDtos = new List<PhongDto>();
            foreach (var item in listData)
            {
                result.PhongDtos.Add(
                    new PhongDto
                    {
                        MaPhong = item.MaPhong,
                        TenPhong = item.TenPhong,
                        MaNhaTro = item.MaNhaTro,
                        TenNhaTro = item.TenNhaTro,
                    });
            }
            result.Message = "OK";
            result.StatusCode = 0; //Success;
            return result;
        }
    }
}
