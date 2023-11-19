using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuocTinhController : ControllerBase
    {
        private readonly IAllRepository<ThuocTinh> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public ThuocTinhController()
        {
            repos = new AllRepository<ThuocTinh>(context, context.ThuocTinhs);
        }
        // GET: api/<ThuocTinhController>
        [HttpGet]
        public List<ThuocTinh> Get()
        {
            return repos.GetAll();
        }

        // GET api/<ThuocTinhController>/5
        [HttpGet("{id}")]
        public ThuocTinh Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x => x.ID == id);
        }

        // POST api/<ThuocTinhController>
        [HttpPost("Create_ThuocTinh")]
        public bool Post(string ten, DateTime ngaytao, int trangthai)
        {
            ThuocTinh thuocTinh = new ThuocTinh();
            thuocTinh.ID = Guid.NewGuid();
            thuocTinh.Ten = ten;
            thuocTinh.NgayTao = ngaytao;
            thuocTinh.TrangThai = trangthai;
            return repos.Add(thuocTinh);
        }

        // PUT api/<ThuocTinhController>/5
        [HttpPut("Update-ThuocTinh")]
        public bool Put(Guid id, string ten, DateTime ngaytao, int trangthai)
        {
            var thuocTinh = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (thuocTinh != null)
            {
                thuocTinh.Ten = ten;
                thuocTinh.NgayTao = ngaytao;
                thuocTinh.TrangThai = trangthai;
                return repos.Update(thuocTinh);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<ThuocTinhController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var thuocTinh = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (thuocTinh != null)
            {
                return repos.Delete(thuocTinh);
            }
            else
            {
                return false;
            }
        }
    }
}
