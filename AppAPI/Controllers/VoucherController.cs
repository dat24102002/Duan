using AppData.IRepositories;
using AppData.Models;
using AppData.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IAllRepository<Voucher> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public VoucherController()
        {
            repos = new AllRepository<Voucher>(context, context.Vouchers);
        }
        // GET: api/<VoucherController>
        [HttpGet]
        public List<Voucher> Get()
        {
            return repos.GetAll();
        }

        // GET api/<VoucherController>/5
        [HttpGet("{name}")]
        public IEnumerable<Voucher> Get(string name)
        {
            return repos.GetAll().Where(p=>p.Ten.Contains(name));
        }

        // POST api/<VoucherController>
        [HttpPost("Create-Voucher")]
        public bool Post(string Ten, int HinhThucGiamGia,int SoTienCan,int GiaTri,DateTime NgayApDung,DateTime NgayKetThuc,int SoLuong,string MoTa,int TrangThai)
        {
            Voucher voucher = new Voucher();
            voucher.ID = Guid.NewGuid();
            voucher.Ten = Ten;
            voucher.SoTienCan = SoTienCan;
            voucher.GiaTri = GiaTri;
            voucher.NgayApDung = NgayApDung;
            voucher.NgayKetThuc = NgayKetThuc;
            voucher.SoLuong = SoLuong;
            voucher.MoTa = MoTa;    
            voucher.TrangThai = TrangThai;
          
            return repos.Add(voucher);
        }

        // PUT api/<VoucherController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string Ten, int HinhThucGiamGia, int SoTienCan, int GiaTri, DateTime NgayApDung, DateTime NgayKetThuc, int SoLuong, string MoTa, int TrangThai)
        {
            var voucher = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (voucher != null)
            {
                voucher.Ten = Ten;
                voucher.SoTienCan = SoTienCan;
                voucher.GiaTri = GiaTri;
                voucher.NgayApDung = NgayApDung;
                voucher.NgayKetThuc = NgayKetThuc;
                voucher.SoLuong = SoLuong;
                voucher.MoTa = MoTa;
                voucher.TrangThai = TrangThai;
                return repos.Update(voucher);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<VoucherController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var voucher = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (voucher != null)
            {
                return repos.Delete(voucher);
            }
            else
            {
                return false;
            }
        }
    }
}
