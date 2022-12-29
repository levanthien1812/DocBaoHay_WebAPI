using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DocBaoHay_WebAPI.Controllers
{
    [RoutePrefix("api/tac-gia")]
    public class TacGiaController : ApiController
    {
        [Route("theo-doi", Name = "ThemTheoDoiTacGia")]
        [HttpPost]
        public int ThemTheoDoiChuDe(int nguoiDungId, int tacGiaId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"NguoiDungId", nguoiDungId},
                {"TacGiaId", tacGiaId }
            };

            int result = int.Parse(Database.Database.ExecuteCommand("ThemTheoDoiTacGia", param).ToString());
            return result;
        }

        [Route("kiem-tra-theo-doi")]
        [HttpGet]
        public int KiemTraTheoDoi(int nguoiDungId, int tacGiaId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"NguoiDungId", nguoiDungId},
                {"TacGiaId", tacGiaId }
            };

            int result = int.Parse(Database.Database.ExecuteCommand("KiemTraTonTaiTDTG", param).ToString());
            return result;
        }
    }
}
