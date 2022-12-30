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
                return Ok(result);
            } catch
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
                return Ok(result);
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
    }
}
