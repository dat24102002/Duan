using AppData.IRepositories;
using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppData.Repositories;
//Commit Test
namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietHoaDonController : ControllerBase
    {
        private readonly IAllRepository<ChiTietHoaDon> repos;
        private readonly IAllRepository<BienThe> bientheres;
        AssignmentDBContext context = new AssignmentDBContext();
        DbSet<ChiTietHoaDon> chiTietHoaDons;
        DbSet<BienThe> bienthe;

        public ChiTietHoaDonController()
        {
            chiTietHoaDons = context.ChiTietHoaDons;
            bienthe = context.BienThes;
            AllRepository<ChiTietHoaDon> all = new AllRepository<ChiTietHoaDon>(context, chiTietHoaDons);
            repos = all;
            AllRepository<BienThe> bienthes = new AllRepository<BienThe>(context, bienthe);
            bientheres = bienthes;
        }
        // GET: api/<ChiTietHoaDOnController>
        [HttpGet]
        public IEnumerable<ChiTietHoaDon> Get()
        {
            return repos.GetAll();
        }
        [HttpGet("{id}")]
        public ChiTietHoaDon Get(Guid id)
        {
            return repos.GetAll().FirstOrDefault(x => x.ID == id);
        }

        [HttpPost("Create-ctHoaDon")]

        public string Post(int soLuong, int trangThai, Guid idBienThe, Guid idHoaDon)
        {

            ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
            chiTietHoaDon.ID = Guid.NewGuid();
            chiTietHoaDon.DonGia = bientheres.GetAll().Find(p => p.ID == idBienThe).GiaBan;
            chiTietHoaDon.SoLuong = soLuong;
            chiTietHoaDon.TrangThai = trangThai;
            chiTietHoaDon.IDBienThe = idBienThe;
            chiTietHoaDon.IDHoaDon = idHoaDon;
            if (repos.GetAll().Exists(p => p.IDBienThe == idBienThe && p.IDHoaDon == idHoaDon))
            {
                Guid id = repos.GetAll().Find(p => p.IDBienThe == idBienThe && p.IDHoaDon == idHoaDon).ID;
                ChiTietHoaDon chiTietHoaDon1 = repos.GetAll().Find(p => p.IDBienThe == idBienThe && p.IDHoaDon == idHoaDon);

                if (chiTietHoaDon1.SoLuong + soLuong > bientheres.GetAll().Find(p => p.ID == idBienThe).SoLuong)
                {
                    return "So luong trong kho ko du";
                }
                else
                {
                    chiTietHoaDon1.SoLuong = chiTietHoaDon1.SoLuong + soLuong;
                    return repos.Update(chiTietHoaDon1).ToString();
                }
            }
            else
            {
                return repos.Add(chiTietHoaDon).ToString();
            }



        }

        // PUT api/<ChiTietHoaDonController>/5
        [HttpPut("Update-ChiTietHoaDon")]

        public bool UpdatectHoaDon(Guid id, int donGia, int soLuong, int trangThai, Guid idBienThe, Guid idHoaDon)
        {
            var chiTietHoaDon = repos.GetAll().FirstOrDefault(p => p.ID == id);
            if (chiTietHoaDon != null)
            {
                chiTietHoaDon.DonGia = donGia;
                chiTietHoaDon.SoLuong = soLuong;
                chiTietHoaDon.TrangThai = trangThai;
                chiTietHoaDon.IDBienThe = idBienThe;
                chiTietHoaDon.IDHoaDon = idHoaDon;
                return repos.Add(chiTietHoaDon);
            }
            else { return false; }
        }
        // DELETE api/<ChiTietHoaDonController>/5
        [HttpDelete("Delete-ChiTietHoaDon")]
        public bool DeleteChiTietHoaDon(Guid id)
        {
            var chiTietHoaDon = repos.GetAll().FirstOrDefault(p => p.ID == id);
            if (chiTietHoaDon != null)
            {
                return repos.Delete(chiTietHoaDon);
            }
            else { return false; }
        }
    }
}


















