using AppData.Models;
using AppAPI.IServices;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using AppData.IRepositories;
using AppData.Repositories;
using AppData.ViewModels;

namespace AppAPI.Services
{
    public class HoaDonService : IHoaDonService
    {
        private readonly IAllRepository<HoaDon> reposHoaDon;
        private readonly IAllRepository<ChiTietHoaDon> reposChiTietHoaDon;
        private readonly IAllRepository<BienThe> reposBienThe;
        AssignmentDBContext context = new AssignmentDBContext();

        public HoaDonService()
        {
            reposHoaDon = new AllRepository<HoaDon>(context, context.HoaDons);
            reposChiTietHoaDon = new AllRepository<ChiTietHoaDon>(context, context.ChiTietHoaDons);
            reposBienThe = new AllRepository<BienThe>(context, context.BienThes);
        }

        public List<ChiTietHoaDon> GetAllChiTietHoaDon(Guid idHoaDon)
        {
            return reposChiTietHoaDon.GetAll().Where(x => x.IDHoaDon == idHoaDon).ToList();
        }

        public List<HoaDon> GetAllHoaDon()
        {
            return reposHoaDon.GetAll();
        }
        public bool CreateHoaDon(List<ChiTietHoaDonViewModel> chiTietHoaDons, string ten, string SDT, string email, string phuongThucThanhToan, string diaChi, int tienShip)
        {
            try
            {
                HoaDon hoaDon = new HoaDon();
                hoaDon.ID = Guid.NewGuid();
                hoaDon.TenNguoiNhan = ten;
                hoaDon.SDT = SDT;
                hoaDon.Email = email;
                hoaDon.NgayTao = DateTime.Now;
                hoaDon.DiaChi = diaChi;
                hoaDon.TienShip = tienShip;
                hoaDon.PhuongThucThanhToan = phuongThucThanhToan;
                hoaDon.TrangThaiGiaoHang = 1;
                if (reposHoaDon.Add(hoaDon))
                {
                    foreach (var x in chiTietHoaDons)
                    {
                        ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                        chiTietHoaDon.ID = Guid.NewGuid();
                        chiTietHoaDon.IDHoaDon = hoaDon.ID;
                        chiTietHoaDon.IDBienThe = x.IDBienThe;
                        chiTietHoaDon.SoLuong = x.SoLuong;
                        chiTietHoaDon.DonGia = reposBienThe.GetAll().First(y => y.ID == x.IDBienThe).GiaBan;
                        chiTietHoaDon.TrangThai = 1;
                        reposChiTietHoaDon.Add(chiTietHoaDon);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void UpdateTrangThaiGiaoHang(int trangThai)
        {
            throw new NotImplementedException();
        }
    }
}
