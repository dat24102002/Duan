using AppData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppAPI.IServices;
using AppAPI.Services;
using AppData.ViewModels;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        //private readonly IAllRepository<HoaDon> repos;
        //AssignmentDBContext context = new AssignmentDBContext();
        //DbSet<HoaDon> hoadon;
        private readonly IHoaDonService _iHoaDonService;
        public HoaDonController()
        {
            //hoadon = context.HoaDons;
            //AllRepository<HoaDon> all = new AllRepository<HoaDon>(context, hoadon);
            //repos = all;
            _iHoaDonService = new HoaDonService(); 
        }

        // GET: api/<HoaDOnController>
        [HttpGet]
        public List<HoaDon> Get()
        {
            return _iHoaDonService.GetAllHoaDon();
        }
        [HttpPost]
        public bool CreateHoaDon(HoaDonViewModel hoaDonViewModel)
        {
            return _iHoaDonService.CreateHoaDon(hoaDonViewModel.ChiTietHoaDons, hoaDonViewModel.Ten,hoaDonViewModel.SDT,hoaDonViewModel.Email, hoaDonViewModel.PhuongThucThanhToan,hoaDonViewModel.DiaChi,
                hoaDonViewModel.TienShip);
        }
    }
}



















