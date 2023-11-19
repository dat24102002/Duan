using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietKhuyenMaiController : ControllerBase
    {
        private readonly IAllRepository<ChiTietKhuyenMai> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        DbSet<ChiTietKhuyenMai> ctKhuyenMai;
        public ChiTietKhuyenMaiController()
        {
            ctKhuyenMai = context.ChiTietKhuyenMais;
            AllRepository<ChiTietKhuyenMai > all = new AllRepository<ChiTietKhuyenMai>(context, ctKhuyenMai);
            repos = all;
        }
        // GET: api/<ChiTietKhuyenMaiController>
        [HttpGet]
        public IEnumerable<ChiTietKhuyenMai> GetAll()
        {
            return repos.GetAll();
        }

        // GET api/<ChiTietKhuyenMaiController>/5
        //[HttpGet("{Name}")]
        //public string Get(int id)
        //{
        //    return repos.GetAll().Where(p => p.Ten.Contains(ten)).ToList();
        //}

        // POST api/<ChiTietKhuyenMaiController>
        [HttpPost("Create-ChiTietKhuyenMai")]
        public bool Post(Guid idBienThe, Guid idKhuyenMai)
        {
            ChiTietKhuyenMai ctKhuyenMai = new ChiTietKhuyenMai();
            ctKhuyenMai.ID = Guid.NewGuid();
            ctKhuyenMai.TrangThai = 1;
            ctKhuyenMai.IDBienThe = idBienThe;
            ctKhuyenMai.IDKhuyenMai = idKhuyenMai;
            return repos.Add(ctKhuyenMai);
        }

        // PUT api/<ChiTietKhuyenMaiController>/5
        [HttpPut("Update-ChiTietKhuyenMai")]
        public bool UpdateKhuyenMai(Guid id, Guid idBienThe, Guid idKhuyenMai, int trangthai)
        {
            var ctKhuyenMai = repos.GetAll().FirstOrDefault(p=>p.ID == id);
            if(ctKhuyenMai != null)
            {
                ctKhuyenMai.IDKhuyenMai = idKhuyenMai;
                ctKhuyenMai.IDBienThe = idBienThe;
                ctKhuyenMai.TrangThai = trangthai;
                return repos.Update(ctKhuyenMai);
            }
            else { return false; }
        }

        // DELETE api/<ChiTietKhuyenMaiController>/5
        [HttpDelete("Delete-ChiTietKhuyenMai")]
        public bool DeleteChiTietKhuyenMai(Guid id)
        {
            var ctKhuyenMai = repos.GetAll().FirstOrDefault(p => p.ID == id);
            if (ctKhuyenMai != null)
            {
                return repos.Delete(ctKhuyenMai);
            }
            else { return false; }
        }
    }
}
