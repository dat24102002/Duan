using Microsoft.AspNetCore.Mvc;
using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietGioHangController : ControllerBase
    {
        private readonly IAllRepository<ChiTietGioHang> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public ChiTietGioHangController()
        {
            repos = new AllRepository<ChiTietGioHang>(context, context.ChiTietGioHangs);
        }
        // GET: api/<ChiTietGioHangController>
        [HttpGet]
        public List<ChiTietGioHang> Get()
        {
            return repos.GetAll().ToList();
        }

        // GET api/<ChiTietGioHangController>/5
        [HttpGet("{id}")]
        public ChiTietGioHang Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x=>x.ID==id);
        }

        // POST api/<ChiTietGioHangController>
        [HttpPost("create-chi-tiet-gio-hang")]
        public bool Post(Guid IdBienthe,Guid IdNguoiDung,int soluong)
        {
            ChiTietGioHang chiTietGioHang = new ChiTietGioHang();
            chiTietGioHang.ID = Guid.NewGuid();
            chiTietGioHang.IDBienThe = IdBienthe;
            chiTietGioHang.IDNguoiDung=IdNguoiDung;
            chiTietGioHang.SoLuong=soluong;
            return repos.Add(chiTietGioHang);
        }

        // PUT api/<ChiTietGioHangController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id,Guid IdBienthe, Guid IdNguoiDung, int soluong)
        {
            var chiTietGioHang=repos.GetAll().FirstOrDefault(x=>x.ID == id);
            if (chiTietGioHang != null)
            {
                chiTietGioHang.IDBienThe = IdBienthe;
                chiTietGioHang.IDNguoiDung = IdNguoiDung;
                chiTietGioHang.SoLuong = soluong;
                return repos.Update(chiTietGioHang);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<ChiTietGioHangController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var chiTietGioHang = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (chiTietGioHang != null)
            {
                
                return repos.Delete(chiTietGioHang);
            }
            else
            {
                return false;
            }
        }
    }
}
