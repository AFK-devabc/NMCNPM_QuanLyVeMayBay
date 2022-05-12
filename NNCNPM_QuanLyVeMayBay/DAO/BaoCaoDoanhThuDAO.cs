using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCNPM_QuanLyVeMayBay.DAO
{
    public class BaoCaoDoanhThuDAO
    {
        #region Make singleton
        private static BaoCaoDoanhThuDAO instance;
        public static BaoCaoDoanhThuDAO Instance
        {
            get
            {
                if (instance == null) instance = new BaoCaoDoanhThuDAO(); return instance;
            }
            private set { instance = value; }
        }

        private BaoCaoDoanhThuDAO() { }
        #endregion

        public List<KeyValuePair<string, int>> LaySoChuyenBayTheoNam(int nam)
        {
            List<KeyValuePair<string, int>> soChuyenBay = new List<KeyValuePair<string, int>>();

            //SoChuyenBay.Add(new KeyValuePair<string, int>("1", 25));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("2", 30));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("3", 20));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("4", 40));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("5", 20));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("6", 20));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("7", 30));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("8", 20));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("9", 40));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("10", 25));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("11", 30));
            //SoChuyenBay.Add(new KeyValuePair<string, int>("12", 28));

            for (int i = 1; i < 13; i++)
            {
                string query = string.Format("SELECT COUNT(*) FROM VEMAYBAY vmb, CHUYENBAY cb WHERE vmb.MaChuyenBay = cb.MaChuyenBay AND YEAR(cb.NgayBay) = {0} AND MONTH(cb.NgayBay) = {1}", nam, i);

                int _soChuyenBay = (int)DataProvider.Instance.ExecuteScalar(query);

                soChuyenBay.Add(new KeyValuePair<string, int>(i.ToString(), _soChuyenBay));
            }

            return soChuyenBay;
        }

        public List<KeyValuePair<string, int>> LayDoanhThuTheoNam(int nam)
        {
            List<KeyValuePair<string, int>> doanhThu = new List<KeyValuePair<string, int>>();

            for (int i = 1; i < 13; i++)
            {
                string query = string.Format("SELECT SUM(vmb.GiaTien) FROM VEMAYBAY vmb, CHUYENBAY cb WHERE vmb.MaChuyenBay = cb.MaChuyenBay AND YEAR(cb.NgayBay) = {0} AND MONTH(cb.NgayBay) = {1}", nam, i);

                try
                {
                    System.Decimal __doanhthu = (System.Decimal)DataProvider.Instance.ExecuteScalar(query);

                    int _doanhThu = Convert.ToInt32(__doanhthu);

                    doanhThu.Add(new KeyValuePair<string, int>(i.ToString(), _doanhThu));
                }
                catch
                {
                    doanhThu.Add(new KeyValuePair<string, int>(i.ToString(), 0));
                }
            }

            return doanhThu;
        }
    }
}
