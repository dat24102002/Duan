using AppData.Models;

namespace AppAPI.IServices
{
    public interface ISanPhamService
    {
        public List<SanPham> GetAll();
        public SanPham GetByID(Guid id);
        public bool Add(SanPham item);
        public bool Update(SanPham item);
        public bool Remove(SanPham item);
    }
}
