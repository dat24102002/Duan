using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietBienTheController : ControllerBase
    {
        private readonly IAllRepository<ChiTietBienThe> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public ChiTietBienTheController()
        {
            repos = new AllRepository<ChiTietBienThe>(context, context.ChiTietBienThes);
        }
        // GET: api/<ChiTietBienTheController>
        [HttpGet]
        public List<ChiTietBienThe> Get()
        {
            return repos.GetAll();
        }

        // GET api/<ChiTietBienTheController>/5
        [HttpGet("{id}")]
        public ChiTietBienThe Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x => x.ID == id);
        }

        // POST api/<ChiTietBienTheController>
        [HttpPost("create-CTBienThe")]
        public bool Post(Guid idBienThe, Guid idGiaTri)
        {
            ChiTietBienThe ctbt = new ChiTietBienThe();
            ctbt.ID = Guid.NewGuid();
            ctbt.IDBienThe = idBienThe;
            ctbt.IDGiaTri = idGiaTri;
            ctbt.TrangThai = 1;
            return repos.Add(ctbt);
        }

        // PUT api/<ChiTietBienTheController>/5
        [HttpPut("Update-CTBienThe")]
        public bool Put(Guid id, Guid idBienThe, Guid idGiaTri, int trangthai)
        {
            var ctbt = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (ctbt != null)
            {
                ctbt.IDBienThe = idBienThe;
                ctbt.IDGiaTri = idGiaTri;
                ctbt.TrangThai = trangthai;
                return repos.Update(ctbt);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<ChiTietBienTheController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var ctbt = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (ctbt != null)
            {
                return repos.Delete(ctbt);
            }
            else
            {
                return false;
            }
        }
    }
}
