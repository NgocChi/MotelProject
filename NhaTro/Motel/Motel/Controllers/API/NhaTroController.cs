using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models.API.Motels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motel.Controllers.API
{
    [Route("api/[controller]")]
    public class NhaTroController : Controller
    {
        private readonly INhaTroRepository _nhaTroRepository;
        public NhaTroController(INhaTroRepository nhaTroRepository)
        {
            _nhaTroRepository = nhaTroRepository;
        }

        [HttpGet]
        [Route("GetNhaTroByChuTro")]
        public Task<ListNhaTroResponse> GetNhaTroByChuTro([FromBody] ListNhaTroRequest request)
        {
            var task = new TaskCompletionSource<ListNhaTroResponse>();
            var response = new ListNhaTroResponse();
            if (request == null)
            {
                response.StatusCode = 1;
                response.Message = "Failed";
                task.SetResult(response);
            }

            if (request.AccessToken != "OK") // todo update after
            {
                response.StatusCode = 1;
                response.Message = "Failed";
                task.SetResult(response);
            }

            var dataFormDB = _nhaTroRepository.Gets();

            response.NhaTroDtos = new List<NhaTroDto>();
            foreach (var item in dataFormDB)
            {
                response.NhaTroDtos.Add(new NhaTroDto
                {
                    DiaChi = item.DiaChi,
                    MaChuTro = item._MaChuTro,
                    TenNhaTro = item.Ten,
                    MaNhaTro = item.MaNT,
                });
            }
            response.Message = "OK";
            response.StatusCode = 0;
            task.SetResult(response);
            return task.Task;
        }

        [HttpGet]
        [Route("GetInfoNhaTroById")]
        public async Task<InfoNhaTroResponse> GetInfoNhaTroById([FromBody] InfoNhaTroRequest request)
        {
            var response = new InfoNhaTroResponse();
            if (request == null)
            {
                response.StatusCode = 1;
                response.Message = "Failed";
                return response;
            }

            if (request.AccessToken != "OK") // todo update after
            {
                response.StatusCode = 1;
                response.Message = "Failed";
                return response;

            }

            var dataFormDb = await _nhaTroRepository.GetInfoNhaTroById(request.MaNhaTro);
            if (dataFormDb != null)
            {
                return dataFormDb;
            }
            else
            {
                response.StatusCode = 1;
                response.Message = "Failed";
                return response;
            }
        }
    }
}
