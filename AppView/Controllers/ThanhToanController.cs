using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AppData.ViewModels;
using AppView.Services;
using System.Net.Http;

namespace AppView.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly HttpClient _httpClient;
        List<ChiTietGioHang> listCTGH = new List<ChiTietGioHang>();

        public ThanhToanController()
        {
            _httpClient = new HttpClient();
            HttpResponseMessage response1 = _httpClient.GetAsync("https://localhost:7021/api/ChiTietGioHang").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                listCTGH = JsonConvert.DeserializeObject<List<ChiTietGioHang>>(data);
            }
        }
        public async Task<IActionResult> GetAllHoaDonAsync()
        {
            string apiUrl = "https://localhost:7021/api/HoaDon";
            var response = await _httpClient.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var sanPhams = JsonConvert.DeserializeObject<List<HoaDon>>(apiData);
            return View(sanPhams);
        }
        [HttpPost]
        public async Task<IActionResult> TaoHoaDonAsync(HoaDonViewModel hoaDonViewModel)
        {
            hoaDonViewModel.PhuongThucThanhToan = "ThanhToanKhiNhanHang";
            hoaDonViewModel.ChiTietHoaDons = new List<ChiTietHoaDonViewModel>();
            if (HttpContext.Session.GetString("IdUser") == null)
            {
                foreach(var x in GioHangService.GetObjFormSession(HttpContext.Session, "Cart"))
                {
                    ChiTietHoaDonViewModel cthd = new ChiTietHoaDonViewModel();
                    cthd.IDBienThe = x.IDBienThe;
                    cthd.SoLuong = x.SoLuong;
                    hoaDonViewModel.ChiTietHoaDons.Add(cthd);
                }
				var CartUser = GioHangService.GetObjFormSession(HttpContext.Session, "Cart");
				// Xóa tất cả sản phẩm trong giỏ hàng
				CartUser.Clear();
				GioHangService.SetObjToJson(HttpContext.Session, "Cart", CartUser);// Cập nhật lại giỏ hàng

			}
			else
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                foreach (var x in listCTGH.Where(c => c.IDNguoiDung == iduser).ToList())
                {
                    ChiTietHoaDonViewModel cthd = new ChiTietHoaDonViewModel();
                    cthd.IDBienThe = x.IDBienThe;
                    cthd.SoLuong = x.SoLuong;
                    hoaDonViewModel.ChiTietHoaDons.Add(cthd);
                }
                //xóa giỏ hàng chi tiết
				string apiUrlGioHang = "https://localhost:7021/api/ChiTietGioHang";
				var responseGioHang = await _httpClient.GetAsync(apiUrlGioHang);
				string apiDataGioHang = await responseGioHang.Content.ReadAsStringAsync();
				var ChitietGH = JsonConvert.DeserializeObject<List<ChiTietGioHang>>(apiDataGioHang);

                var getallChiTietGioHangbyName = ChitietGH.Where(p => p.IDNguoiDung == iduser).ToList();
                foreach (var item in getallChiTietGioHangbyName)
                {
					var url = $"https://localhost:7021/api/ChiTietGioHang/{item.ID}";
					var res = await _httpClient.DeleteAsync(url);
				}

				
			}
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7021/api/HoaDon", hoaDonViewModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else return RedirectToAction("AboutUs", "Home");
        }
    }
}
