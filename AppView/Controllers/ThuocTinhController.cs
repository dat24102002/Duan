using Microsoft.AspNetCore.Mvc;
using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace AppView.Controllers
{
    public class ThuocTinhController : Controller
    {
        private readonly IAllRepository<ThuocTinh> repos;
        private readonly HttpClient httpClients;
       private AssignmentDBContext dbContext =new AssignmentDBContext();
        private DbSet<ThuocTinh> _thuoctinh;
        public ThuocTinhController()
        {
            _thuoctinh = dbContext.ThuocTinhs;
            AllRepository<ThuocTinh> all = new AllRepository<ThuocTinh>(dbContext, _thuoctinh);
            repos = all;
            httpClients = new HttpClient();
        }
        public async Task<IActionResult> GetAllThuocTinh()
        {
            string apiUrl = "https://localhost:7021/api/ThuocTinh";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var LoaiSPs = JsonConvert.DeserializeObject<List<ThuocTinh>>(apiData);
            return View(LoaiSPs);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ThuocTinh thuoctinh)
        {DateTime ngaytao=thuoctinh.NgayTao;
            var url = $"https://localhost:7021/api/ThuocTinh/Create_ThuocTinh?ten={thuoctinh.Ten}&ngaytao={ngaytao.ToString("MM-dd-yyyy")}&trangthai={thuoctinh.TrangThai}";
            var response = await httpClients.PostAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllThuocTinh");
            return View();
            //repos.Add(thuoctinh);
            //return RedirectToAction("GetAllThuocTinh");
        }
        public async Task<IActionResult> Details(Guid id)
        {

            string apiUrl = "https://localhost:7021/api/ThuocTinh/" + id;
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var LoaiSPs = JsonConvert.DeserializeObject<ThuocTinh>(apiData);
            return View(LoaiSPs);

        }
        [HttpGet]
        public IActionResult Update(Guid id)
        {
            var url = $"https://localhost:7021/api/ThuocTinh/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var LoaiSPs = JsonConvert.DeserializeObject<ThuocTinh>(result);
            return View(LoaiSPs);
            //repos.GetAll().FirstOrDefault(x => x.ID == id);
            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(ThuocTinh thuoctinh,Guid id)
        {
            //repos.Update(thuoctinh);
            //return RedirectToAction("GetAllThuocTinh");
            DateTime ngaytao = thuoctinh.NgayTao;
            var url =
                $"https://localhost:7021/api/ThuocTinh/Update-ThuocTinh?id={id}&ten={thuoctinh.Ten}&ngaytao={ngaytao.ToString("MM-dd-yyyy")}&trangthai={thuoctinh.TrangThai}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllThuocTinh");


            return View();
        }
        public async Task<IActionResult> Deletes(Guid id)
        {
            //repos.Delete(p);
            //return RedirectToAction("GetAllThuocTinh");
            var url = $"https://localhost:7021/api/ThuocTinhLoaiSP/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllThuocTinh");
            return BadRequest();
        }

    }
}
