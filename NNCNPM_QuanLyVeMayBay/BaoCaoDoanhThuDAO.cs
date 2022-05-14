using System;
using System.Collections.Generic;
using System.Data;
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


        /// <summary>
        /// Trả về số chuyến bay của từng tháng trong năm
        /// </summary>
        /// <param name="nam"></param>
        /// <returns></returns>
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
                string query = string.Format("SELECT COUNT(cb.MaChuyenBay) FROM VEMAYBAY vmb, CHUYENBAY cb WHERE vmb.MaChuyenBay = cb.MaChuyenBay AND YEAR(cb.NgayBay) = {0} AND MONTH(cb.NgayBay) = {1}", nam, i);

                int _soChuyenBay = (int)DataProvider.Instance.ExecuteScalar(query);

                soChuyenBay.Add(new KeyValuePair<string, int>(i.ToString(), _soChuyenBay));
            }

            return soChuyenBay;
        }


        /// <summary>
        /// Trả về doanh thu của từng tháng trong năm
        /// </summary>
        /// <param name="nam"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Trả về doanh thu của từng chuyến bay trong tháng 
        /// </summary>
        /// <param name="thang"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, int>> LayDoanhThuTheoThang(int nam, int thang)
        {
            List<KeyValuePair<string, int>> doanhThu = new List<KeyValuePair<string, int>>();

            string query = string.Format("SELECT cb.MaChuyenBay, SUM(vmb.GiaTien) DoanhThu FROM(SELECT * FROM CHUYENBAY cb WHERE MONTH(cb.NgayBay) = {0} AND YEAR(cb.NgayBay) = {1}) cb LEFT JOIN VeMayBay vmb ON cb.MaChuyenBay = vmb.MaChuyenBay GROUP BY cb.MaChuyenBay ", thang, nam);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                string maChuyenBay = (string)row["MaChuyenBay"];

                System.Decimal _doanhThu = 0;
                int __doanhThu = 0;
                try
                {
                    _doanhThu = (System.Decimal)row["DoanhThu"];
                    __doanhThu = Convert.ToInt32(_doanhThu);
                }
                catch
                {

                }

                doanhThu.Add(new KeyValuePair<string, int>(maChuyenBay, __doanhThu));
            }

            return doanhThu;
        }


        /// <summary>
        /// Hàm trả về số vé từng chuyến bay trong tháng
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="thang"></param>
        /// <returns></returns>
        public List<KeyValuePair<string, int>> LaySoVeTheoThang(int nam, int thang)
        {
            List<KeyValuePair<string, int>> soVe = new List<KeyValuePair<string, int>>();

            string query = string.Format("SELECT cb.MaChuyenBay, COUNT(vmb.MaChuyenBay) SoVe FROM(SELECT * FROM CHUYENBAY cb WHERE MONTH(cb.NgayBay) = {0} AND YEAR(cb.NgayBay) = {1}) cb LEFT JOIN VeMayBay vmb ON cb.MaChuyenBay = vmb.MaChuyenBay GROUP BY cb.MaChuyenBay ", thang, nam);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                string maChuyenBay = (string)row["MaChuyenBay"];

                System.Decimal _soVe = 0;
                int __soVe = 0;
                try
                {
                    _soVe = (System.Decimal)row["SoVe"];
                    __soVe = Convert.ToInt32(_soVe);
                }
                catch
                {

                }

                soVe.Add(new KeyValuePair<string, int>(maChuyenBay, __soVe));
            }

            return soVe;
        }
    }
}
