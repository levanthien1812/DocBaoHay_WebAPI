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
                int result = int.Parse(Database.Database.ExecuteCommand("TaoNguoiDung", param, 1).ToString());
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

        [Route("dang-nhap", Name = "DangNhap")]
        [HttpGet]
        public IHttpActionResult DangNhap(string email, string matKhau)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "Email", email },
                    { "MatKhau", matKhau }
                };
                DataTable result = Database.Database.ReadTable("DangNhap", param);
                NguoiDung nd = new NguoiDung();
                if (result.Rows.Count > 0) { 
                    nd.ID = int.Parse(result.Rows[0]["ID"].ToString());
                    nd.TenDangNhap = result.Rows[0]["TenDangNhap"].ToString();
                    nd.HoTen = result.Rows[0]["HoTen"].ToString();
                    nd.Email = result.Rows[0]["Email"].ToString();
                    nd.MatKhau = result.Rows[0]["MatKhau"].ToString();
                    nd.QuanTriVien = bool.Parse(result.Rows[0]["QuanTriVien"].ToString());
                } else
                {
                    nd = null;
                }
                return Ok(nd);
            } catch
            {
                return NotFound();
            }
        }

        [Route("{nguoiDungId}/theo-doi/tac-gia")]
        [HttpGet]
        public IHttpActionResult GetFollowedAuthors (int nguoiDungId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "NguoiDungId", nguoiDungId }
                };
                DataTable result = Database.Database.ReadTable("TimTacGiaTheoDoi", param);
                return Ok(result);
            } catch
            {
                return NotFound();
            }
        }

        [Route("{nguoiDungId}/theo-doi/tac-gia/{tacGiaId}")]
        [HttpDelete]
        public int UnfollowedAuthors (int nguoiDungId, int tacGiaId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "NguoiDungId", nguoiDungId },
                    { "TacGiaId" , tacGiaId }
                };
                Database.Database.ExecuteCommand("XoaTheoDoiTacGia", param);
                return 1;
            } catch
            {
                return -1;
            }
        }

        [Route("{nguoiDungId}/theo-doi/chu-de")]
        [HttpGet]
        public IHttpActionResult GetFollowedTopics(int nguoiDungId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "NguoiDungId", nguoiDungId }
                };
                DataTable result = Database.Database.ReadTable("TimChuDeTheoDoi", param);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("{nguoiDungId}/theo-doi/chu-de/{chuDeId}")]
        [HttpDelete]
        public int UnfollowedTopics(int nguoiDungId, int chuDeId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "NguoiDungId", nguoiDungId },
                    { "ChuDeId" , chuDeId }
                };
                Database.Database.ExecuteCommand("XoaTheoDoiChuDe", param);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        [Route("{nguoiDungId}/bao-da-doc")]
        [HttpGet]
        public IHttpActionResult GetRecentNews(int nguoiDungId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "NguoiDungId", nguoiDungId }
                };
                DataTable result = Database.Database.ReadTable("SelectBaiBaoDaDoc", param);
                result = (new BaiBaoController()).AddKhoangTGColumn(result);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("{nguoiDungId}/bao-da-luu")]
        [HttpGet]
        public IHttpActionResult GetSavedNews(int nguoiDungId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "NguoiDungId", nguoiDungId }
                };
                DataTable result = Database.Database.ReadTable("SelectBaiBaoDaLuu", param);
                result = (new BaiBaoController()).AddKhoangTGColumn(result);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("{nguoiDungId}/bao-da-doc/xoa")]
        [HttpDelete]
        public int XoaCacBaiBaoDaDoc(int nguoiDungId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "NguoiDungId", nguoiDungId}
                };
                int result = int.Parse(Database.Database.ExecuteCommand("XoaCacBaiBaoDaDoc", param, 1).ToString());
                return result;
            }
            catch
            {
                return -1;
            }
        }

        [Route("cap-nhat")]
        [HttpPost]
        public int CapNhatTaiKhoan(NguoiDung nd)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "NguoiDungId", nd.ID},
                    {"HoTenMoi", nd.HoTen },
                    {"TenDangNhapMoi", nd.TenDangNhap },
                    {"EmailMoi", nd.Email },
                    {"MatKhauMoi", nd.MatKhau }
                };
                int result = int.Parse(Database.Database.ExecuteCommand("CapNhatTaiKhoan", param, 1).ToString());
                return result;
            }
            catch
            {
                return -1;
            }
        }
    }
}
