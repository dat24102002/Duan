using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuTichDiemController : ControllerBase
    {
        private readonly IAllRepository<LichSuTichDiem> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public LichSuTichDiemController()
        {
            repos = new AllRepository<LichSuTichDiem>(context, context.LichSuTichDiems);
        }
        // GET: api/<LichSuTichDiemController>
        [HttpGet]
        public List<LichSuTichDiem> Get()
        {
            return repos.GetAll();
        }

        // GET api/<LichSuTichDiemController>/5
        [HttpGet("{id}")]
        public LichSuTichDiem Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x => x.ID == id);
        }

        // POST api/<LichSuTichDiemController>
        [HttpPost]
        public bool Post(Guid idNguoiDung,Guid idQuyDoiDiem,Guid idHoaDon,int Diem,int TrangThai)
        {
            LichSuTichDiem lichSuTichDiem = new LichSuTichDiem();
            lichSuTichDiem.ID = Guid.NewGuid();
            lichSuTichDiem.IDQuyDoiDiem = idQuyDoiDiem;
            lichSuTichDiem.IDNguoiDung = idNguoiDung;
            lichSuTichDiem.IDHoaDon = idHoaDon;
            lichSuTichDiem.Diem = Diem;
            lichSuTichDiem.TrangThai = TrangThai;


            return repos.Add(lichSuTichDiem);
        }

        // PUT api/<LichSuTichDiemController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid idNguoiDung, Guid idQuyDoiDiem, Guid idHoaDon, int Diem, int TrangThai)
        {
            var lichSuTichDiem = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (lichSuTichDiem != null)
            {
                lichSuTichDiem.IDQuyDoiDiem = idQuyDoiDiem;
                lichSuTichDiem.IDNguoiDung = idNguoiDung;
                lichSuTichDiem.IDHoaDon = idHoaDon;
                lichSuTichDiem.Diem = Diem;
                lichSuTichDiem.TrangThai = TrangThai;
                return repos.Update(lichSuTichDiem);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<LichSuTichDiemController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var lichSuTichDiem = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (lichSuTichDiem != null)
            {
                return repos.Delete(lichSuTichDiem);
            }
            else
            {
                return false;
            }
        }
    }
}
