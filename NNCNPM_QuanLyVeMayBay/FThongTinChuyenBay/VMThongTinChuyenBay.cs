using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCNPM_QuanLyVeMayBay
{
    class VMThongTinChuyenBay
    {
        public IList<DataLoaiVe> dataLoaiVes { get; set; }
        public VMThongTinChuyenBay() { dataLoaiVes = new List<DataLoaiVe>(); }
    }
}
