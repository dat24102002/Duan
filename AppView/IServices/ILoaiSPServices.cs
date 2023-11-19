using AppData.Models;

namespace AppAPI.IServices
{
    public interface ILoaiSPService
    {
        public List<LoaiSP> GetAll();
        public LoaiSP GetByID(Guid id);
        public bool Add(LoaiSP item);
        public bool Update(LoaiSP item);
        public bool Remove(LoaiSP item);
    }
}
