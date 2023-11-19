﻿using AppData.Models;

namespace AppData.ViewModels
{
    public class HoaDonViewModel
    {
        public List<ChiTietHoaDonViewModel> ChiTietHoaDons { get; set; }
        public string Ten { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public string DiaChi { get; set; }
        public int TienShip { get; set; }
    }
}
