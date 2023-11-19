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
    public class BienTheController : ControllerBase
    {
        private readonly IAllRepository<BienThe> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        DbSet<BienThe> bienthe;
        public BienTheController()
        {
            bienthe = context.BienThes;
            AllRepository<BienThe> all = new AllRepository<BienThe>(context, bienthe);
            repos = all;
        }

        // GET: api/<BienTheController>
        [HttpGet]
        public List<BienThe> Get()
        {
            return repos.GetAll().ToList();
        }

        // GET api/<BienTheController>/5
        [HttpGet("{id}")]
        public BienThe Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x=>x.ID==id);
        }

        // POST api/<BienTheController>
        [HttpPost("create-bien-the")]
        public bool Post(Guid id, Guid IdSanPham, int SoLuong, int GiaBan, DateTime NgayTao, string Anh)
        {
            BienThe bienThe = new BienThe();
            bienThe.ID=id;
            bienThe.IDSanPham=IdSanPham;
            bienThe.SoLuong=SoLuong;
            bienThe.GiaBan=GiaBan;
            bienThe.NgayTao=NgayTao;
            bienThe.Anh=Anh;
            bienThe.TrangThai = 1;
            return repos.Add(bienThe);
        }

        // PUT api/<BienTheController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, Guid IdSanPham, int SoLuong, int GiaBan, DateTime NgayTao, string Anh)
        {
            var bienThe = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (bienThe != null)
            {
                bienThe.IDSanPham = IdSanPham;
                bienThe.SoLuong = SoLuong;
                bienThe.GiaBan = GiaBan;
                bienThe.NgayTao = NgayTao;
                bienThe.Anh = Anh;
              return   repos.Update(bienThe);
            }
            else
            {
                return false;
            }

        }

        // DELETE api/<BienTheController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var bienThe = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (bienThe != null)
            {
               
                return repos.Delete(bienThe);
            }
            else
            {
                return false;
            }
        }
    }
}
