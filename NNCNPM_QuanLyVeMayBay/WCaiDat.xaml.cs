using System;
using System.Collections.Generic;
using System.Data;
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

namespace NNCNPM_QuanLyVeMayBay
{
    /// <summary>
    /// Interaction logic for WCaiDat.xaml
    /// </summary>
    public partial class WCaiDat : Window
    {
        DataTable datatable_ChuyenBay;
        public WCaiDat()
        {
            InitializeComponent();
            datatable_ChuyenBay = DataProvider.Instance.ExecuteQuery("select MaSanBay as 'Mã sân bay', TenSanBay as 'Tên sân bay' from SANBAY");
            datatable_ChuyenBay.PrimaryKey = new DataColumn[] { datatable_ChuyenBay.Columns["Mã sân bay"] };
            DGV_SanBay.ItemsSource = datatable_ChuyenBay.AsDataView();
            TB_SanBayToiDa.Text = datatable_ChuyenBay.Rows.Count.ToString() ;

            DataTable temp;
            temp = DataProvider.Instance.ExecuteQuery("select * from THAMSO");
            TB_SBTGToiDa.Text = temp.Rows[0].ItemArray[1].ToString();
            TB_TGBMin.Text = temp.Rows[0].ItemArray[2].ToString();
            TB_TGDMin.Text = temp.Rows[0].ItemArray[3].ToString();
            TB_TGDMax.Text = temp.Rows[0].ItemArray[4].ToString();
            TB_TGDatVeMax.Text = temp.Rows[0].ItemArray[6].ToString();
            TB_TGHuyVe.Text = temp.Rows[0].ItemArray[7].ToString();

        }

        private void IsNumberic_only(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            var fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            double val;
            e.Handled = !double.TryParse(fullText, out val);
        }

        private void BTN_Them_Click(object sender, RoutedEventArgs e)
        {
            if(TB_MaSanBay.Text == ""|| TB_TenSanBay.Text == "")
            {
                MessageBox.Show("không thể để trống thông tin");
                return;
            }
            if(datatable_ChuyenBay.Rows.Contains(TB_MaSanBay.Text))
            {
                MessageBox.Show("Mã sân bay không được trùng");
                return;
            }
            string command = "insert into SANBAY values ('" + TB_MaSanBay.Text + "','" + TB_TenSanBay.ToString() + "')";
            
            DataProvider.Instance.ExecuteNonQuery(command);
            TB_SanBayToiDa.Text = datatable_ChuyenBay.Rows.Count.ToString();
            MessageBox.Show("Thêm sân bay thành công!");
            datatable_ChuyenBay.Rows.Add(TB_MaSanBay.Text, TB_TenSanBay.Text);
            TB_MaSanBay.Text = TB_TenSanBay.Text = "";
        }

        private void BTN_Luu_Click(object sender, RoutedEventArgs e)
        {
            string command = "update THAMSO set SoLuongSanBayTGToiDa = '" + TB_SBTGToiDa.Text + "'"
            + ", ThoiGianBayToiThieu = '" + TB_TGBMin.Text + "'"
            + ", ThoiGianDungToiThieu = '" + TB_TGDMin.Text + "'"
            + ", ThoiGianDungToiDa = '" + TB_TGDMax.Text + "'"
            + ", ThoiGianDatVe = '" + TB_TGDatVeMax.Text + "'"
            + ", ThoiGianHuyVe = '" + TB_TGHuyVe.Text + "'";
            DataProvider.Instance.ExecuteNonQuery(command);
            MessageBox.Show("Sửa thông tin thành công");
        }
    }
}
