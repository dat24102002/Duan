using Microsoft.AspNetCore.Mvc;
using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class LoaiSPController : Controller
    {
        private readonly IAllRepository<LoaiSP> repos;
        private readonly HttpClient httpClients;
        public LoaiSPController()
        {
            httpClients = new HttpClient();
        }
        public async Task<IActionResult> GetAllLoaiSP()
        {
            string apiUrl = "https://localhost:7021/api/LoaiSP";
            var response=await httpClients.GetAsync(apiUrl);
            string apiData=await response.Content.ReadAsStringAsync();
            var LoaiSPs=JsonConvert.DeserializeObject<List<LoaiSP>>(apiData);
            return View(LoaiSPs);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LoaiSP loaiSP )
        {
      
            var url = $"https://localhost:7021/api/LoaiSP/create-loaisp?ten={loaiSP.Ten}&idLoaiSPCha={loaiSP.IDLoaiSPCha}&trangthai={loaiSP.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllLoaiSP");
            return View();
        }
        public async Task<IActionResult> Details(Guid id)
        {
           
            string apiUrl = "https://localhost:7021/api/LoaiSP/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var LoaiSPs = JsonConvert.DeserializeObject<LoaiSP>(apiData);
            return View(LoaiSPs);

        }
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7021/api/LoaiSP/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var LoaiSPs = JsonConvert.DeserializeObject<LoaiSP>(result);
            return View(LoaiSPs);
        }
        [HttpPost]
        public async Task<IActionResult> Update(LoaiSP loaiSP,Guid id)
        {

            var url =
                $"https://localhost:7021/api/LoaiSP/{id}?ten={loaiSP.Ten}&idLoaiSPCha={loaiSP.IDLoaiSPCha}&trangthai={loaiSP.TrangThai}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllLoaiSP");


            return View();
        }
        
        public async Task<IActionResult> Deletes(Guid id)
        {
            var url = $"https://localhost:7021/api/LoaiSP/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllLoaiSP");
            return BadRequest();
        }




    }
}
