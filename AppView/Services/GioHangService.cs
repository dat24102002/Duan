using AppData.Models;
using Newtonsoft.Json;
namespace AppView.Services
{
    static class GioHangService
    {
        // Đưa dữ liệu vào Session dưới dạng Json
        public static void SetObjToJson(ISession session, string key, object value)// object value là dữ liệu thêm vào Session
        {
            // Chuyển đổi dữ liệu qua dạng JsonString
            var JsonString = JsonConvert.SerializeObject(value); // Đưa dạng List về dạng string
            session.SetString(key, JsonString);// Đưa vào Session
        }
        // Lấy và đưa dữ liệu từ Session về dạng List<obj>
        public static List<ChiTietGioHang> GetObjFormSession(ISession session, string key)
        {
            var data = session.GetString(key); // Đọc dữ liệu từ Session ở dạng chuỗi
            if (data != null)
            {
                var listObj = JsonConvert.DeserializeObject<List<ChiTietGioHang>>(data); // Đưa dạng string về dạng List
                return listObj;
            }
            else
            {
                return new List<ChiTietGioHang>();
            }

        }
       
        // Lấy danh sách Biến thể trong session
        public static List<BienThe> GetProductFromSession(ISession session, string key)
        {
            var data = session.GetString(key); // Đọc dữ liệu từ Session ở dạng chuỗi
            if (data != null)
            {
                var listObj = JsonConvert.DeserializeObject<List<BienThe>>(data); // Đưa dạng string về dạng List
                return listObj;
            }
            else
            {
                return new List<BienThe>();
            }

        }
        // Kiểm tra xem có tồn tại sản phẩm trong giỏ hàng hay không 
        public static bool CheckProductInCart(Guid id, List<ChiTietGioHang> cartProducts)
        {
            return cartProducts.Any(p => p.IDBienThe == id);
        }
        public static void UpdateQuantity(Guid id, List<ChiTietGioHang> cartProducts, int quantity)
        {
            var cartItem = cartProducts.FirstOrDefault(p => p.ID == id);
            cartItem.SoLuong = quantity;
        }
        public static void RemoveCartItem(Guid id, List<ChiTietGioHang> cartProducts)
        {
            var cartItem = cartProducts.FirstOrDefault(p => p.ID == id);
            cartProducts.Remove(cartItem);
        }
        public static void RemoveAll(ISession session, string key)
        {
            session.Clear();
        }
    }
}
