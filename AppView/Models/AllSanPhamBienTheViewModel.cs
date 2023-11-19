namespace AppView.Models
{
    public class AllSanPhamBienTheViewModel
    {
        public Guid IDBienThe { get; set; }
        public Guid IDSanPham { get; set; }
        public string Ten { get; set; }
        public int SoLuong { get; set; }
        public int GiaBan { get; set; }
        public DateTime NgayTao { get; set; }
        public int TrangThai { get; set; }
        public string Anh { get; set; }
        public Guid IDChiTietBienThe { get; set; }
        public Guid IDGiaTri { get; set; }
        public string TenGiaTri { get; set; }
        public Guid IDThuocTinh { get; set; }
        public string TenThuocTinh { get; set; }
        public Guid IDLoaiSP { get; set; }
        public string TenLoaiSP { get; set; }
        public string MoTa { get; set; }
    }
}
