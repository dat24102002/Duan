using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoiDungController : ControllerBase
    {
        private readonly IAllRepository<NguoiDung> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public NguoiDungController()
        {
            repos = new AllRepository<NguoiDung>(context, context.NguoiDungs);
        }
        // GET: api/<SanPhamController>
        [HttpGet]
        public List<NguoiDung> Get()
        {
            return repos.GetAll();
        }

        // GET api/<SanPhamController>/5
        [HttpGet("{name}")]
        public List<NguoiDung> Get(string name)
        {
            return repos.GetAll().Where(x => x.Ten.Contains(name)).ToList();
        }

        // POST api/<SanPhamController>
        [HttpPost("create-nguoi-dung")]
        public bool Post(string ten, string tendem, string ho, DateTime ngaysinh, int gioitinh, string email, string diachi, string sdt, int diemtich, Guid idvaitro)
        {
            NguoiDung nguoiDung = new NguoiDung();
            nguoiDung.IDNguoiDung = Guid.NewGuid();
            nguoiDung.Ho = ho;
            nguoiDung.Ten = ten;
            nguoiDung.TenDem = tendem;
            nguoiDung.GioiTinh = gioitinh;
            nguoiDung.NgaySinh = ngaysinh;
            nguoiDung.Email = email;
            nguoiDung.DiaChi = diachi;
            nguoiDung.SDT = sdt;
            nguoiDung.DiemTich = diemtich;
            nguoiDung.TrangThai = 1;
            nguoiDung.IDVaiTro = idvaitro;
            return repos.Add(nguoiDung);
        }

        // PUT api/<SanPhamController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string ten, string tendem, string ho, DateTime ngaysinh, int gioitinh, string email, string diachi, string sdt, int diemtich, Guid idvaitro)
        {
            var vaitro = repos.GetAll().FirstOrDefault(x => x.IDNguoiDung == id);
            if (vaitro != null)
            {
                vaitro.Ten = ten;
                vaitro.TenDem = tendem;
                vaitro.Ho = ho;
                vaitro.NgaySinh = ngaysinh;
                vaitro.GioiTinh = gioitinh;
                vaitro.Email = email;
                vaitro.DiaChi = diachi;
                vaitro.SDT = sdt;
                vaitro.DiemTich = diemtich;
                vaitro.IDVaiTro = idvaitro;
                return repos.Update(vaitro);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<SanPhamController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var nguoiDung = repos.GetAll().FirstOrDefault(x => x.IDNguoiDung == id);
            return repos.Delete(nguoiDung);
        }
    }
}
