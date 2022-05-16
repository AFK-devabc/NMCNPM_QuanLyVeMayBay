using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Data;

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for WThongTinChuyenBay.xaml
    /// </summary>
   
    public partial class WThongTinChuyenBay : Window
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
        internal class DataTrungGian
        {
            public string MaSanBay { get; set; }
            public string ThoiGianDung { get; set; }
            public DataTrungGian(string MaSanBay = "", string ThoiGianDung = "")
            {
                this.MaSanBay = MaSanBay;
                this.ThoiGianDung = ThoiGianDung;
            }
        }
        class VMThongTinChuyenBay
        {
            public IList<DataLoaiVe> dataLoaiVes { get; set; }
            public IList<DataTrungGian> dataTrungGian { get; set; }
            public VMThongTinChuyenBay() 
            { 
                dataLoaiVes = new List<DataLoaiVe>();
                dataTrungGian = new List<DataTrungGian>();
            }
        }
        List<string> SanBay;
        DataTable dt_TTChuyenBay;
        private VMThongTinChuyenBay vmThongTinChuyenBay;
        public WThongTinChuyenBay()
        {
            InitializeComponent();
            this.vmThongTinChuyenBay = new VMThongTinChuyenBay();
        }

        private void LoadData()
        {
            dt_TTChuyenBay = DataProvider.Instance.ExecuteQuery("select MaChuyenBay as 'Chuyến bay', MaSanBayDi as 'Mã sân bay đi', SB1.TenSanBay as 'Tên sân bay đi', MaSanBayDen as 'Mã sân bay đến', SB2.TenSanBay as 'Tên sân bay đến', ThoiGianBay as 'Thời gian bay', TongSoVe as 'Số Lượng vé', SoGheDaDat as 'So Ve Da Ban', GiaVe as 'Giá vé', from CHUYENBAY, SANBAY SB1, SANBAY SB2, SOLUONGVE where CHUYENBAY.MaSanBayDi = SB1.MaSanBay and CHUYENBAY.MaSanBayDen = SB2.MaSanBay and CHUYENBAY.MaChuyenBay = SOLUONGVE.MaChuyenBay");
            foreach (DataRow i in dt_TTChuyenBay.Rows)
            {
                CB_SanBayDi.Items.Add(i.ItemArray[1].ToString());
                CB_SanBayDen.Items.Add(i.ItemArray[1].ToString());
            }
            dtg_DSChuyenBay.ItemsSource = dt_TTChuyenBay.AsDataView();
            dtg_DSChuyenBay.IsReadOnly = true;
            dt_TTChuyenBay.Clear();
            dt_TTChuyenBay = DataProvider.Instance.ExecuteQuery("select * from HANGVE");

            foreach(DataRow i in dt_TTChuyenBay.Rows)
            {
                vmThongTinChuyenBay.dataLoaiVes.Add(new DataLoaiVe(i.ItemArray[1].ToString(), i.ItemArray[2].ToString()));
            }
            dtg_LoaiVe.ItemsSource = vmThongTinChuyenBay.dataLoaiVes;
            dt_TTChuyenBay.Clear();
            dt_TTChuyenBay = DataProvider.Instance.ExecuteQuery("select * from TRUNGGIAN");
            foreach (DataRow i in dt_TTChuyenBay.Rows)
            {
                vmThongTinChuyenBay.dataTrungGian.Add(new DataTrungGian(i.ItemArray[1].ToString(), i.ItemArray[2].ToString()));
            }
            dtg_SanBayTrungGian.ItemsSource = vmThongTinChuyenBay.dataTrungGian;
        }   
        private void IsNumberic_only(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            double val;
            e.Handled = !double.TryParse(fullText, out val);
        }

        private void bt_Them_Click(object sender, RoutedEventArgs e)
        {
            if (tb_MaChuyenBay.Text == "" || CB_SanBayDi.Text == "" || CB_SanBayDen.Text == "" || DP_NgayBay.Text == "" || TP_NgayBay.Text == "" || tb_ThoiGianBay.Text == "" || tb_PhutBay.Text == "" || tb_GiaVe.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                DataProvider.Instance.ExecuteNonQuery("insert into CHUYENBAY values('" + tb_MaChuyenBay.Text + "','" + CB_SanBayDi.Text + "', '" + CB_SanBayDen.Text + "','" + DP_NgayBay.Text + "','" + TP_NgayBay.Text + "', '" + tb_ThoiGianBay.Text + "', '" + tb_PhutBay.Text + "', '" + tb_GiaVe.Text + "')");
                LoadData();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SanBay = new List<string>() { "AP0001", "AP0002", "AP0003", "AP0004", "AP0005", "AP0006", "AP0007", "AP0008", "AP0009", "AP0010" };
            CB_SanBayDen.ItemsSource = SanBay;
            CB_SanBayDi.ItemsSource = SanBay;
            LoadData();
        }
    }
}
