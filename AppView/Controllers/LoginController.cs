using AppData.Models;
using AppView.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace App_Ban_Quan_Ao_Thoi_Trang_Nam.Controllers
{
    public class LoginController : Controller
    {
        HttpClient client = new HttpClient();
        public LoginController()
        {
            client = new HttpClient();
        }
        [HttpGet]
        public IActionResult Login()
        {
            var response = new UserViewModel();
            return View(response);
        }
        [HttpPost]
        public IActionResult Login(UserViewModel user)
        {
            if (!ModelState.IsValid) return View(user);
            HttpResponseMessage response = client.GetAsync("https://localhost:7021/api/NguoiDung").Result;
            List<NguoiDung> listNgDung = new List<NguoiDung>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listNgDung = JsonConvert.DeserializeObject<List<NguoiDung>>(data);
            }
            HttpResponseMessage response1 = client.GetAsync("https://localhost:7021/api/VaiTro").Result;
            List<VaiTro> listVaiTro = new List<VaiTro>();
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                listVaiTro = JsonConvert.DeserializeObject<List<VaiTro>>(data);
            }
            var ngdung = listNgDung.Where(c => c.Email == user.Email).FirstOrDefault();
            if (ngdung != null)
            {
                if (user.Password.Equals(ngdung.Password))
                {
                    HttpContext.Session.SetString("IdUser", ngdung.IDNguoiDung.ToString());
                    HttpContext.Session.SetString("UserName", ngdung.Ten);
                    var quyen = listVaiTro.Where(c => c.Ten == "Admin").First();
                    if (ngdung.IDVaiTro == quyen.ID)
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (!ModelState.IsValid) return View(user);
                }
            }
            return View(user);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Register(UserViewModel user)
        {
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
