using DocBaoHay_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace DocBaoHay_WebAPI.Controllers
{
    [RoutePrefix("api/nguoi-dung")]
    public class NguoiDungController : ApiController
    {
        [Route("dang-ky", Name = "DangKy")]
        [HttpPost]
        public NguoiDung DangKy(NguoiDung nguoiDung)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "HoTen", nguoiDung.HoTen },
                    { "TenDangNhap", nguoiDung.TenDangNhap },
                    { "Email", nguoiDung.Email },
                    { "MatKhau", nguoiDung.MatKhau }
                };
                int result = int.Parse(Database.Database.ExecuteCommand("TaoNguoiDung", param).ToString());
                if (result == 1)
                {
                    return nguoiDung;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
