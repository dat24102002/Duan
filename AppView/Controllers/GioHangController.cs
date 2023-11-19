using AppView.Services;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppView.Controllers
{
    public class GioHangController : Controller
    {
        Uri urlapi = new Uri("https://localhost:7021/api");
        private readonly HttpClient _httpClient;
        private readonly ILogger<GioHangController> _logger;
        List<ChiTietGioHang> listCTGH = new List<ChiTietGioHang>();
        List<BienThe> listBT = new List<BienThe>();
        List<SanPham> listSP = new List<SanPham>();
        AssignmentDBContext _context = new AssignmentDBContext();
        public GioHangController(ILogger<GioHangController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = urlapi;
            _context = new AssignmentDBContext();

            // List ChitietGioHang
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/ChiTietGioHang").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                listCTGH = JsonConvert.DeserializeObject<List<ChiTietGioHang>>(data);
            }
            // List BienThe
            HttpResponseMessage response1 = _httpClient.GetAsync(_httpClient.BaseAddress + "/BienThe").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data1 = response1.Content.ReadAsStringAsync().Result;
                listBT = JsonConvert.DeserializeObject<List<BienThe>>(data1);
            }
            // List SanPham
            HttpResponseMessage response2 = _httpClient.GetAsync(_httpClient.BaseAddress + "/SanPham").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data2 = response2.Content.ReadAsStringAsync().Result;
                listSP = JsonConvert.DeserializeObject<List<SanPham>>(data2);
            }

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
        // HIỂN THỊ SẢN PHẨM TRONG GIỎ HÀNG
        public IActionResult ShowCart()
        {
            // Nếu chưa đăng nhập thì hiển thị toàn bộ cart trong session
            if (HttpContext.Session.GetString("IdUser") == null)
            {
                var products = GioHangService.GetObjFormSession(HttpContext.Session, "Cart");
                if (products.Count == 0)
                {
                    TempData["Message"] = "Không có sản phẩm nào trong giỏ hàng!";
                }
                ViewData["listBienThe"] = LoadBThe();
                ViewData["listSP"] = LoadSP();
                return View(products);
            }
            else // Lấy toàn bộ chitietgiohang của người dùng 
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));// Lấy iduser đăng nhập
                var ctgh = listCTGH.Where(c => c.IDNguoiDung == iduser).ToList();
                if (ctgh.Count == 0)
                {
                    TempData["Message"] = "Không có sản phẩm nào trong giỏ hàng!";
                }
                ViewData["listBienThe"] = LoadBThe();
                ViewData["listSP"] = LoadSP();
                return View(ctgh);
            }
        }
        // THÊM VÀO GIỎ
        public async Task<IActionResult> AddToCart(Guid id)
        {
            // Lấy Biến thể từ ID
            var product = listBT.Where(c => c.ID.Equals(id)).FirstOrDefault();

            if (HttpContext.Session.GetString("IdUser") == null)  // Nếu không đăng nhập, thêm sp thì lưu vào session giỏ hàng
            {
                //Đọc dữ liệu từ Session xem trong Cart nó có cái gì chưa?
                var cartproducts = GioHangService.GetObjFormSession(HttpContext.Session, "Cart");
                if (cartproducts.Count == 0)
                {
                    var cartitem = new ChiTietGioHang()
                    {
                        IDNguoiDung = new Guid(),
                        IDBienThe = product.ID,
                        SoLuong = 1,
                    };
                    cartproducts.Add(cartitem); // Nếu Cart rỗng thì thêm sp vào luôn
                    // Đưa dữ liệu về lại Session
                    GioHangService.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                }
                else
                {
                    if (!GioHangService.CheckProductInCart(id, cartproducts)) // Cart không rỗng nhưng k chứa sản phẩm đó
                    {
                        var cartitem = new ChiTietGioHang()
                        {
                            IDNguoiDung = new Guid(),
                            IDBienThe = product.ID,
                            SoLuong = 1,
                        };
                        cartproducts.Add(cartitem); // Nếu Cart chưa chứa sản phẩm thì thêm sp vào luôn
                                                    // Đưa dữ liệu về lại Session
                        GioHangService.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                    }
                    else // Nếu đã có sản phẩm trong giỏ rồi thì tăng số lượng lên 1
                    {
                        var cartitem = cartproducts.Where(c => c.IDBienThe == id).FirstOrDefault();
                        cartitem.SoLuong++;

                        GioHangService.SetObjToJson(HttpContext.Session, "Cart", cartproducts);
                        var x = GioHangService.GetObjFormSession(HttpContext.Session, "Cart");
                    }
                }
            } // Ngược lại có đăng nhập
            else
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IDNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 

                if (cartDetailList.Count == 0)// Giỏ rỗng thì thêm mới
                {
                    var cartitem = new ChiTietGioHang()
                    {
                        IDNguoiDung = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                        IDBienThe = product.ID,
                        SoLuong = 1,
                    };
                    var url = $"https://localhost:7021/api/ChiTietGioHang/create-chi-tiet-gio-hang?IdBienthe={cartitem.IDBienThe}&IdNguoiDung={cartitem.IDNguoiDung}&soluong={cartitem.SoLuong}";
                    var response = await _httpClient.PostAsync(url, null);
                    
                }
                else // Không rỗng thì ktra có chứa sản phẩm đấy không
                {
                    if (!GioHangService.CheckProductInCart(id, cartDetailList)) // 
                    {
                        var cartitem = new ChiTietGioHang()
                        {
                            IDNguoiDung = Guid.Parse(HttpContext.Session.GetString("IdUser")),
                            IDBienThe = product.ID,
                            SoLuong = 1,
                        };
                        var url = $"https://localhost:7021/api/ChiTietGioHang/create-chi-tiet-gio-hang?IdBienthe={cartitem.IDBienThe}&IdNguoiDung={cartitem.IDNguoiDung}&soluong={cartitem.SoLuong}";
                        var response = await _httpClient.PostAsync(url, null);
                    }
                    else // Nếu đã có sản phẩm trong giỏ rồi thì tăng số lượng lên 1
                    {
                        var cartitem = cartDetailList.Where(c => c.IDBienThe == id).FirstOrDefault();
                        cartitem.SoLuong++;
                        var url = $"https://localhost:7021/api/ChiTietGioHang/{cartitem.ID}?IdBienthe={cartitem.IDBienThe}&IdNguoiDung={cartitem.IDNguoiDung}&soluong={cartitem.SoLuong}";
                        var response = await _httpClient.PutAsync(url, null);
                        
                    }
                }
            }
            return RedirectToAction("ShowCart");
        }
        // CẬP NHẬT GIỎ HÀNG
        public async Task<IActionResult> Update_Quantity(IFormCollection f)
        {
            var countsp = listBT.Where(c=> c.ID == Guid.Parse(f["ID_product"].ToString())).FirstOrDefault(); // Sản phẩm bị sửa
            Guid id = Guid.Parse(f["ID_product"].ToString()); // Lấy id sp bị chỉnh sửa trong giỏ
            // đã đăng nhập
            if (HttpContext.Session.GetString("IdUser") != null) 
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IDNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 

                if (int.Parse(f["Quantity"].ToString()) == 0) // bằng 0 thì xóa luôn
                {
                    var x = cartDetailList.Where(c => c.IDBienThe == id).FirstOrDefault();
                    var url = $"https://localhost:7021/api/ChiTietGioHang/{x.ID}";
                    var response = await _httpClient.DeleteAsync(url);
                }
                else if (int.Parse(f["Quantity"].ToString()) > countsp.SoLuong) // Lớn hơn thì thông báo
                {
                    TempData["OverQuantity"] = "Vượt quá số lượng sản phẩm trong kho!";
                    return RedirectToAction("ShowCart");
                }
                else
                { // cập nhật vào giỏ hàng
                    var x = cartDetailList.Where(c => c.IDBienThe == id).FirstOrDefault();
                    x.SoLuong = int.Parse(f["Quantity"].ToString());
                    var url = $"https://localhost:7021/api/ChiTietGioHang/{x.ID}?IdBienthe={x.IDBienThe}&IdNguoiDung={x.IDNguoiDung}&soluong={x.SoLuong}";
                    var response = await _httpClient.PutAsync(url, null);

                }
                return RedirectToAction("ShowCart");
            }
            else // chưa đăng nhập
            {
                var products = GioHangService.GetObjFormSession(HttpContext.Session, "Cart"); // Lấy cart trong session
                if (int.Parse(f["Quantity"].ToString()) == 0)
                {
                    var x = products.Where(c => c.IDBienThe == id).FirstOrDefault();
                    products.Remove(x);
                }
                else if (int.Parse(f["Quantity"].ToString()) > countsp.SoLuong)
                {
                    TempData["OverQuantity"] = "Vượt quá số lượng sản phẩm trong kho!";
                    return RedirectToAction("ShowCart");
                }
                else
                {
                    var x = products.Where(c => c.IDBienThe == id).FirstOrDefault();
                    x.SoLuong = int.Parse(f["Quantity"].ToString());
                }
                GioHangService.SetObjToJson(HttpContext.Session, "Cart", products);
                return RedirectToAction("ShowCart");
            }
        }
        // XÓA SẢN PHẨM TRONG GIỎ HÀNG
        public async Task<IActionResult> RemoveAll()
        {
            if (HttpContext.Session.GetString("IdUser") != null)// đã đăng nhập
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IDNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 
                for (int i = 0; i < cartDetailList.Count; i++)
                {
                    var url = $"https://localhost:7021/api/ChiTietGioHang/{cartDetailList[i].ID}";
                    var response = await _httpClient.DeleteAsync(url);
                }
                return RedirectToAction("ShowCart");
            }
            else
            {
                GioHangService.RemoveAll(HttpContext.Session, "Cart");
                return RedirectToAction("ShowCart");
            }
        }
        public async Task<IActionResult> RemoveCartItem(Guid id)
        {
            if (HttpContext.Session.GetString("IdUser") != null)// đã đăng nhập
            {
                Guid iduser = Guid.Parse(HttpContext.Session.GetString("IdUser"));
                var cartDetailList = listCTGH.Where(c => c.IDNguoiDung == iduser).ToList();// Lấy cartitem trong CSDL của người dùng 
                ChiTietGioHang cartItem = cartDetailList.Where(c => c.IDBienThe == id).FirstOrDefault();

                var url = $"https://localhost:7021/api/ChiTietGioHang/{cartItem.ID}";
                var response = await _httpClient.DeleteAsync(url);
                return RedirectToAction("ShowCart");
            }
            else
            {
                var products = GioHangService.GetObjFormSession(HttpContext.Session, "Cart");
                var x = products.Where(c => c.IDBienThe == id).FirstOrDefault();
                products.Remove(x);
                GioHangService.SetObjToJson(HttpContext.Session, "Cart", products);
                return RedirectToAction("ShowCart");
            }
        }
    }
}
