using Microsoft.AspNetCore.Mvc;
using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly IAllRepository<SanPham> repos;
        private readonly HttpClient httpClients;
        public SanPhamController()
        {
            httpClients = new HttpClient();
        }
        public async Task<IActionResult> GetAllSanPham()
        {
            string apiUrl = "https://localhost:7021/api/SanPham";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SanPhams = JsonConvert.DeserializeObject<List<SanPham>>(apiData);
            return View(SanPhams);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SanPham sanpham)
        {
            var url = $"https://localhost:7021/api/SanPham/create-san-pham?ten={sanpham.Ten}&idLoaiSP={sanpham.IDLoaiSP}&moTa={sanpham.MoTa}&trangthai={sanpham.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllSanPham");
            return View();
        }
        public async Task<IActionResult> Details(Guid id)
        {

            string apiUrl = "https://localhost:7021/api/SanPham/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SanPhams = JsonConvert.DeserializeObject<SanPham>(apiData);
            return View(SanPhams);

        }
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7021/api/SanPham/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var SanPhams = JsonConvert.DeserializeObject<SanPham>(result);
            return View(SanPhams);
        }
        [HttpPost]
        public async Task<IActionResult> Update(SanPham sanpham,Guid id)
        {

            var url =
                $"https://localhost:7021/api/SanPham/{id}?ten={sanpham.Ten}&idLoaiSP={sanpham.IDLoaiSP}&moTa={sanpham.MoTa}&trangthai={sanpham.TrangThai}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllSanPham");


            return View();
        }
        public async Task<IActionResult> Deletes(Guid id)
        {
            var url = $"https://localhost:7021/api/SanPham/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllSanPham");
            return BadRequest();
        }
    }
}
