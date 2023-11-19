using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuocTinhLoaiSPController : ControllerBase
    {
        private readonly IAllRepository<ThuocTinhLoaiSP> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public ThuocTinhLoaiSPController()
        {
            repos = new AllRepository<ThuocTinhLoaiSP>(context, context.ThuocTinhSanPhams);
        }
        // GET: api/<ThuocTinhLoaiSPController>
        [HttpGet]
        public List<ThuocTinhLoaiSP> Get()
        {
            return repos.GetAll().ToList();
        }

        // GET api/<ThuocTinhLoaiSPController>/5
        [HttpGet("{id}")]
        public ThuocTinhLoaiSP Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x => x.ID == id);
        }

        // POST api/<ThuocTinhLoaiSPController>
        [HttpPost("create-thuoc-tinh-loaisp")]
        public bool Post(Guid idThuocTinh,Guid idLoaiSP)
        {
            ThuocTinhLoaiSP thuocTinhLoaiSP = new ThuocTinhLoaiSP();
            thuocTinhLoaiSP.ID = Guid.NewGuid();
            thuocTinhLoaiSP.IDThuocTinh = idThuocTinh;
            thuocTinhLoaiSP.IDLoaiSP = idLoaiSP;
            return repos.Add(thuocTinhLoaiSP);
        }

        // PUT api/<ThuocTinhLoaiSPController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid idThuocTinh, Guid idLoaiSP)
        {
            var thuocTinhLoaiSP = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (thuocTinhLoaiSP != null)
            {
                thuocTinhLoaiSP.IDThuocTinh = idThuocTinh;
                thuocTinhLoaiSP.IDLoaiSP = idLoaiSP;
                return repos.Update(thuocTinhLoaiSP);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<ThuocTinhLoaiSPController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var thuocTinhLoaiSP = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (thuocTinhLoaiSP != null)
            {
                return repos.Delete(thuocTinhLoaiSP);
            }
            else
            {
                return false;
            }
        }
    }
}
