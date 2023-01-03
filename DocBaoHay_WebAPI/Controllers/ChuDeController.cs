using DocBaoHay_WebAPI.Database;
using DocBaoHay_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DocBaoHay_WebAPI.Controllers
{
    [RoutePrefix("api/chu-de")]
    public class ChuDeController : ApiController
    {
        [Route("", Name = "GetAllChuDe")]
        [HttpGet]
        public IHttpActionResult getAllChuDe()
        {
            try
            {
                DataTable result = Database.Database.ReadTable("SelectAllChuDe");
                return Ok(result);
            } catch
            {
                return NotFound();
            }
        }

        [Route("theo-doi", Name = "ThemTheoDoiChuDe")]
        [HttpPost]
        public int ThemTheoDoiChuDe(int nguoiDungId, int chuDeId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"NguoiDungId", nguoiDungId},
                {"ChuDeId", chuDeId }
            };

            int result = int.Parse(Database.Database.ExecuteCommand("ThemTheoDoiChuDe", param).ToString());
            return result;
        }

        [Route("kiem-tra-theo-doi")]
        [HttpGet]
        public int KiemTraTheoDoi(int nguoiDungId, int chuDeId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"NguoiDungId", nguoiDungId},
                {"ChuDeId", chuDeId }
            };

            int result = int.Parse(Database.Database.ExecuteCommand("KiemTraTonTaiTDCD", param).ToString());
            return result;
        }
    }
}
