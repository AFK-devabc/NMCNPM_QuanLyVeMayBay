using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCNPM_QuanLyVeMayBay
{
    internal class DataLoaiVe
    {
        public string TenLoaiVe { get; set; }
        public string TiLe { get; set; }
        public DataLoaiVe(string TenLoaiVe = "", string tile = "")
        {
            this.TenLoaiVe = TenLoaiVe;
            this.TiLe = tile;
        }
    }
}
