using System;
using System.Collections.Generic;
using System.Text;

namespace DocBaoHay_WebAPI.Models
{
    public class BaiBao
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public string Thumbnail { get; set; }
        public string Content { get; set; }
        public string KhoangTG { get; set; }
        public int ChuDe { get; set; }
        public int TacGia { get; set; }
    }
}
