using AppData.Models;
using AppData.ViewModels;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AppView.Controllers
{
    public class HomeController : Controller
    {
        Uri urlapi = new Uri("https://localhost:7021/api");
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = urlapi;
        }

        public IActionResult Index()
        {
            return View();
        }
        public List<LoaiSP> LoadLoaiSP()
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/LoaiSP").Result;
            List<LoaiSP> ListLoaiSP = new List<LoaiSP>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ListLoaiSP = JsonConvert.DeserializeObject<List<LoaiSP>>(data);
            }

            return ListLoaiSP.ToList();
        }
        public List<SanPham> LoadSP()
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/SanPham").Result;
            List<SanPham> listSP = new List<SanPham>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listSP = JsonConvert.DeserializeObject<List<SanPham>>(data);
            }
            return listSP.ToList();
        }
        public List<BienThe> LoadBThe()
        {
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/BienThe").Result;
            List<BienThe> listBT = new List<BienThe>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listBT = JsonConvert.DeserializeObject<List<BienThe>>(data);
            }
            return listBT.ToList();
        }


        // HIỂN THỊ DANH SÁCH BIẾN THỂ ĐẦU TIÊN CỦA TỪNG SẢN PHẨM SẢN PHẨM 
        [HttpGet]
        public async Task<IActionResult> AllProduct()
        {
            ViewData["listBienThe"] = LoadBThe();
            ViewData["listLoaiSP"] = LoadLoaiSP();
            return View("Shop", LoadSP());
        }
        public async Task<IActionResult> LocLoaiSP(Guid id)
        {
            List<SanPham> listSp = LoadSP();
            var listsp = listSp.Where(c => c.IDLoaiSP.Equals(id)).ToList();
            ViewData["listBienThe"] = LoadBThe();
            ViewData["listLoaiSP"] = LoadLoaiSP();
            return View("Shop", listsp);
        }
        [HttpGet]
        public async Task<IActionResult> TimKiemSanPham(string ten)
        {
            List<SanPham> listSp = LoadSP();
            var listsp = listSp.Where(c => c.Ten.Contains(ten)).ToList();
            ViewData["listBienThe"] = LoadBThe();
            ViewData["listLoaiSP"] = LoadLoaiSP();
            return View("Shop", listsp);
        }
        [HttpGet]
        public async Task<IActionResult> LocKhoangGia(string ten)
        {
            List<SanPham> listSp = LoadSP();
            var listsp = listSp.Where(c => c.Ten.Contains(ten)).ToList();
            ViewData["listBienThe"] = LoadBThe();
            ViewData["listLoaiSP"] = LoadLoaiSP();
            return View("Shop", listsp);
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ProductDetail()
        {
            return View();
        }
        public IActionResult CheckOut(long tongtien)
        {
            ViewData["TongTien"] = tongtien;
            return View();           
        }
        public IActionResult BlogDetails()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}