﻿using Motel.Models;
using Motel.Models.API.Motels;
using Motel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Interfaces.Repositories
{
    public interface INhaTroRepository
    {
        IEnumerable<NhaTro> Gets();
        IEnumerable<NhaTro> GetsList(string tentaikhoan);

        Task<NhaTro> GetsById(int? id);

        Task<int> Create(NhaTro nhaTro);

        Task<int> Update(NhaTro nhaTro);

        Task<int> Delete(int id);

        Task<int> UpdateSoLuongPhong(int maNt, int soluong);

        int CheckForeignKey(int id);

        int TongPhongTrong(int nhaTro);

        int TongPhong(int nhaTro);

        Task<InfoNhaTroResponse> GetInfoNhaTroById(int maNhaTro);


        Task<InfoNhaTroResponse> GetInfoNhaTroById(int maNhaTro);
        int ThongPhongTrong(int nhaTro);

        int ThongPhong(int nhaTro);


    }
}
