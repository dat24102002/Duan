using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuyenMaiController : ControllerBase
    {
        private readonly IAllRepository<KhuyenMai> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        DbSet<KhuyenMai> khuyenmai;
        public KhuyenMaiController()
        {
            khuyenmai = context.KhuyenMais;
            AllRepository<KhuyenMai> all = new AllRepository<KhuyenMai> (context, khuyenmai);
            repos = all;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<KhuyenMai> Get()
        {
            return repos.GetAll();
        }

        // GET api/<KhuyenMaiController>/5
        [HttpGet("{name}")]
        public IEnumerable<KhuyenMai> Get(string name)
        {
            return repos.GetAll().Where(p=>p.Ten.Contains(name)); 
        }

        // POST api/<KhuyenMaiController>
        [HttpPost("Create-KhuyenMai")]
        public bool Post(string ten,int giatri,DateTime ngayApDung, DateTime ngayKetThuc, string mota )
        {
            KhuyenMai khuyenMai = new KhuyenMai();
            khuyenMai.ID = Guid.NewGuid();
            khuyenMai.Ten = ten;
            khuyenMai.GiaTri = Convert.ToInt32(giatri);
            khuyenMai.NgayApDung = ngayApDung;
            //khuyenMai.NgayKetThuc = DateTime.ParseExact(ngayKetThuc.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            khuyenMai.NgayKetThuc = ngayKetThuc.ToUniversalTime();
           
            khuyenMai.TrangThai = 1;
            khuyenMai.MoTa = mota;
            return repos.Add(khuyenMai);
        }

        // PUT api/<KhuyenMaiController>/5
        [HttpPut("Update-KhuyenMai")]
        public bool UpdateKhuyenMai(Guid id, string ten, int giatri, DateTime ngayApDung, DateTime ngayKetThuc, string mota, int trangthai)
        {
            var khuyenMai = repos.GetAll().FirstOrDefault(p=>p.ID == id);   
            if (khuyenMai != null)
            {
                khuyenMai.Ten = ten;
                khuyenMai.GiaTri = Convert.ToInt32(giatri);
                khuyenMai.NgayApDung = ngayApDung;
                khuyenMai.NgayKetThuc = ngayKetThuc.ToUniversalTime();
                khuyenMai.TrangThai = trangthai;
                khuyenMai.MoTa = mota;
                return repos.Update(khuyenMai);
            }
            else 
            {
                return false; 
            }
        }

        // DELETE api/<KhuyenMaiController>/5
        [HttpDelete("Delete-KhuyenMai")]
        public bool DeleteKhuyenMai(Guid id)
        {
            var khuyenMai = repos.GetAll().FirstOrDefault(p => p.ID == id);
            if(khuyenMai != null)
            {
                return repos.Delete(khuyenMai);
            }
            else 
            { 
                return false; 
            }
        }
    }
}
