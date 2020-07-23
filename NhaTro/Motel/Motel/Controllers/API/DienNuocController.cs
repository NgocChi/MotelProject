﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motel.Interfaces.Repositories;
using Motel.Models.API.ElectrictyAndWaters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Motel.Controllers.API
{
    [Route("api/[controller]")]
    public class DienNuocController : Controller
    {
        private readonly IDienNuocRepository _dienNuocRepository;

        public DienNuocController(IDienNuocRepository dienNuocRepository)
        {
            _dienNuocRepository = dienNuocRepository;
        }

        [HttpGet]
        [Route("GetListElectrictyAndWaterByTime")]
        public async Task<ElectrictyAndWaterResponse> GetListElectrictyAndWaterByTime([FromBody] ElectrictyAndWaterRequest request)
        {
            var response = new ElectrictyAndWaterResponse();
            if (request == null || request.AccessToken != "OK")
            {
                response.Message = "Failed";
                response.StatusCode = 1;
                return response;
            }

            var dataFormDB =  await _dienNuocRepository.GetListElectrictyAndWaterByTime(request);

            if (dataFormDB == null)
            {
                response.Message = "Failed";
                response.StatusCode = 1;
                return response;
            }
            else return dataFormDB;
        }
    }
}
