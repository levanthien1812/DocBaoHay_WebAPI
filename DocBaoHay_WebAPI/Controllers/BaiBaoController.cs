using DocBaoHay_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace DocBaoHay_WebAPI.Controllers
{
    [RoutePrefix("api/bai-bao")]
    public class BaiBaoController : ApiController
    {
        [Route("hot", Name = "GetBaiBaoNong")]
        [HttpGet]
        public IHttpActionResult getBaiBaoNong()
        {
            try
            {
                DataTable result = Database.Database.ReadTable("SelectBaiBaoNong");
                result = AddKhoangTGColumn(result);
                return Ok(result);
            } catch
            {
                return NotFound();
            }
        }

        [Route("cho-ban", Name = "GetBaiBaoChoBan")]
        [HttpGet]
        public IHttpActionResult getBaiBaoChoBan(int nguoiDungId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    {"NguoiDungId", nguoiDungId }
                };
                DataTable result = Database.Database.ReadTable("SelectBaiBaoChoBan", param);
                result = AddKhoangTGColumn(result);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        public DataTable AddKhoangTGColumn (DataTable table)
        {
            table.Columns.Add("KhoangTG", typeof(String));
            foreach (DataRow res in table.Rows)
            {
                Dictionary<string, object> baiBaoId = new Dictionary<string, object>
                    {
                        {"baiBaoId", int.Parse(res["Id"].ToString()) }
                    };
                res["KhoangTG"] = Database.Database.ExecuteCommand("TinhKhoangThoiGian", baiBaoId, 2).ToString();
            }
            return table;
        }

        [Route("", Name = "GetBaiBaoByChuDe")]
        [HttpGet]
        public IHttpActionResult getBaiBaoByChuDe(int ChuDe)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "ChuDeId", ChuDe }
                };
                DataTable result = Database.Database.ReadTable("SelectBaiBaoByChuDe", param);
                result = AddKhoangTGColumn(result);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("{baiBaoId}", Name = "GetBaiBao")]
        [HttpGet]
        public IHttpActionResult getBaiBao(int baiBaoId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "BaiBaoId", baiBaoId }
                };
                DataTable result = Database.Database.ReadTable("SelectBaiBao", param);
                return Ok(result.Rows[0].Table);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("doc")]
        [HttpPost]
        public int ThemBaiBaoDaDoc(int nguoiDungId, int baiBaoId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "NguoiDungId", nguoiDungId},
                    { "BaiBaoId", baiBaoId }
                };
                int result = int.Parse(Database.Database.ExecuteCommand("ThemBaiBaoDaDoc", param).ToString());
                return result;
            } catch
            {
                return -1;
            }
        }

        [Route("luu")]
        [HttpPost]
        public int ThemBaiBaoDaLuu(int nguoiDungId, int baiBaoId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "NguoiDungId", nguoiDungId},
                    { "BaiBaoId", baiBaoId }
                };
                int result = int.Parse(Database.Database.ExecuteCommand("ThemLuuBaiBao", param).ToString());
                return result;
            }
            catch
            {
                return -1;
            }
        }

        [Route("kiem-tra-luu")]
        [HttpGet]
        public int KiemTraLuu(int nguoiDungId, int baiBaoId)
        {
            Dictionary<string, object> param = new Dictionary<string, object>
            {
                {"NguoiDungId", nguoiDungId},
                {"BaiBaoId", baiBaoId }
            };

            int result = int.Parse(Database.Database.ExecuteCommand("KiemTraTonTaiLBB", param).ToString());
            return result;
        }

        [Route("tim-kiem")]
        [HttpGet]

        public IHttpActionResult TimKiem(string tuKhoa)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "TuKhoa", tuKhoa },
                    { "Loai", 1 }
                };
                DataTable result = Database.Database.ReadTable("TimKiem", param);
                result = AddKhoangTGColumn(result);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("{baiBaoId}/doan-van")]
        [HttpGet]
        public IHttpActionResult getDoanVan(int baiBaoId)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>
                {
                    { "BaiBaoId", baiBaoId }
                };
                DataTable result = Database.Database.ReadTable("SelectDoanVan", param);
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
