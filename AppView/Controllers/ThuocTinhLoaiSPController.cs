using Microsoft.AspNetCore.Mvc;
using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class ThuocTinhLoaiSPController : Controller
    {
        private readonly IAllRepository<ThuocTinhLoaiSP> repos;
        private readonly HttpClient httpClients;
        public IActionResult Index()
        {
            return View();
        }
        public ThuocTinhLoaiSPController()
        {
            httpClients = new HttpClient();
        }
        public async Task<IActionResult> GetAllThuocTinhLoaiSP()
        {
            string apiUrl = "https://localhost:7021/api/ThuocTinhLoaiSP";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SanPhams = JsonConvert.DeserializeObject<List<ThuocTinhLoaiSP>>(apiData);
            return View(SanPhams);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ThuocTinhLoaiSP ttlSP)
        {
            var url = $"https://localhost:7021/api/ThuocTinhLoaiSP/create-thuoc-tinh-loaisp?idThuocTinh={ttlSP.IDThuocTinh}&idLoaiSP={ttlSP.IDLoaiSP}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllThuocTinhLoaiSP");
            return View();
        }
        public async Task<IActionResult> Details(Guid id)
        {

            string apiUrl = "https://localhost:7021/api/ThuocTinhLoaiSP/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var SanPhams = JsonConvert.DeserializeObject<ThuocTinhLoaiSP>(apiData);
            return View(SanPhams);

        }
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7021/api/ThuocTinhLoaiSP/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var SanPhams = JsonConvert.DeserializeObject<ThuocTinhLoaiSP>(result);
            return View(SanPhams);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ThuocTinhLoaiSP ttlSP,Guid id)
        {

            var url =
                $"https://localhost:7021/api/ThuocTinhLoaiSP/{id}?idThuocTinh={ttlSP.IDThuocTinh}&idLoaiSP={ttlSP.IDLoaiSP}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllThuocTinhLoaiSP");


            return View();
        }
        public async Task<IActionResult> Deletes(Guid id)
        {
            var url = $"https://localhost:7021/api/ThuocTinhLoaiSP/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllThuocTinhLoaiSP");
            return BadRequest();
        }

    }
}
