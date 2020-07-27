﻿using Motel.Models;
using Motel.Models.API.ElectrictyAndWaters;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface IDienNuocRepository
    {
        DienNuocViewModel GetDienNuocByIdPhong(int idPhong, DateTime ngayghi);

        IEnumerable<DienNuocViewModel> Gets();

        Task<int> Create(DienNuoc dn);

        Task<int> Update(DienNuoc dn);

        Task<int> UpdateChotSo(int idDN);

        Task<DienNuoc> GetById(int id);
        Task<ElectrictyAndWaterResponse> GetListElectrictyAndWaterByTime(ElectrictyAndWaterRequest request);
        Task<ElectrictyAndWaterRoomsNotInputRespone> GetListRoomNotInputElectrictyAndWater(ElectrictyAndWaterRoomsNotInputRequest request);
        Task<InfoElectrictyAndWaterResponse> UpdateInfoElectrictyAndWater(InfoElectrictyAndWaterRequest request);
        Task<ElectrictyAndWaterOldRespone> GetElectrictyAndWaterOld(ElectrictyAndWaterOldRequest request);
    }
}
