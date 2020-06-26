using Common;
using DAL;
using Dapper;
using Motel.Data;
using Motel.Interfaces.Repositories;
using Motel.Models;
using Motel.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motel.Repositories
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly AppDBContext _appDBContext;

        public KhachHangRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        public IEnumerable<KhachHang> Gets()
        {

            return _appDBContext.KhachHangs.ToList();
        }

        public async Task<KhachHang> GetsById(int? id)
        {
            return await _appDBContext.KhachHangs.FindAsync(id);
        }

        public async Task<int> Create(KhachHang khach)
        {
            if (khach != null)
            {
                _appDBContext.KhachHangs.Add(khach);
                 await _appDBContext.SaveChangesAsync();
                return khach.MaKh;
            }
            return 0;
        }
        public async Task<int> Update(KhachHang khach)
        {
            KhachHang find = await _appDBContext.KhachHangs.FindAsync(khach.MaKh);
            if (find != null)
            {
                find.TenKH = khach.TenKH;
                find.TenTaiKhoan = khach.TenTaiKhoan;
                find.CMND = khach.CMND;
                find.NgaySinh = khach.NgaySinh;
                find.SoDienThoai = khach.SoDienThoai;
                find.Mail = khach.Mail;
                find.MaNguoiThan = khach.MaNguoiThan;
                _appDBContext.KhachHangs.Update(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }

        public async Task<int> Delete(int id)
        {
            KhachHang find = await _appDBContext.KhachHangs.FindAsync(id);
            if (find != null)
            {
                _appDBContext.KhachHangs.Remove(find);
                await _appDBContext.SaveChangesAsync();
                return 1;
            }
            return 0;
        }


    }
}
