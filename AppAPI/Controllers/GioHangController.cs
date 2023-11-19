using Microsoft.AspNetCore.Mvc;
using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly IAllRepository<GioHang> repos;
        AssignmentDBContext context=new AssignmentDBContext();
        public GioHangController()
        {
            repos = new AllRepository<GioHang>(context,context.GioHangs);
        }
        // GET: api/<GioHangController>
        [HttpGet]
        public List<GioHang> Get()
        {
            return repos.GetAll().ToList();
        }

        // GET api/<GioHangController>/5
        [HttpGet("{id}")]
        public GioHang Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x=>x.IDNguoiDung==id);
        }

        // POST api/<GioHangController>
        [HttpPost("create-gio-hang")]
        public bool Post(Guid IdNguoiDung, DateTime ngaytao)
        {
            GioHang gioHang = new GioHang();
            gioHang.IDNguoiDung = IdNguoiDung;
            gioHang.NgayTao=ngaytao;
            return repos.Add(gioHang);
        }

        // PUT api/<GioHangController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id,DateTime ngaytao)
        {
            var gioHang=repos.GetAll().FirstOrDefault(x=>x.IDNguoiDung==id);
            if (gioHang != null)
            {
                gioHang.NgayTao = ngaytao;
                return repos.Update(gioHang);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<GioHangController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var gioHang = repos.GetAll().FirstOrDefault(x => x.IDNguoiDung == id);
            if (gioHang != null)
            {
                
                return repos.Delete(gioHang);
            }
            else
            {
                return false;
            }
        }
    }
}
