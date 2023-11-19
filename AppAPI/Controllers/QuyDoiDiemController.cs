using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuyDoiDiemController : ControllerBase
    {
        private readonly IAllRepository<QuyDoiDiem> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public QuyDoiDiemController()
        {
            repos = new AllRepository<QuyDoiDiem>(context, context.QuyDoiDiems);
        }
        // GET: api/<QuyDoiDiemController>
        [HttpGet]
        public List<QuyDoiDiem> Get()
        {
            return repos.GetAll();
        }

        // GET api/<QuyDoiDiemController>/5
        [HttpGet("{id}")]
        public QuyDoiDiem Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x => x.ID == id);
        }

        // POST api/<QuyDoiDiemController>
        [HttpPost]
        public bool Post(int SoDiem,int TiLeTichDiem,int TiLeTieuDiem,int TrangThai)
        {
            QuyDoiDiem quyDoiDiem = new QuyDoiDiem();
            quyDoiDiem.ID = Guid.NewGuid();
            quyDoiDiem.SoDiem = SoDiem; 
            quyDoiDiem.TiLeTichDiem = TiLeTichDiem;
            quyDoiDiem.TiLeTieuDiem = TiLeTieuDiem;
            quyDoiDiem.TrangThai = TrangThai;
            

            return repos.Add(quyDoiDiem);
        }

        // PUT api/<QuyDoiDiemController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, int SoDiem, int TiLeTichDiem, int TiLeTieuDiem, int TrangThai)
        {
            var quyDoiDiem = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (quyDoiDiem != null)
            {
                quyDoiDiem.SoDiem = SoDiem;
                quyDoiDiem.TiLeTichDiem = TiLeTichDiem;
                quyDoiDiem.TiLeTieuDiem = TiLeTieuDiem;
                quyDoiDiem.TrangThai = TrangThai;
                return repos.Update(quyDoiDiem);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<QuyDoiDiemController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var quyDoiDiem = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (quyDoiDiem != null)
            {
                return repos.Delete(quyDoiDiem);
            }
            else
            {
                return false;
            }
        }
    }
}
