using AppData.Models;
using AppData.ViewModels;

namespace AppAPI.IServices
{
    public interface IHoaDonService
    {
        public bool CreateHoaDon(List<ChiTietHoaDonViewModel> chiTietHoaDons, string ten, string SDT, string email, string phuongThucThanhToan, string diaChi, int tienShip);
        public List<HoaDon> GetAllHoaDon();
        public List<ChiTietHoaDon> GetAllChiTietHoaDon(Guid idHoaDon);
        public void UpdateTrangThaiGiaoHang(int trangThai);
    }
}
