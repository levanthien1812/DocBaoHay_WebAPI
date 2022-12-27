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
    }
}
