using AppData.Models;

namespace AppAPI.IServices
{
    public interface IThuocTinhLoaiSPServices
    {
        public List<ThuocTinhLoaiSP> GetAll();
        public ThuocTinhLoaiSP GetByID(Guid id);
        public bool Add(ThuocTinhLoaiSP item);
        public bool Update(ThuocTinhLoaiSP item);
        public bool Remove(ThuocTinhLoaiSP item);
    }
}
