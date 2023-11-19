using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiSPController : ControllerBase
    {
        private readonly IAllRepository<LoaiSP> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public LoaiSPController()
        {
            repos = new AllRepository<LoaiSP>(context, context.LoaiSPs);
        }
        // GET: api/<LoaiSPController>
        [HttpGet]
        public List<LoaiSP> Get()
        {
            return repos.GetAll();
        }

        // GET api/<LoaiSPController>/5
        [HttpGet("{id}")]
        public LoaiSP Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x=>x.ID == id);
        }

        // POST api/<LoaiSPController>
        [HttpPost("create-loaisp")]
        public bool Post(string ten,Guid? idLoaiSPCha, int trangthai)
        {
            LoaiSP loaiSP = new LoaiSP();
            loaiSP.ID = Guid.NewGuid();
            loaiSP.Ten = ten;
            loaiSP.IDLoaiSPCha = idLoaiSPCha;
            loaiSP.TrangThai = trangthai;
            return repos.Add(loaiSP);
        }

        // PUT api/<LoaiSPController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string ten, Guid? idLoaiSPCha, int trangthai)
        {
            var loaiSP = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (loaiSP != null)
            {
                loaiSP.Ten = ten;
                loaiSP.TrangThai= trangthai;
                loaiSP.IDLoaiSPCha = idLoaiSPCha;
                return repos.Update(loaiSP);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<LoaiSPController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var loaiSP = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (loaiSP != null)
            {
                return repos.Delete(loaiSP);
            }
            else
            {
                return false;
            }
        }
    }
}
