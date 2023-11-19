using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AppView.Controllers
{
    public class BienTheController : Controller
    {
        private readonly HttpClient httpClients;
        public BienTheController()
        {
            httpClients = new HttpClient();
        }
        public async Task<IActionResult> GetAllBienThe()
        {
            string apiUrl = "https://localhost:7021/api/BienThe";
            var response = await httpClients.GetAsync(apiUrl);
            string apiData = await response.Content.ReadAsStringAsync();
            var bienthe = JsonConvert.DeserializeObject<List<BienThe>>(apiData);

            //SanPham Getall
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;

            return View(bienthe);
        }
        public async Task<IActionResult> Create()
        {
            //SanPham Getall
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BienThe bienthe, IFormFile Anh)
        {
            if (Anh != null && Anh.Length > 0) // Kiểm tra đường dẫn phù hợp
            {
                // thực hiện việc sao chép ảnh đó vào wwwroot
                // Tạo đường dẫn tới thư mục sao chép (nằm trong root)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    "img", Anh.FileName); // abc/wwwroot/images/xxx.png
                var stream = new FileStream(path, FileMode.Create); // Tạo 1 filestream để tạo mới
                Anh.CopyTo(stream); // Copy ảnh vừa dc chọn vào đúng cái stream đó
                // Gán lại giá trị link ảnh (lúc này đã nằm trong root cho thuộc tính description)
                bienthe.Anh = Anh.FileName;
            }
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;
            DateTime ngaytao = bienthe.NgayTao;
             
            var url = $"https://localhost:7021/api/BienThe/create-bien-the?IdSanPham={bienthe.IDSanPham}&SoLuong={bienthe.SoLuong}&GiaBan={bienthe.GiaBan}&NgayTao={ngaytao.ToString("MM-dd-yyyy")}&Anh={bienthe.Anh}";
            var response = await httpClients.PostAsync(url, null);
            string apiData = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllBienThe");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;

            var url = $"https://localhost:7021/api/BienThe/{id}";
            var response = httpClients.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var bienthe = JsonConvert.DeserializeObject<BienThe>(result);
            return View(bienthe);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BienThe bienthe, IFormFile Anh)
        {
            if (Anh != null && Anh.Length > 0) // Kiểm tra đường dẫn phù hợp
            {
                // thực hiện việc sao chép ảnh đó vào wwwroot
                // Tạo đường dẫn tới thư mục sao chép (nằm trong root)
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    "img", Anh.FileName); // abc/wwwroot/images/xxx.png
                var stream = new FileStream(path, FileMode.Create); // Tạo 1 filestream để tạo mới
                
                Anh.CopyTo(stream); // Copy ảnh vừa dc chọn vào đúng cái stream đó
                // Gán lại giá trị link ảnh (lúc này đã nằm trong root cho thuộc tính description)
                bienthe.Anh = Anh.FileName;
            }
            string apiUrlsp = "https://localhost:7021/api/SanPham";
            var responsesp = await httpClients.GetAsync(apiUrlsp);
            string apiDatasp = await responsesp.Content.ReadAsStringAsync();
            var SanPham = JsonConvert.DeserializeObject<List<SanPham>>(apiDatasp);
            ViewBag.SanPham = SanPham;

            var url =
                $"https://localhost:7021/api/BienThe/{bienthe.ID}?IdSanPham={bienthe.IDSanPham}&SoLuong={bienthe.SoLuong}&GiaBan={bienthe.GiaBan}&NgayTao={bienthe.NgayTao.ToString("MM-dd-yyyy")}&Anh={bienthe.Anh}";
            var response = await httpClients.PutAsync(url, null);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllBienThe");


            return View();
        }
        public async Task<IActionResult> Deletes(Guid id)
        {
            var url = $"https://localhost:7021/api/BienThe/{id}";
            var response = await httpClients.DeleteAsync(url);
            if (response.IsSuccessStatusCode) return RedirectToAction("GetAllBienThe");
            return BadRequest();
        }
    }
}
