using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models.API.Contacts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motel.Controllers.API
{
    [Route("api/[controller]")]
    public class HopDongController : Controller
    {
        private readonly IHopDongRepository _hopDongRepository;

        public HopDongController(IHopDongRepository hopDongRepository)
        {
            this._hopDongRepository = hopDongRepository;
        }

        [HttpGet]
        [Route("GetThongTinHopDongByIdMaPhong")]
        public async Task<HopDongInfoResponse> GetThongTinHopDongByIdMaPhong([FromBody] HopDongInfoRequest request)
        {
            var result = new HopDongInfoResponse();
            if (request == null)
            {
                result.Message = "Failed";
                result.StatusCode = 1;
                return result;
            }
            result = _hopDongRepository.GetThongTinHopDongByIdMaPhong(request.MaHopDong);
            if(result == null)
            {
                result.Message = "Failed";
                result.StatusCode = 1;
                return result;
            }
            return result;
        }
    }
}
