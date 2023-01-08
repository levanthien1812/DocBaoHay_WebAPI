using System;
using System.Collections.Generic;
using System.Text;

namespace DocBaoHay_WebAPI.Models
{
    public class NguoiDung
    {
        public int ID { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public bool QuanTriVien { get; set; }
    }
}
