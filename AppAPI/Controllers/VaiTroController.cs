using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaiTroController : ControllerBase
    {
        private readonly IAllRepository<VaiTro> vaitr;
        AssignmentDBContext context = new AssignmentDBContext();
        public VaiTroController()
        {
            vaitr = new AllRepository<VaiTro>(context, context.VaiTros);
        }
        // GET: api/<ThuocTinhLoaiSPController>
        [HttpGet]
        public List<VaiTro> Get()
        {
            return vaitr.GetAll().ToList();
        }

        // GET api/<ThuocTinhLoaiSPController>/5
        [HttpGet("{name}")]
        public List<VaiTro> Get(string name)
        {
            return vaitr.GetAll().Where(x => x.Ten.Contains(name)).ToList();
        }

        // POST api/<ThuocTinhLoaiSPController>
        [HttpPost("create-vai-tro")]
        public bool Post(int trangthai, string ten)
        {
            VaiTro vaitro = new VaiTro();

            vaitro.Ten = ten;
            vaitro.TrangThai = trangthai;
            vaitro.ID = Guid.NewGuid();
            return vaitr.Add(vaitro);
        }
        [HttpPut("{id}")]
        public bool Put(Guid id, int trangthai, string ten)
        {
            var vtro = vaitr.GetAll().First(p => p.ID == id);
            vtro.Ten = ten; vtro.TrangThai = trangthai;
            return vaitr.Update(vtro);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("Delete/{id}")]
        public bool Delete(Guid id)
        {
            var vtro = vaitr.GetAll().First(p => p.ID == id);
            return vaitr.Delete(vtro);
        }
    }
}
