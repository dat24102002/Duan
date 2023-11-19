using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class GiaTriController : Controller
    {
        private readonly HttpClient httpClients;
        public GiaTriController()
        {
            httpClients = new HttpClient();
        }
        public async Task<IActionResult> GetAllGiaTri()
        {
            string apiUrl = "https://localhost:7021/api/GiaTri";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var giatri = JsonConvert.DeserializeObject<List<GiaTri>>(apiData);

            //thuoctinh getall
            string apiUrlThuoctinh = "https://localhost:7021/api/ThuocTinh";
            var responseTT = await httpClients.GetAsync(apiUrlThuoctinh);
            string apiDataTT = await responseTT.Content.ReadAsStringAsync();
            var thuocTinh = JsonConvert.DeserializeObject<List<ThuocTinh>>(apiDataTT);
            ViewBag.ThuocTinh = thuocTinh;

            return View(giatri);
        }
        public async Task<IActionResult> Create()
        {
            string apiUrlThuoctinh = "https://localhost:7021/api/ThuocTinh";
            var responseTT = await httpClients.GetAsync(apiUrlThuoctinh);
            string apiDataTT = await responseTT.Content.ReadAsStringAsync();
            var thuocTinh = JsonConvert.DeserializeObject<List<ThuocTinh>>(apiDataTT);
            ViewBag.ThuocTinh = thuocTinh;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(GiaTri giaTri)
        {
            var url = $"https://localhost:7021/api/GiaTri/Create_GiaTri?ten={giaTri.Ten}&idThuocTinh={giaTri.IdThuocTinh}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllGiaTri");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiUrlThuoctinh = "https://localhost:7021/api/ThuocTinh";
            var responseTT = await httpClients.GetAsync(apiUrlThuoctinh);
            string apiDataTT = await responseTT.Content.ReadAsStringAsync();
            var thuocTinh = JsonConvert.DeserializeObject<List<ThuocTinh>>(apiDataTT);
            ViewBag.ThuocTinh = thuocTinh;

            var url = $"https://localhost:7021/api/GiaTri/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var giatri = JsonConvert.DeserializeObject<GiaTri>(result);
            return View(giatri);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(GiaTri giaTri)
        {

            var url =
                $"https://localhost:7021/api/GiaTri/Update-GiaTri?id={giaTri.ID}&ten={giaTri.Ten}&idThuocTinh={giaTri.IdThuocTinh}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllGiaTri");


            return View();
        }
        public async Task<IActionResult> Deletes(Guid id)
        {
            var url = $"https://localhost:7021/api/GiaTri/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllGiaTri");
            return BadRequest();
        }
    }
}
